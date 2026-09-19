using PhotoSauce.MagicScaler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

// --------------------------------------------------------
// メインフォーム
// --------------------------------------------------------

// 画像を選択し数字キーで各種情報を取得
// 1 → フルパス
// 2 → ファイル名
// 3 → フォルダーパス
// 4 → 画像そのもの
// 5 → 拡張子
// 6 → ファイルサイズ
// 7 → 画像サイズ
// 8 → 更新日時
// 9 → 作成日時

// --------------------------------------------------------
// 使用ライブラリ
// --------------------------------------------------------
// PhotoSauce.MagicScaler
// 高性能画像処理

// --------------------------------------------------------
// フォーム
// --------------------------------------------------------
// Form1:メインフォーム
// Form2:バージョン情報フォーム
// Form3:画像ビューフォーム
// Form4:インデックス印刷フォーム

// --------------------------------------------------------
// クラス
// --------------------------------------------------------
// NaturalStringComparer.cs:ファイル名ソート用クラス

namespace MyView
{
    public partial class Form1 : Form
    {
        // 画像拡張子
        private readonly string[] _validExtensions =
        {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff", ".ico", ".wmf", ".emf"
        };

        // 現在表示している画像ファイル
        private List<string> _imageFiles = new List<string>();

        // サムネイル用ImageList
        private ImageList _imageList = new ImageList();

        // 画像番号 → ImageList番号
        // 例：
        // 0番目の画像 → ImageListの1番
        // 1番目の画像 → ImageListの2番
        // ImageListの0番はプレースホルダー
        private readonly Dictionary<int, int>
            _thumbnailIndexMap = new Dictionary<int, int>();

        // 現在の読み込み処理
        private CancellationTokenSource? _cts;

        // 読み込み処理の世代番号
        // フォルダー変更やサムネイルサイズ変更のたびに増やす。
        // 古い処理が新しい画面へ画像を追加しないようにする。
        private int _loadGeneration = 0;

        // プレースホルダー
        private Image? _placeholderImage;

        // ドラッグ開始位置・対象画像番号
        private Point _dragStartPoint;
        private int _dragIndex = -1;

        // 画像プレビュー用Form3
        private Form3? _previewForm;

        // 終了時フラグ
        private bool _isClosing;

        // 前回終了時のフォルダ保存
        // C:\Users\ユーザー名\AppData\Local\MyView\LastFolder.txt
        private string LastFolderFilePath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MyView",
                "LastFolder.txt");

        // ListViewの縦スクロールを先頭へ戻す
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_VSCROLL = 0x0115;
        private const int SB_TOP = 6;

        private void ResetThumbnailScroll()
        {
            if (listViewThumbnails.IsDisposed || !listViewThumbnails.IsHandleCreated)
            {
                return;
            }

            // 縦スクロールバーを一番上へ
            SendMessage(listViewThumbnails.Handle, WM_VSCROLL, (IntPtr)SB_TOP, IntPtr.Zero);

            // 念のため再描画
            listViewThumbnails.Invalidate();
            listViewThumbnails.Update();
        }

        // ============================================================
        // コンストラクタ
        // ============================================================

        public Form1()
        {
            InitializeComponent();

            // フォームサイズ
            this.Width = 1000;
            this.Height = 700;
            this.MinimumSize = new Size(400, 400);
            this.Text = "ともさんの画像一覧帖";

            // ログ
            logTxt.Dock = DockStyle.Bottom;
            logTxt.ReadOnly = true;
            logTxt.BorderStyle = BorderStyle.None;
            logTxt.BackColor = this.BackColor;
            logTxt.TabStop = false;
            logTxt.Height = 50;

            // パス表示
            pathTxt.Dock = DockStyle.Top;

            // レイアウト
            splitContainer1.Dock = DockStyle.Fill;
            treeViewFolders.Dock = DockStyle.Fill;
            listViewThumbnails.Dock = DockStyle.Fill;

            splitContainer1.SplitterDistance = 200;
            splitContainer1.Panel1MinSize = 100;


            numThumbnailSize.Value = 256;

            // 背景色
            treeViewFolders.BackColor = Color.DimGray;
            listViewThumbnails.BackColor = Color.DimGray;
            pathTxt.BackColor = Color.DimGray;

            // ステータス
            StatusLabel1.Text = "画像ファイルはありません。";

            // 初期設定
            SetupComponents();

            // VirtualModeのアイテム取得イベント
            // Designer側ですでに登録されていても二重登録しない。
            listViewThumbnails.RetrieveVirtualItem -= ListViewThumbnails_RetrieveVirtualItem;
            listViewThumbnails.RetrieveVirtualItem += ListViewThumbnails_RetrieveVirtualItem;

            // ドライブ一覧
            LoadDriveNodes();
        }

        // ============================================================
        // 初期設定
        // ============================================================
        private void SetupComponents()
        {
            int size = (int)numThumbnailSize.Value;

            // --------------------------------------------------------
            // ImageList
            // --------------------------------------------------------
            _imageList.ImageSize = new Size(size, size);
            _imageList.ColorDepth = ColorDepth.Depth32Bit;

            // --------------------------------------------------------
            // プレースホルダー
            // --------------------------------------------------------
            _placeholderImage = CreatePlaceholder(size, size);
            _imageList.Images.Clear();
            _imageList.Images.Add("placeholder", _placeholderImage);

            // --------------------------------------------------------
            // ListView
            // --------------------------------------------------------
            listViewThumbnails.View = View.LargeIcon;

            // VirtualModeを使用する
            listViewThumbnails.VirtualMode = true;

            // 選択できる
            listViewThumbnails.MultiSelect = false;

            // ImageList
            listViewThumbnails.LargeImageList = _imageList;
        }

        // ============================================================
        // フォームロード時
        // ============================================================
        private async void Form1_Load(object? sender, EventArgs e)
        {
            // 起動時に前回のフォルダを復元
            await RestoreLastFolderAsync();

            // F5でフォルダー更新(ショートカットキー)
            folderUpdate.ShortcutKeys = Keys.F5;
            // インデクス印刷(ショートカットキー)
            printMenu.ShortcutKeys = Keys.Control | Keys.P;
        }

        // ============================================================
        // 前回のフォルダを復元
        // ============================================================
        private async Task RestoreLastFolderAsync()
        {
            try
            {
                if (!File.Exists(LastFolderFilePath))
                    return;

                string lastFolder = File.ReadAllText(LastFolderFilePath).Trim();

                if (string.IsNullOrEmpty(lastFolder))
                    return;

                if (!Directory.Exists(lastFolder))
                    return;

                pathTxt.Text = lastFolder;

                TreeNode? node = FindAndExpandTreeNode(lastFolder);

                if (node != null)
                {
                    treeViewFolders.SelectedNode =
                        node;

                    node.EnsureVisible();
                }
                else
                {
                    await LoadFolderAsync(lastFolder);
                }
            }
            catch
            {
                // 起動時の復元失敗では
                // アプリを終了させない
            }
        }

        // ============================================================
        // プレースホルダー作成
        // ============================================================
        private Image CreatePlaceholder(int width, int height)
        {
            var bmp = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);

                using (var pen = new Pen(Color.Gray, 2))
                {
                    g.DrawRectangle(pen, 1, 1, width - 2, height - 2);
                }
            }

            return bmp;
        }

        // ============================================================
        // ドライブ一覧
        // ============================================================
        private void LoadDriveNodes()
        {
            treeViewFolders.Nodes.Clear();

            // 特殊フォルダー
            AddSpecialFolderNode("デスクトップ", Environment.SpecialFolder.Desktop);
            AddSpecialFolderNode("ドキュメント", Environment.SpecialFolder.MyDocuments);
            AddSpecialFolderNode("ピクチャ", Environment.SpecialFolder.MyPictures);
            AddSpecialFolderNode("ダウンロード", Environment.SpecialFolder.UserProfile, "Downloads");

            // ドライブ
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                var node = new TreeNode(drive.Name)
                {
                    Tag = drive.RootDirectory.FullName
                };

                node.Nodes.Add("...");
                treeViewFolders.Nodes.Add(node);
            }
        }

        // ============================================================
        // 特殊フォルダーをTreeViewへ追加
        // ============================================================
        private void AddSpecialFolderNode(string displayName, Environment.SpecialFolder specialFolder)
        {
            string path =
                Environment.GetFolderPath(specialFolder);

            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(path))
                return;

            var node = new TreeNode(displayName)
            {
                Tag = path
            };

            node.Nodes.Add("...");

            treeViewFolders.Nodes.Add(node);
        }

        // ============================================================
        // ユーザープロファイル配下のフォルダーを追加
        // ============================================================
        private void AddSpecialFolderNode(string displayName, Environment.SpecialFolder specialFolder, string subFolder)
        {
            string basePath = Environment.GetFolderPath(specialFolder);

            if (string.IsNullOrEmpty(basePath))
                return;

            string path = Path.Combine(basePath, subFolder);

            if (!Directory.Exists(path))
                return;

            var node = new TreeNode(displayName)
            {
                Tag = path
            };

            node.Nodes.Add("...");
            treeViewFolders.Nodes.Add(node);
        }

        // ============================================================
        // フォルダ展開
        // ============================================================
        private void TreeViewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode? node = e.Node;

            if (node == null)
                return;

            if (node.Nodes.Count != 1 || node.Nodes[0].Text != "...")
            {
                return;
            }

            node.Nodes.Clear();

            string? path = node.Tag?.ToString();

            if (string.IsNullOrEmpty(path))
                return;

            try
            {
                var dirInfo = new DirectoryInfo(path);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) != 0)
                    {
                        continue;
                    }

                    var childNode = new TreeNode(dir.Name)
                    {
                        Tag = dir.FullName
                    };

                    childNode.Nodes.Add("...");

                    node.Nodes.Add(childNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // アクセスできないフォルダは無視
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "フォルダ取得エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // フォルダ選択
        // ============================================================
        private async void TreeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null)
                return;

            string? selectedPath = e.Node.Tag.ToString();

            if (string.IsNullOrEmpty(selectedPath))
                return;

            pathTxt.Text = selectedPath;

            await LoadFolderAsync(selectedPath);
        }

        // ============================================================
        // フォルダ読み込み
        // ============================================================
        private async Task LoadFolderAsync(string folderPath)
        {
            // 前回の読み込みをキャンセル
            _cts?.Cancel();
            int generation = Interlocked.Increment(ref _loadGeneration);
            var newCts = new CancellationTokenSource();
            _cts = newCts;
            CancellationToken token = newCts.Token;

            // 画面をクリア
            ClearImages();

            if (!Directory.Exists(folderPath))
            {
                StatusLabel1.Text = "フォルダが存在しません。";
                printMenu.Enabled = false;
                return;
            }

            StatusLabel1.Text = "画像ファイルを検索中...";

            // 画像ファイルを検索
            List<string> files;

            try
            {
                files =
                    await Task.Run(
                        () =>
                            Directory.GetFiles(folderPath)
                                .Where(
                                    file =>
                                        _validExtensions
                                            .Contains(Path.GetExtension(file)
                                            .ToLowerInvariant()))
                                .OrderBy(
                                    file =>
                                        Path.GetFileName(file),
                                    new NaturalStringComparer())
                                .ToList(),
                        token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像検索エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (!IsCurrentGeneration(generation) || token.IsCancellationRequested)
            {
                return;
            }

            // 画像一覧を設定
            _imageFiles = files;
            _thumbnailIndexMap.Clear();
            listViewThumbnails.BeginUpdate();

            try
            {
                listViewThumbnails.VirtualListSize = _imageFiles.Count;
                listViewThumbnails.Invalidate();
            }
            finally
            {
                listViewThumbnails.EndUpdate();
            }

            // 新しいフォルダーを表示したらスクロール位置を先頭へ戻す
            if (_imageFiles.Count > 0)
            {
                ResetThumbnailScroll();
            }

            // 画像がない
            if (_imageFiles.Count == 0)
            {
                StatusLabel1.Text = "画像ファイルはありません。";
                printMenu.Enabled = false;
                return;
            }

            // サムネイル作成開始
            StatusLabel1.Text = $"{_imageFiles.Count:N0} 枚の画像を読み込み中...";
            printMenu.Enabled = true;

            // ListViewのレイアウトが終わってから開始
            try
            {
                BeginInvoke(
                    new Action(
                        () =>
                        {
                            if (IsDisposed || !IsHandleCreated || token.IsCancellationRequested || !IsCurrentGeneration(generation))
                            {
                                return;
                            }

                            _ = GenerateAllThumbnailsAsync(generation, token);
                        }));
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        // ============================================================
        // 全サムネイル作成
        //
        // 今回は複雑なスクロール優先処理を使わず、
        // MagicScalerで全部高速生成する。
        // ============================================================
        private async Task GenerateAllThumbnailsAsync(int generation, CancellationToken token)
        {
            try
            {
                int count = _imageFiles.Count;
                int thumbnailSize = (int)numThumbnailSize.Value;
                const int batchSize = 32;

                for (int batchStart = 0; batchStart < count; batchStart += batchSize)
                {
                    if (token.IsCancellationRequested || !IsCurrentGeneration(generation) || _isClosing)
                    {
                        return;
                    }

                    int batchEnd = Math.Min(batchStart + batchSize, count);

                    int batchCount = batchEnd - batchStart;

                    // ------------------------------------------------
                    // 配列の各要素は画像番号と対応する。
                    // 並列処理でも安全。
                    // ------------------------------------------------

                    Image?[] generatedImages = new Image?[batchCount];

                    await Task.Run(
                        () =>
                        {
                            Parallel.For(0, batchCount, new ParallelOptions
                            {
                                MaxDegreeOfParallelism = GetMaxParallel(),
                                CancellationToken = token
                            },
                                i =>
                                {
                                    int imageIndex = batchStart + i;
                                    if (token.IsCancellationRequested)
                                        return;

                                    if (imageIndex < 0 || imageIndex >= _imageFiles.Count)
                                    {
                                        return;
                                    }

                                    string filePath = _imageFiles[imageIndex];
                                    Image? thumbnail = CreateThumbnail(filePath, thumbnailSize, thumbnailSize);
                                    generatedImages[i] = thumbnail;
                                });
                        },
                        token);

                    // ------------------------------------------------
                    // UIへ追加するデータを作成
                    // ------------------------------------------------
                    var thumbnails = new List<ThumbnailData>();
                    for (int i = 0; i < batchCount; i++)
                    {
                        Image? thumbnail = generatedImages[i];
                        if (thumbnail == null)
                            continue;
                        thumbnails.Add(new ThumbnailData(batchStart + i, thumbnail));
                    }

                    // ------------------------------------------------
                    // ImageListへ追加
                    // ------------------------------------------------
                    await AddThumbnailBatchAsync(thumbnails, generation, token);

                    // ------------------------------------------------
                    // UIが最新状態なら表示を更新
                    // ------------------------------------------------
                    if (token.IsCancellationRequested || !IsCurrentGeneration(generation))
                    {
                        return;
                    }
                }

                if (!token.IsCancellationRequested && IsCurrentGeneration(generation))
                {
                    StatusLabel1.Text = $"サムネイル読み込み完了　{count:N0} 枚";

                    listViewThumbnails.Invalidate();
                    listViewThumbnails.Update();
                }
            }
            catch (OperationCanceledException)
            {
                // フォルダ変更などによるキャンセルは正常
            }
            catch (Exception ex)
            {
                if (!IsDisposed && IsHandleCreated && !token.IsCancellationRequested)
                {
                    try
                    {
                        BeginInvoke(
                            new Action(
                                () =>
                                {
                                    MessageBox.Show(
                                        ex.Message,
                                        "サムネイル作成エラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }));
                    }
                    catch
                    {
                        // 終了処理中は無視
                    }
                }
            }
        }

        // ============================================================
        // ImageListへ一括追加
        // ============================================================
        private async Task AddThumbnailBatchAsync(List<ThumbnailData> thumbnails, int generation, CancellationToken token)
        {
            if (thumbnails.Count == 0)
                return;

            if (token.IsCancellationRequested ||
                !IsCurrentGeneration(generation) ||
                IsDisposed ||
                !IsHandleCreated)
            {
                DisposeThumbnails(thumbnails);
                return;
            }

            bool ownershipTransferred = false;

            try
            {
                await InvokeAsync(
                    () =>
                    {
                        try
                        {
                            if (IsDisposed || !IsCurrentGeneration(generation) || token.IsCancellationRequested)
                            {
                                return;
                            }

                            int firstImageIndex = _imageList.Images.Count;

                            Image[] images = thumbnails.Select(x => x.Thumbnail).ToArray();

                            listViewThumbnails.BeginUpdate();

                            try
                            {
                                _imageList.Images.AddRange(images);

                                ownershipTransferred = true;

                                for (int i = 0;
                                     i < thumbnails.Count;
                                     i++)
                                {
                                    ThumbnailData data = thumbnails[i];

                                    _thumbnailIndexMap[data.Index] = firstImageIndex + i;
                                }
                            }
                            finally
                            {
                                listViewThumbnails.EndUpdate();
                            }

                            int processed = _thumbnailIndexMap.Count;

                            StatusLabel1.Text =
                                $"サムネイル読み込み中... " +
                                $"{processed:N0} / " +
                                $"{_imageFiles.Count:N0} 枚";

                            // VirtualModeの再描画
                            listViewThumbnails.Invalidate();
                            listViewThumbnails.Update();
                        }
                        catch (ObjectDisposedException)
                        {
                            // 終了処理中は無視
                        }
                    });
            }
            catch
            {
                // フォーム終了などによる反映失敗は無視
            }
            finally
            {
                if (!ownershipTransferred)
                {
                    DisposeThumbnails(thumbnails);
                }
            }
        }

        // ============================================================
        // VirtualMode
        //
        // ListViewから
        // 「○番目の画像を表示したい」
        // と要求されたときに呼ばれる。
        // ============================================================

        private void ListViewThumbnails_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
        {
            int index = e.ItemIndex;

            if (index < 0 || index >= _imageFiles.Count)
            {
                e.Item = new ListViewItem();

                return;
            }

            string filePath = _imageFiles[index];

            string fileName = Path.GetFileName(filePath);

            // --------------------------------------------------------
            // ImageListの番号
            //
            // まだ生成されていなければ0番。
            // 0番はプレースホルダー。
            // --------------------------------------------------------

            int imageIndex = 0;

            if (_thumbnailIndexMap.TryGetValue(index, out int mappedImageIndex))
            {
                if (mappedImageIndex >= 0 && mappedImageIndex < _imageList.Images.Count)
                {
                    imageIndex = mappedImageIndex;
                }
            }

            var item = new ListViewItem(fileName, imageIndex);

            e.Item =
                item;
        }

        // ============================================================
        // InvokeAsync
        //
        // UIスレッドで処理を実行する。
        // ============================================================

        private Task InvokeAsync(Action action)
        {
            if (IsDisposed || !IsHandleCreated)
            {
                return Task.CompletedTask;
            }

            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

            try
            {
                BeginInvoke(
                    new Action(
                        () =>
                        {
                            try
                            {
                                if (!IsDisposed)
                                {
                                    action();
                                }

                                tcs.TrySetResult(null);
                            }
                            catch (Exception ex)
                            {
                                tcs.TrySetException(ex);
                            }
                        }));
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        // ============================================================
        // ThumbnailData
        // ============================================================

        private sealed class ThumbnailData
        {
            // 元画像の番号
            public int Index { get; }

            // サムネイル
            public Image Thumbnail { get; }

            public ThumbnailData(int index, Image thumbnail)
            {
                Index = index;
                Thumbnail = thumbnail;
            }
        }

        // ============================================================
        // サムネイル破棄
        // ============================================================

        private void DisposeThumbnails(IEnumerable<ThumbnailData> thumbnails)
        {
            foreach (ThumbnailData data in thumbnails)
            {
                try
                {
                    data.Thumbnail.Dispose();
                }
                catch
                {
                    // 破棄時の例外は無視
                }
            }
        }

        // ============================================================
        // 並列数
        // ============================================================

        private int GetMaxParallel()
        {
            return Math.Min(Environment.ProcessorCount, 8);
        }

        // ============================================================
        // 現在の世代かどうか
        // ============================================================

        private bool IsCurrentGeneration(int generation)
        {
            return generation == Volatile.Read(ref _loadGeneration);
        }

        // ============================================================
        // サムネイル作成本体
        //
        // MagicScalerを使用して高速に画像を縮小する。
        // ============================================================

        private Image? CreateThumbnail(string filePath, int width, int height)
        {
            try
            {
                using var inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                using var outputStream = new MemoryStream();

                var settings = new ProcessImageSettings
                {
                    Width = width,
                    Height = height,
                    ResizeMode = CropScaleMode.Max
                };

                MagicImageProcessor.ProcessImage(inputStream, outputStream, settings);

                outputStream.Position = 0;

                using var resizedImage = Image.FromStream(outputStream, false, false);

                var bitmap = new Bitmap(width, height);

                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);

                    float scale = Math.Min((float)width / resizedImage.Width, (float)height / resizedImage.Height);

                    int drawWidth = (int)(resizedImage.Width * scale);

                    int drawHeight = (int)(resizedImage.Height * scale);

                    int x = (width - drawWidth) / 2;

                    int y = (height - drawHeight) / 2;

                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;

                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.DrawImage(resizedImage, new Rectangle(x, y, drawWidth, drawHeight));
                }

                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        // ============================================================
        // 画像表示をクリア
        // ============================================================

        private void ClearImages()
        {
            _thumbnailIndexMap.Clear();
            listViewThumbnails.BeginUpdate();

            try
            {
                listViewThumbnails.VirtualListSize = 0;
                listViewThumbnails.Invalidate();
                _imageFiles.Clear();

                // 現在のImageList設定を保存
                Size imageSize = _imageList.ImageSize;
                ColorDepth colorDepth = _imageList.ColorDepth;

                // ListViewから古いImageListを外す
                listViewThumbnails.LargeImageList = null;

                // 古いImageListを破棄
                ImageList oldImageList = _imageList;
                oldImageList.Dispose();

                // 新しいImageList
                _imageList = new ImageList
                {
                    ImageSize = imageSize,
                    ColorDepth = colorDepth
                };

                // プレースホルダー
                _placeholderImage = CreatePlaceholder(imageSize.Width, imageSize.Height);

                _imageList.Images.Add("placeholder", _placeholderImage);

                // ListViewへ再設定
                listViewThumbnails.LargeImageList = _imageList;
            }
            finally
            {
                listViewThumbnails.EndUpdate();
            }
        }

        // ============================================================
        // サムネイルサイズ変更
        // ============================================================

        private async void NumThumbnailSize_ValueChanged(object sender, EventArgs e)
        {
            int newSize = (int)numThumbnailSize.Value;

            // 現在の読み込みをキャンセル
            _cts?.Cancel();

            // ImageListサイズ変更
            _imageList.ImageSize = new Size(newSize, newSize);

            // 一旦クリア
            ClearImages();

            // 現在のフォルダ
            string? selectedPath = treeViewFolders.SelectedNode?.Tag?.ToString();

            if (string.IsNullOrEmpty(selectedPath))
            {
                return;
            }

            await LoadFolderAsync(selectedPath);
        }

        // ============================================================
        // パス入力欄でEnter
        // ============================================================

        private void PathTxt_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            string path = pathTxt.Text.Trim();

            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(path))
            {
                MessageBox.Show(
                    "指定されたフォルダーが存在しません。",
                    "フォルダーが見つかりません",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            TreeNode? node = FindAndExpandTreeNode(path);

            if (node == null)
            {
                MessageBox.Show(
                    "指定されたフォルダーをTreeViewで表示できません。",
                    "フォルダーが見つかりません",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            treeViewFolders.SelectedNode = node;

            node.EnsureVisible();
        }

        // ============================================================
        // 指定されたパスまでTreeViewを展開して探す
        // ============================================================

        private TreeNode? FindAndExpandTreeNode(string targetPath)
        {
            string fullPath;

            try
            {
                fullPath = Path.GetFullPath(targetPath).TrimEnd(Path.DirectorySeparatorChar);
            }
            catch
            {
                return null;
            }

            string root =
                Path.GetPathRoot(fullPath) ?? "";

            if (string.IsNullOrEmpty(root))
                return null;

            TreeNode? currentNode = null;

            foreach (TreeNode node in treeViewFolders.Nodes)
            {
                string? nodePath = node.Tag?.ToString();

                if (string.Equals(
                    nodePath,
                    root,
                    StringComparison.OrdinalIgnoreCase))
                {
                    currentNode = node;
                    break;
                }
            }

            if (currentNode == null)
                return null;

            if (string.Equals(fullPath.TrimEnd('\\'), root.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase))
            {
                return currentNode;
            }

            string relativePath = fullPath.Substring(root.Length);

            string[] parts =
                relativePath.Split(
                    Path.DirectorySeparatorChar,
                    StringSplitOptions
                        .RemoveEmptyEntries);

            foreach (string part in parts)
            {
                currentNode.Expand();

                TreeNode? nextNode = null;

                foreach (TreeNode child in currentNode.Nodes)
                {
                    string? childPath = child.Tag?.ToString();

                    if (string.Equals(
                        childPath,
                        Path.Combine(
                            currentNode.Tag?
                                .ToString() ?? "",
                            part),
                        StringComparison
                            .OrdinalIgnoreCase))
                    {
                        nextNode = child;
                        break;
                    }
                }

                if (nextNode == null)
                    return null;

                currentNode = nextNode;
            }

            return currentNode;
        }

        // ============================================================
        // 数字キー取得
        // ============================================================

        private void ListViewThumbnails_KeyDown(object sender, KeyEventArgs e)
        {
            int number = e.KeyCode switch
            {
                Keys.D1 or Keys.NumPad1 => 1,
                Keys.D2 or Keys.NumPad2 => 2,
                Keys.D3 or Keys.NumPad3 => 3,
                Keys.D4 or Keys.NumPad4 => 4,
                Keys.D5 or Keys.NumPad5 => 5,
                Keys.D6 or Keys.NumPad6 => 6,
                Keys.D7 or Keys.NumPad7 => 7,
                Keys.D8 or Keys.NumPad8 => 8,
                Keys.D9 or Keys.NumPad9 => 9,
                _ => 0
            };

            if (number == 0)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            CopyImageInfo(number);
        }

        // ============================================================
        // 数字キーに対応した処理
        // ============================================================

        private void CopyImageInfo(int number)
        {
            if (listViewThumbnails.SelectedIndices.Count == 0)
            {
                return;
            }

            int selectedIndex = listViewThumbnails.SelectedIndices[0];

            if (selectedIndex < 0 || selectedIndex >= _imageFiles.Count)
            {
                return;
            }

            string fullPath = _imageFiles[selectedIndex];

            if (string.IsNullOrEmpty(fullPath))
                return;

            try
            {
                switch (number)
                {
                    case 1: // フルパス
                        {
                            Clipboard.SetText(fullPath);
                            logTxt.Text =
                                "フルパスをコピーしました:" +
                                Environment.NewLine +
                                fullPath;
                            break;
                        }

                    case 2: // ファイル名
                        {
                            string fileName = Path.GetFileName(fullPath);
                            Clipboard.SetText(fileName);
                            logTxt.Text =
                                "ファイル名をコピーしました:" +
                                Environment.NewLine +
                                fileName;
                            break;
                        }

                    case 3: // フォルダーパス
                        {
                            string folderPath = Path.GetDirectoryName(fullPath) ?? "";
                            Clipboard.SetText(folderPath);
                            logTxt.Text =
                                "フォルダーパスをコピーしました:" +
                                Environment.NewLine +
                                folderPath;
                            break;
                        }

                    case 4: // 画像
                        {
                            using (Image sourceImage = Image.FromFile(fullPath))
                            {
                                using (Bitmap bitmap = new Bitmap(sourceImage))
                                {
                                    Clipboard.SetImage(new Bitmap(bitmap));
                                }
                            }
                            logTxt.Text =
                                "画像をコピーしました:" +
                                Environment.NewLine +
                                fullPath;
                            break;
                        }

                    case 5: // 拡張子
                        {
                            string extension = Path.GetExtension(fullPath);
                            Clipboard.SetText(extension);
                            logTxt.Text =
                                "拡張子をコピーしました:" +
                                Environment.NewLine +
                                extension;
                            break;
                        }

                    case 6: // ファイルサイズ
                        {
                            FileInfo fileInfo = new FileInfo(fullPath);
                            string fileSize = FormatFileSize(fileInfo.Length);
                            Clipboard.SetText(fileSize);
                            logTxt.Text =
                                "ファイルサイズをコピーしました:" +
                                Environment.NewLine +
                                fileSize;
                            break;
                        }

                    case 7: // 画像サイズ
                        {
                            using (Image image = Image.FromFile(fullPath))
                            {
                                string imageSize = $"{image.Width} × {image.Height}";
                                Clipboard.SetText(imageSize);
                                logTxt.Text =
                                    "画像サイズをコピーしました:" +
                                    Environment.NewLine +
                                    imageSize;
                            }
                            break;
                        }

                    case 8: // 更新日時
                        {
                            FileInfo updateInfo = new FileInfo(fullPath);
                            string updateDate = updateInfo.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss");
                            Clipboard.SetText(updateDate);
                            logTxt.Text =
                                "更新日時をコピーしました:" +
                                Environment.NewLine +
                                updateDate;
                            break;
                        }

                    case 9: // 作成日時
                        {
                            FileInfo createInfo = new FileInfo(fullPath);
                            string createDate = createInfo.CreationTime.ToString("yyyy/MM/dd HH:mm:ss");
                            Clipboard.SetText(createDate);
                            logTxt.Text =
                                "作成日時をコピーしました:" +
                                Environment.NewLine +
                                createDate;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "クリップボードエラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // ファイルサイズ取得
        // ============================================================

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes:N0} bytes";

            if (bytes < 1024 * 1024)
            {
                return
                    $"{bytes / 1024.0:N2} KB";
            }

            if (bytes < 1024L * 1024L * 1024L)
            {
                return
                    $"{bytes / (1024.0 * 1024.0):N2} MB";
            }

            return
                $"{bytes / (1024.0 * 1024.0 * 1024.0):N2} GB";
        }

        // ============================================================
        // サムネイルのマウス押下
        // ============================================================
        private void ListViewThumbnails_MouseDown(object? sender, MouseEventArgs e)
        {

            // 右クリック
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem? item = listViewThumbnails.GetItemAt(e.X, e.Y);

                if (item == null)
                {
                    _dragIndex = -1;
                    return;
                }

                item.Selected = true;
                item.Focused = true;

                return;
            }

            // 左クリック
            if (e.Button != MouseButtons.Left)
            {
                _dragIndex = -1;
                return;
            }

            _dragStartPoint = e.Location;
            ListViewItem? dragitem = listViewThumbnails.GetItemAt(e.X, e.Y);
            _dragIndex = dragitem?.Index ?? -1;
        }

        // ============================================================
        // サムネイルのドラッグ実行
        // ============================================================
        private void ListViewThumbnails_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (_dragIndex < 0 || _dragIndex >= _imageFiles.Count)
            {
                return;
            }

            Size dragSize = SystemInformation.DragSize;

            Rectangle dragRect =
                new Rectangle(
                    _dragStartPoint.X -
                        dragSize.Width / 2,
                    _dragStartPoint.Y -
                        dragSize.Height / 2,
                    dragSize.Width,
                    dragSize.Height);

            if (dragRect.Contains(e.Location))
            {
                return;
            }

            string fullPath = _imageFiles[_dragIndex];

            if (string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
            {
                return;
            }

            try
            {
                var data = new DataObject();

                data.SetData(
                    DataFormats.FileDrop,
                    new string[]
                    {
                        fullPath
                    });

                listViewThumbnails.DoDragDrop(data, DragDropEffects.Copy);
            }
            catch (InvalidOperationException)
            {
                // 無視
            }
            finally
            {
                _dragIndex = -1;
            }
        }

        // ============================================================
        // コンテキストメニュー表示前
        // ============================================================

        private void ConMenu1_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Point point = listViewThumbnails.PointToClient(Cursor.Position);

            ListViewItem? item = listViewThumbnails.GetItemAt(point.X, point.Y);

            if (item == null)
            {
                e.Cancel = true;
                return;
            }
        }

        // ============================================================
        // コンテキストメニューから画像情報を取得
        //
        // number
        //   1 = フルパス
        //   2 = ファイル名
        //   3 = パス
        //   4 = 画像そのもの
        //   5 = 拡張子
        //   6 = ファイルサイズ
        //   7 = 画像サイズ
        //   8 = 更新日時
        //   9 = 作成日時
        // ============================================================

        // 1 = フルパス
        private void ConTxtMenu1_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(1);
        }

        // 2 = ファイル名
        private void ConTxtMenu2_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(2);
        }

        // 3 = パス
        private void ConTxtMenu3_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(3);
        }

        // 4 = 画像そのもの
        private void ConTxtMenu4_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(4);
        }

        // 5 = 拡張子
        private void ConTxtMenu5_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(5);
        }

        // 6 = ファイルサイズ
        private void ConTxtMenu6_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(6);
        }

        // 7 = 画像サイズ
        private void ConTxtMenu7_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(7);
        }

        // 8 = 更新日時
        private void ConTxtMenu8_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(8);
        }

        // 9 = 作成日時
        private void ConTxtMenu9_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(9);
        }

        // ============================================================
        // サムネイルをダブルクリック
        // ============================================================
        private void ListViewThumbnails_DoubleClick(object? sender, EventArgs e)
        {
            if (listViewThumbnails.SelectedIndices.Count == 0)
            {
                return;
            }

            int index = listViewThumbnails.SelectedIndices[0];

            if (index < 0 || index >= _imageFiles.Count)
            {
                return;
            }

            string fullPath = _imageFiles[index];

            if (string.IsNullOrEmpty(fullPath))
                return;

            if (!File.Exists(fullPath))
                return;

            // --------------------------------------------------------
            // Form3がすでに開いているか
            // --------------------------------------------------------

            if (_previewForm == null || _previewForm.IsDisposed)
            {
                _previewForm = new Form3(fullPath);
                _previewForm.FormClosed += PreviewForm_FormClosed;
                _previewForm.Show(this);
            }
            else
            {
                _previewForm.SetImage(fullPath);
                _previewForm.Activate();
            }
        }

        // ============================================================
        // Form3が閉じられた
        // ============================================================

        private void PreviewForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _previewForm = null;
        }

        // ============================================================
        // フォーム終了時
        // ============================================================

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // --------------------------------------------------------
            // 最後に表示していたフォルダを保存
            // --------------------------------------------------------

            try
            {
                string? selectedPath = treeViewFolders.SelectedNode?.Tag?.ToString();

                if (!string.IsNullOrEmpty(selectedPath) && Directory.Exists(selectedPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LastFolderFilePath)!);

                    File.WriteAllText(LastFolderFilePath, selectedPath);
                }
            }
            catch
            {
                // 終了時の保存失敗は無視
            }

            // --------------------------------------------------------
            // 終了処理
            // --------------------------------------------------------
            _isClosing = true;
            Interlocked.Increment(ref _loadGeneration);
            _cts?.Cancel();
            _cts?.Dispose();

            // --------------------------------------------------------
            // ImageListをListViewから外す
            // --------------------------------------------------------
            listViewThumbnails.LargeImageList = null;

            // --------------------------------------------------------
            // ImageListを破棄
            // --------------------------------------------------------
            _imageList.Dispose();
            base.OnFormClosed(e);
        }

        // ============================================================
        // 終了
        // ============================================================

        private void exitMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ============================================================
        // バージョン情報
        // ============================================================

        private void verMenu_Click(object sender, EventArgs e)
        {
            using (var aboutform = new Form2())
            {
                aboutform.ShowDialog(this);
            }
        }

        // ============================================================
        // フォルダパスのテキストボックスをクリックすると全選択
        // ============================================================

        private void pathTxt_Click(object sender, EventArgs e)
        {
            pathTxt.SelectAll();
        }

        // ============================================================
        // 設定ファイル保存フォルダを開く
        // ============================================================

        private void SettingFolderMenu_Click(object sender, EventArgs e)
        {
            try
            {
                string? folderPath = Path.GetDirectoryName(LastFolderFilePath);

                if (!string.IsNullOrEmpty(folderPath))
                {
                    Directory.CreateDirectory(folderPath);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"\"{folderPath}\"",
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"設定フォルダを開けませんでした。\r\n\r\n{ex.Message}",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // フォルダーツリーを更新
        // ============================================================

        private void RefreshFolderTree()
        {
            // 現在選択しているフォルダーのパスを保存
            string? currentPath = treeViewFolders.SelectedNode?.Tag?.ToString();

            // フォルダーツリーを再構築
            LoadDriveNodes();

            // 現在のフォルダーを復元
            if (!string.IsNullOrEmpty(currentPath))
            {
                TreeNode? restoredNode = FindAndExpandTreeNode(currentPath);

                // フォルダーがまだ存在している
                if (restoredNode != null)
                {
                    treeViewFolders.SelectedNode = restoredNode;

                    restoredNode.EnsureVisible();

                    pathTxt.Text = currentPath;

                    _ = LoadFolderAsync(currentPath);

                    return;
                }

                // ----------------------------------------------------
                // フォルダーが削除・移動されていた場合
                // 親フォルダーへ移動
                // ----------------------------------------------------

                try
                {
                    string? parentPath = Directory.GetParent(currentPath)?.FullName;

                    if (!string.IsNullOrEmpty(parentPath))
                    {
                        TreeNode? parentNode = FindAndExpandTreeNode(parentPath);

                        if (parentNode != null)
                        {
                            treeViewFolders.SelectedNode = parentNode;

                            parentNode.EnsureVisible();

                            pathTxt.Text = parentPath;

                            _ = LoadFolderAsync(parentPath);

                            return;
                        }
                    }
                }
                catch
                {
                    // 無視
                }
            }
        }

        // ============================================================
        // フォルダー更新
        // ============================================================

        private void folderUpdate_Click(object sender, EventArgs e)
        {
            RefreshFolderTree();
        }

        // ============================================================
        // 使い方
        // ============================================================

        private void useMenu_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "使い方.pdf",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("外部アプリで開けませんでした。\n" + ex.Message, "外部アプリオープン失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#else
                MessageBox.Show("外部アプリで開けませんでした。", "外部アプリオープン失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.WriteLine(ex.ToString());
#endif
            }
        }

        // ============================================================
        // listViewThumbnailsへファイルをドロップした時にパスを取得して
        // そのフォルダのサムネイルを表示する
        // ============================================================
        private void listViewThumbnails_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // ドロップされたファイル・フォルダのパス一覧を取得
                if (e.Data?.GetData(DataFormats.FileDrop) is string[] paths)
                {
                    foreach (string droppedPath in paths)
                    {
                        string targetFolderPath = "";

                        // ディレクトリ（フォルダ）かどうかを判定
                        if (Directory.Exists(droppedPath))
                        {
                            // フォルダがドロップされた場合は、そのフォルダのパスをそのまま使う
                            targetFolderPath = droppedPath;
                        }
                        else if (File.Exists(droppedPath))
                        {
                            // ファイルがドロップされた場合は、親フォルダのパスを取得する
                            targetFolderPath = Path.GetDirectoryName(droppedPath) ?? "";
                        }
                        else
                        {
                            continue; // どちらでもなければスキップ
                        }

                        //logTxt.Text = "ファイルがドロップされました(パス取得):" + Environment.NewLine + targetFolderPath;
                        pathTxt.Text = targetFolderPath;

                        // KeyDown イベントを Enter キーが押された前提で手動実行する
                        PathTxt_KeyDown(pathTxt, new KeyEventArgs(Keys.Enter));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // listViewThumbnailsへドロップを許可するカーソル表示にする
        // ============================================================
        private void listViewThumbnails_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effect = DragDropEffects.Copy; // ドロップを許可するカーソル表示にする
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // ============================================================
        // インデックス印刷フォームを開く
        // ============================================================
        private void printMenu_Click(object sender, EventArgs e)
        {
            // 画像が読み込まれていない場合は処理しない
            if (_imageFiles == null || _imageFiles.Count == 0)
            {
                MessageBox.Show("印刷対象の画像がありません。", "案内", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Form4 に現在保持している画像ファイルリスト (_imageFiles) を渡して表示
            using (var form4 = new Form4(_imageFiles))
            {
                form4.ShowDialog(this);
            }
        }
    }
}
