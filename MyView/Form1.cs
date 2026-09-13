using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

// --------------------------------------------------------
// メインフォーム
// --------------------------------------------------------

// 画像を選択し数字キーで各種情報を取得
// 1 → フルパス(C:\Pictures\旅行\IMG_001.jpg)
// 2 → ファイル名(IMG_001.jpg)
// 3 → フォルダーパス(C:\Pictures\旅行)
// 4 → 画像そのもの
// 5 → 拡張子(.jpg)
// 6 → ファイルサイズ(3.25 MB)
// 7 → 画像サイズ(4032 × 3024)
// 8 → 更新日時(2026 /09/09 15:32:10)
// 9 → 作成日時(2026 /08/08 10:15:22)

namespace MyView
{
    public partial class Form1 : Form
    {

        // ============================================================
        // ListViewのスクロールメッセージを検知するIMessageFilter
        // ListViewのHandleへ直接接続せず、アプリケーション全体の
        // メッセージフィルターでListView宛てのスクロールを検知する。
        // ============================================================
        private sealed class ScrollMessageFilter : IMessageFilter
        {
            private const int WM_VSCROLL = 0x0115;
            private const int WM_HSCROLL = 0x0114;
            private const int WM_MOUSEWHEEL = 0x020A;
            private const int WM_MOUSEHWHEEL = 0x020E;

            private readonly Func<IntPtr> _getHandle;
            private readonly Action _onScroll;

            public ScrollMessageFilter(Func<IntPtr> getHandle, Action onScroll)
            {
                _getHandle = getHandle;
                _onScroll = onScroll;
            }

            public bool PreFilterMessage(ref Message m)
            {
                try
                {
                    if (m.Msg != WM_VSCROLL &&
                        m.Msg != WM_HSCROLL &&
                        m.Msg != WM_MOUSEWHEEL &&
                        m.Msg != WM_MOUSEHWHEEL)
                    {
                        return false;
                    }

                    IntPtr targetHandle = _getHandle();

                    if (targetHandle == IntPtr.Zero)
                        return false;

                    bool isTarget = m.HWnd == targetHandle;

                    if (!isTarget)
                        return false;

                    _onScroll();
                }
                catch
                {
                    // メッセージフィルターの例外でアプリを止めない。
                }

                return false;
            }
        }


        // ============================================================
        // スクロール検知用メッセージフィルター
        // ============================================================
        private ScrollMessageFilter? _scrollMessageFilter;

        // 連続するスクロール操作の要求をまとめるための世代
        private int _scrollRequestGeneration;

        // 同一スクロール操作中にBeginInvokeを大量発行しない。
        private int _scrollHandlingPosted;

        // ============================================================
        // 画像拡張子
        // ============================================================
        private readonly string[] _validExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff", ".ico", ".wmf", ".emf" };

        // ============================================================
        // 現在表示している画像ファイル
        // ============================================================
        private List<string> _imageFiles = new List<string>();

        // ============================================================
        // サムネイル用ImageList
        // ============================================================
        private ImageList _imageList = new ImageList();


        // ============================================================
        // 画像番号 → ImageList番号
        // 例：
        //   0番目の画像 → ImageListの1番
        //   1番目の画像 → ImageListの2番
        // 0番はプレースホルダー
        // ============================================================
        private readonly Dictionary<int, int>
            _thumbnailIndexMap = new Dictionary<int, int>();


        // ============================================================
        // 現在サムネイル生成中の画像番号
        // 同じ画像を二重に生成しないために使用
        // ============================================================

        private readonly HashSet<(int Generation, int WorkGeneration, int Index)>
            _thumbnailLoadingPriorities = new HashSet<(int Generation, int WorkGeneration, int Index)>();


        // ============================================================
        // 同期用ロック
        // ============================================================
        private readonly object _thumbnailLock = new object();


        // ============================================================
        // サムネイル生成の並列数制御
        // ============================================================
        private readonly PriorityQueue<ThumbnailRequest, int>
            _thumbnailQueue = new PriorityQueue<ThumbnailRequest, int>();

        private readonly Dictionary<(int Generation, int WorkGeneration, int Index), int>
            _thumbnailQueuedPriority = new Dictionary<(int Generation, int WorkGeneration, int Index), int>();

        // 同じWorkGenerationでスケジューラを二重起動しない。
        // ただし、スクロールで新しいWorkGenerationになった場合は、
        // 古いスケジューラが終了を待っていても新しいスケジューラを
        // 並行して開始できる。
        private readonly HashSet<int> _runningSchedulerWorkGenerations = new HashSet<int>();


        // ============================================================
        // 現在の読み込み処理
        // ============================================================
        private CancellationTokenSource? _cts;

        // サムネイル生成専用のキャンセルトークン。
        // フォルダ読み込み全体のキャンセルとは分離する。
        private CancellationTokenSource? _thumbnailWorkCts;

        // スクロールするたびに増えるサムネイル生成世代。
        // 古いスクロール位置の処理結果をUIへ反映しないために使用。
        private int _thumbnailWorkGeneration;


        // ============================================================
        // 読み込み処理の世代番号
        // ============================================================
        private int _loadGeneration = 0;


        // ============================================================
        // プレースホルダー
        // ============================================================
        private Image? _placeholderImage;

        // ============================================================
        // ドラッグ開始位置・対象画像番号
        // ============================================================
        private Point _dragStartPoint;
        private int _dragIndex = -1;

        // ============================================================
        // 画像プレビュー用Form3
        // ============================================================
        private Form3? _previewForm;

        // ============================================================
        // サムネイル再描画の間隔管理
        // ============================================================
        private long _lastThumbnailInvalidateTick;

        // ============================================================
        // 前回終了時のフォルダ保存
        // ============================================================

        // 保存場所は、C:\Users\ユーザー名\AppData\Local\MyView\LastFolder.txt
        private string LastFolderFilePath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MyView", "LastFolder.txt");
        // MyView.exeと同じ場所に保存
        //private string LastFolderFilePath => Path.Combine(AppContext.BaseDirectory, "LastFolder.txt");

        private async Task RestoreLastFolderAsync()
        {
            try
            {
                // LastFolder.txt が無ければ何もしない
                if (!File.Exists(LastFolderFilePath))
                    return;

                string lastFolder =
                    File.ReadAllText(LastFolderFilePath).Trim();

                if (string.IsNullOrEmpty(lastFolder))
                    return;

                // 前回のフォルダが削除されていた場合も何もしない
                if (!Directory.Exists(lastFolder))
                    return;

                pathTxt.Text = lastFolder;

                TreeNode? node =
                    FindAndExpandTreeNode(lastFolder);

                if (node != null)
                {
                    treeViewFolders.SelectedNode = node;
                    node.EnsureVisible();
                }
                else
                {
                    await LoadFolderAsync(lastFolder);
                }
            }
            catch
            {
                // 起動時の復元失敗ではアプリを終了させない
            }
        }


        // ============================================================
        // コンストラクタ
        // ============================================================
        public Form1()
        {
            InitializeComponent();

            // --------------------------------------------------------
            // フォームサイズ
            // --------------------------------------------------------

            this.Width = 1000;
            this.Height = 700;
            this.MinimumSize = new Size(400, 400);

            this.Text = "ともさんの画像一覧帖";


            // --------------------------------------------------------
            // ログ
            // --------------------------------------------------------
            logTxt.Dock = DockStyle.Bottom;
            logTxt.ReadOnly = true;
            logTxt.BorderStyle = BorderStyle.None;
            logTxt.BackColor = this.BackColor;
            logTxt.TabStop = false;
            logTxt.Height = 50;

            // --------------------------------------------------------
            // パス表示
            // --------------------------------------------------------
            pathTxt.Dock = DockStyle.Top;

            // --------------------------------------------------------
            // レイアウト
            // --------------------------------------------------------
            splitContainer1.Dock = DockStyle.Fill;
            treeViewFolders.Dock = DockStyle.Fill;
            listViewThumbnails.Dock = DockStyle.Fill;
            splitContainer1.SplitterDistance = 200;
            numThumbnailSize.Value = 256;

            // 背景色
            treeViewFolders.BackColor = Color.DimGray;
            listViewThumbnails.BackColor = Color.DimGray;
            pathTxt.BackColor = Color.DimGray;
            //panel1.BackColor = Color.DimGray;

            String statustxt = "画像ファイルはありません。";
            StatusLabel1.Text = statustxt;

            // --------------------------------------------------------
            // 初期設定
            // --------------------------------------------------------
            SetupComponents();
            LoadDriveNodes();

        }

        // ============================================================
        // 初期設定
        // ============================================================
        private void SetupComponents()
        {
            // --------------------------------------------------------
            // スクロール検知
            // Designerで作成された通常のListViewのHandleへ
            // NativeWindowを接続する。
            // --------------------------------------------------------
            AttachScrollMessageFilter();
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
            listViewThumbnails.LargeImageList = _imageList;

            // --------------------------------------------------------
            // VirtualMode
            // --------------------------------------------------------
            listViewThumbnails.VirtualMode = true;
            listViewThumbnails.VirtualListSize = 0;
        }

        // ============================================================
        // フォームロード時
        // ============================================================
        private async void Form1_Load(object? sender, EventArgs e)
        {
            // 起動時に前回のフォルダを復元
            await RestoreLastFolderAsync();

            folderUpdate.ShortcutKeys = Keys.F5;
            
        }

        // ============================================================
        // ListViewへスクロール検知用メッセージフィルターを接続
        // ============================================================

        private void AttachScrollMessageFilter()
        {
            try
            {
                if (_scrollMessageFilter != null)
                    return;

                _scrollMessageFilter =
                    new ScrollMessageFilter(
                        () => listViewThumbnails.IsHandleCreated
                            ? listViewThumbnails.Handle
                            : IntPtr.Zero,
                        () => ListViewThumbnails_ScrollDetected(this, EventArgs.Empty));

                Application.AddMessageFilter(_scrollMessageFilter);
            }
            catch
            {
                // 接続できない場合はスクロール検知を無効にする。
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
            AddSpecialFolderNode(
                "デスクトップ",
                Environment.SpecialFolder.Desktop);

            AddSpecialFolderNode(
                "ドキュメント",
                Environment.SpecialFolder.MyDocuments);

            AddSpecialFolderNode(
                "ピクチャ",
                Environment.SpecialFolder.MyPictures);

            AddSpecialFolderNode(
                "ダウンロード",
                Environment.SpecialFolder.UserProfile,
                "Downloads");

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
            string path = Environment.GetFolderPath(specialFolder);

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
                MessageBox.Show(ex.Message, "フォルダ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        // サムネイル生成処理を新しい世代へ切り替える
        //
        // スクロール時は「フォルダ読み込み」自体は止めず、
        // サムネイル生成だけをキャンセルして新しい表示範囲を
        // 最優先で開始する。
        // ============================================================

        private CancellationToken StartNewThumbnailWork()
        {
            _thumbnailWorkCts?.Cancel();

            _thumbnailWorkCts =
                CancellationTokenSource.CreateLinkedTokenSource(
                    _cts?.Token ?? CancellationToken.None);

            Interlocked.Increment(ref _thumbnailWorkGeneration);

            return _thumbnailWorkCts.Token;
        }

        // ============================================================
        // フォルダ読み込み
        // ============================================================
        private async Task LoadFolderAsync(string folderPath)
        {
            // 前回の処理をキャンセル
            _cts?.Cancel();

            int generation = Interlocked.Increment(ref _loadGeneration);

            var newCts = new CancellationTokenSource();
            _cts = newCts;
            CancellationToken token = newCts.Token;

            _thumbnailWorkCts?.Cancel();
            _thumbnailWorkCts =
                CancellationTokenSource.CreateLinkedTokenSource(token);
            Interlocked.Increment(ref _thumbnailWorkGeneration);

            ClearImages();

            if (!Directory.Exists(folderPath))
            {
                StatusLabel1.Text = "フォルダが存在しません。";
                return;
            }

            StatusLabel1.Text = "画像ファイルを検索中...";

            List<string> files;

            try
            {
                files = await Task.Run(() =>
                    Directory.GetFiles(folderPath)
                        .Where(file => _validExtensions.Contains(
                            Path.GetExtension(file).ToLowerInvariant()))
                        //.OrderBy(file => file)
                        .OrderBy(file => Path.GetFileName(file), new NaturalStringComparer())
                        .ToList(), token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            if (!IsCurrentGeneration(generation) || token.IsCancellationRequested)
                return;

            _imageFiles = files;

            listViewThumbnails.BeginUpdate();
            try
            {
                _thumbnailIndexMap.Clear();
                lock (_thumbnailLock)
                {
                    // 世代をキーにしているため、旧世代の後始末と衝突しない。
                    _thumbnailLoadingPriorities.RemoveWhere(x => x.Generation == generation);
                }

                listViewThumbnails.VirtualListSize = _imageFiles.Count;
                listViewThumbnails.Invalidate();
            }
            finally
            {
                listViewThumbnails.EndUpdate();
            }

            if (_imageFiles.Count == 0)
            {
                StatusLabel1.Text = "画像ファイルはありません。";
                return;
            }

            StatusLabel1.Text =
                $"{_imageFiles.Count:N0} 枚の画像を表示準備中...";

            // --------------------------------------------------------
            // 重要：VirtualModeの初動をListViewのレイアウト後に実行する。
            // ListViewのレイアウト後に、最初の表示範囲の生成を開始する。
            // --------------------------------------------------------
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed || !IsHandleCreated ||
                        token.IsCancellationRequested ||
                        !IsCurrentGeneration(generation))
                        return;

                    RequestInitialThumbnails(
                        generation,
                        _thumbnailWorkCts?.Token ?? token);
                }));
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        // ============================================================
        // スクロール検知
        //
        // ListView.Scrollイベントがないため、IMessageFilterで通知を受ける。
        //
        // スクロールされたら「今見えている範囲」を最優先で要求する。
        // ============================================================
        private void ListViewThumbnails_ScrollDetected(object? sender, EventArgs e)
        {
            if (IsDisposed || !IsHandleCreated || _imageFiles.Count == 0)
                return;

            // スクロールメッセージはListViewの実際のスクロール処理より
            // スクロールメッセージは実際のスクロール処理より先に届くため、
            // 生成要求はUIキューへ回してから現在位置を取得する。
            if (Interlocked.Exchange(ref _scrollHandlingPosted, 1) != 0)
            {
                return;
            }

            try
            {
                BeginInvoke(new Action(() =>
                {
                    Interlocked.Exchange(ref _scrollHandlingPosted, 0);

                    if (IsDisposed || !IsHandleCreated || _imageFiles.Count == 0)
                    {
                        return;
                    }

                    ProcessScrollDetected();
                }));
            }
            catch
            {
                Interlocked.Exchange(ref _scrollHandlingPosted, 0);
            }
        }

        // ============================================================
        // スクロール後の実処理
        // ============================================================
        private void ProcessScrollDetected()
        {
            if (IsDisposed || !IsHandleCreated || _imageFiles.Count == 0)
            {
                return;
            }

            int generation = Volatile.Read(ref _loadGeneration);

            if (!IsCurrentGeneration(generation))
                return;

            CancellationToken token = StartNewThumbnailWork();

            int requestGeneration = Interlocked.Increment(ref _scrollRequestGeneration);

            lock (_thumbnailLock)
            {
                _thumbnailQueue.Clear();
                _thumbnailQueuedPriority.Clear();
            }

            _ = RequestCurrentVisiblePriorityAsync(
                generation,
                requestGeneration,
                token);
        }

        // ============================================================
        // 現在表示中のサムネイルを最優先で要求
        // ============================================================
        private async Task RequestCurrentVisiblePriorityAsync(int generation, int requestGeneration, CancellationToken token)
        {
            // 同じ世代の古いスクロール要求なら破棄
            if (requestGeneration != Volatile.Read(ref _scrollRequestGeneration))
            {
                return;
            }

            if (!IsCurrentGeneration(generation) || token.IsCancellationRequested)
            {
                return;
            }

            // ListViewのレイアウトが落ち着くまで少し待つ。
            await Task.Yield();

            if (requestGeneration != Volatile.Read(ref _scrollRequestGeneration))
            {
                return;
            }

            int startIndex = GetTopItemIndex();

            if (startIndex < 0)
                startIndex = 0;

            if (startIndex >= _imageFiles.Count)
                return;

            int visibleCount = GetEstimatedVisibleCount();

            int endIndex = Math.Min(_imageFiles.Count - 1, startIndex + visibleCount - 1);

            // --------------------------------------------------------
            // 現在画面に見える範囲だけを最優先
            // --------------------------------------------------------
            RequestThumbnailRange(
                startIndex,
                endIndex,
                generation,
                token,
                priority: 0);

            // --------------------------------------------------------
            // 現在表示範囲の前後を低い優先度で先読み
            //
            // 先読みは「今見えている範囲」の邪魔をしない。
            // --------------------------------------------------------

            int prefetch = Math.Max(visibleCount * 2, 32);

            int prefetchEnd =
                Math.Min(
                    _imageFiles.Count - 1,
                    startIndex +
                    prefetch -
                    1);

            if (prefetchEnd > endIndex)
            {
                RequestThumbnailRange(
                    endIndex + 1,
                    prefetchEnd,
                    generation,
                    token,
                    priority: 100);
            }
        }

        // ============================================================
        // VirtualMode
        //
        // ListViewが必要なアイテムを要求したとき
        // ============================================================
        private void ListViewThumbnails_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < 0 || e.ItemIndex >= _imageFiles.Count)
            {
                e.Item = new ListViewItem();
                return;
            }

            string filePath = _imageFiles[e.ItemIndex];
            var item = new ListViewItem(Path.GetFileName(filePath));
            item.Tag = filePath;

            // --------------------------------------------------------
            // サムネイル生成済み
            // --------------------------------------------------------
            if (_thumbnailIndexMap.TryGetValue(e.ItemIndex, out int imageIndex))
            {
                item.ImageIndex = imageIndex;
            }
            else
            {
                // ----------------------------------------------------
                // 未生成
                // ----------------------------------------------------
                item.ImageIndex = 0;
            }
            e.Item = item;
        }

        // ============================================================
        // VirtualMode
        //
        // ListViewがキャッシュを要求したとき
        // ============================================================
        private void ListViewThumbnails_CacheVirtualItems(object? sender, CacheVirtualItemsEventArgs e)
        {
            if (_imageFiles.Count == 0)
                return;

            int startIndex = Math.Max(0, e.StartIndex);
            int endIndex = Math.Min(_imageFiles.Count - 1, e.EndIndex);
            if (startIndex > endIndex)
                return;

            int generation = Volatile.Read(ref _loadGeneration);
            CancellationToken token =
                _thumbnailWorkCts?.Token ??
                _cts?.Token ??
                CancellationToken.None;

            // キャッシュ要求は、現在表示中の範囲より低い優先度。
            RequestThumbnailRange(
                startIndex, endIndex, generation, token, 20);
        }

        // ============================================================
        // 初期表示範囲
        // ============================================================
        private void RequestInitialThumbnails(int generation, CancellationToken token)
        {
            if (_imageFiles.Count == 0 || token.IsCancellationRequested || !IsCurrentGeneration(generation))
                return;

            RequestCurrentVisibleThumbnails(generation, token);

            int visibleCount = GetEstimatedVisibleCount();
            int startIndex = Math.Min(_imageFiles.Count, visibleCount);
            int prefetchEnd = Math.Min(_imageFiles.Count - 1, Math.Max((visibleCount * 2) - 1, 31));

            if (startIndex <= prefetchEnd)
            {
                _ = PrefetchAfterInitialAsync(startIndex, prefetchEnd, generation, token);
            }
        }

        // ============================================================
        // 初期先読み
        // ============================================================
        private async Task PrefetchAfterInitialAsync(int startIndex, int endIndex, int generation, CancellationToken token)
        {
            try
            {
                await Task.Delay(200, token);

                if (token.IsCancellationRequested || !IsCurrentGeneration(generation))
                    return;

                RequestThumbnailRange(startIndex, endIndex, generation, token, 100);
            }
            catch (OperationCanceledException)
            {
            }
        }

        // ============================================================
        // 現在見えている範囲
        // ============================================================
        private void RequestCurrentVisibleThumbnails(int generation, CancellationToken token)
        {
            if (_imageFiles.Count == 0 || token.IsCancellationRequested || !IsCurrentGeneration(generation))
                return;

            int startIndex = GetTopItemIndex();
            int visibleCount = GetEstimatedVisibleCount();

            int endIndex = Math.Min(_imageFiles.Count - 1, startIndex + visibleCount - 1);

            // 現在画面に見えている範囲は絶対優先。
            RequestThumbnailRange(startIndex, endIndex, generation, token, 0);

            // その先は先読み。
            int prefetchEnd = Math.Min(
                _imageFiles.Count - 1,
                startIndex + Math.Max(visibleCount * 2, 32) - 1);

            if (endIndex + 1 <= prefetchEnd)
            {
                RequestThumbnailRange(endIndex + 1, prefetchEnd, generation, token, 100);
            }
        }

        // ============================================================
        // TopItem番号取得
        // ============================================================
        private int GetTopItemIndex()
        {
            // LargeIcon / SmallIcon / Tile では ListView.TopItem が
            // サポートされていないため、TopItemプロパティは使用しない。
            // クライアント領域の左上付近にある仮想アイテムを取得して、
            // それを現在表示範囲の先頭として扱う。
            try
            {
                if (!listViewThumbnails.IsHandleCreated || _imageFiles.Count == 0)
                {
                    return 0;
                }

                ListViewItem? item = listViewThumbnails.GetItemAt(1, 1);

                if (item != null)
                {
                    return Math.Clamp(
                        item.Index,
                        0,
                        Math.Max(0, _imageFiles.Count - 1));
                }
            }
            catch (InvalidOperationException)
            {
            }
            catch (ArgumentException)
            {
            }

            return 0;
        }

        // ============================================================
        // 表示可能枚数の概算
        // ============================================================
        private int GetEstimatedVisibleCount()
        {
            int size = Math.Max(1, _imageList.ImageSize.Width);
            int itemWidth = Math.Max(size + 30, 80);
            int itemHeight = Math.Max(size + 40, 80);

            int columns = Math.Max(1, listViewThumbnails.ClientSize.Width / itemWidth);
            int rows = Math.Max(1, listViewThumbnails.ClientSize.Height / itemHeight);

            return Math.Max(1, columns * rows);
        }

        // ============================================================
        // 指定範囲のサムネイル生成を要求
        //
        // 0   = 現在表示中
        // 20  = ListViewキャッシュ
        // 100 = 先読み
        // ============================================================
        private void RequestThumbnailRange(int startIndex, int endIndex, int generation, CancellationToken token, int priority)
        {
            if (_imageFiles.Count == 0 || token.IsCancellationRequested || !IsCurrentGeneration(generation))
                return;

            startIndex = Math.Max(0, startIndex);
            endIndex = Math.Min(_imageFiles.Count - 1, endIndex);
            if (startIndex > endIndex)
                return;

            int workGeneration = Volatile.Read(ref _thumbnailWorkGeneration);

            lock (_thumbnailLock)
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (_thumbnailIndexMap.ContainsKey(i))
                        continue;

                    var key = (generation, workGeneration, i);

                    if (_thumbnailLoadingPriorities.Contains(key))
                    {
                        // すでにキューにある画像でも、より高い優先度なら
                        // 新しいキュー項目を追加する。古い項目は後で捨てる。
                        if (_thumbnailQueuedPriority.TryGetValue(
                                key, out int oldPriority) &&
                            priority < oldPriority)
                        {
                            _thumbnailQueuedPriority[key] = priority;
                            _thumbnailQueue.Enqueue(
                                new ThumbnailRequest(generation, workGeneration, i),
                                priority);
                        }

                        continue;
                    }

                    _thumbnailLoadingPriorities.Add(key);
                    _thumbnailQueuedPriority[key] = priority;
                    _thumbnailQueue.Enqueue(
                        new ThumbnailRequest(generation, workGeneration, i), priority);
                }
            }

            StartThumbnailScheduler();
        }

        // ============================================================
        // スケジューラ開始
        // ============================================================
        private void StartThumbnailScheduler()
        {
            CancellationToken token =
                _thumbnailWorkCts?.Token ??
                _cts?.Token ??
                CancellationToken.None;

            int generation = Volatile.Read(ref _loadGeneration);

            int workGeneration = Volatile.Read(ref _thumbnailWorkGeneration);

            if (token.IsCancellationRequested)
                return;

            lock (_thumbnailLock)
            {
                if (_runningSchedulerWorkGenerations.Contains(workGeneration))
                {
                    return;
                }

                _runningSchedulerWorkGenerations.Add(workGeneration);
            }

            // --------------------------------------------------------
            // 重要：ここでは「現在のWorkGeneration」と
            // 「その世代専用のToken」をローカルに保持する。
            //
            // スクロールして新しい世代が始まっても、
            // このスケジューラ自身は新しいTokenを見に行かない。
            // そのため、古いスケジューラが新しいキューを奪わない。
            // --------------------------------------------------------
            _ = ProcessThumbnailQueueAsync(generation, workGeneration, token);
        }

        // ============================================================
        // 優先度付きサムネイル生成
        //
        // 「キャンセル待ちをしない」方式。
        //
        // スクロールすると新しいWorkGenerationのスケジューラを
        // 即座に開始する。古いスケジューラは古いTokenのままなので、
        // 新しいキューには触れない。
        //
        // 古い実行中Taskは、CreateThumbnail()が同期処理のため
        // その瞬間に強制停止できない場合があるが、古いTaskの完了を
        // 新しいスケジューラが待つことはない。
        // ============================================================
        private async Task ProcessThumbnailQueueAsync(int generation, int workGeneration, CancellationToken token)
        {
            var running = new Dictionary<Task<ThumbnailData?>, ThumbnailRequest>();

            var completedBuffer = new List<ThumbnailData>();

            try
            {
                while (!token.IsCancellationRequested)
                {
                    // ----------------------------------------------------
                    // このスケジューラは起動時の世代だけを処理する。
                    // ----------------------------------------------------
                    if (!IsCurrentGeneration(generation) || workGeneration != Volatile.Read(ref _thumbnailWorkGeneration))
                    {
                        break;
                    }

                    // ----------------------------------------------------
                    // 並列数までタスクを投入
                    // ----------------------------------------------------
                    while (running.Count < GetMaxParallel() && !token.IsCancellationRequested)
                    {
                        ThumbnailRequest? request = null;
                        int priority = 0;
                        bool gotRequest = false;

                        lock (_thumbnailLock)
                        {
                            // 新しいWorkGenerationが開始されていたら、
                            // 古いスケジューラはキューに触らない。
                            if (workGeneration != Volatile.Read(ref _thumbnailWorkGeneration))
                            {
                                break;
                            }

                            while (_thumbnailQueue.Count > 0)
                            {
                                _thumbnailQueue.TryDequeue(out request, out priority);

                                if (request == null)
                                    break;

                                var key = (request.Generation, request.WorkGeneration, request.Index);

                                // 優先度が更新された古いキュー項目は捨てる。
                                if (!_thumbnailQueuedPriority.TryGetValue(
                                        key,
                                        out int currentPriority) ||
                                    currentPriority != priority)
                                {
                                    continue;
                                }

                                _thumbnailQueuedPriority.Remove(key);
                                gotRequest = true;
                                break;
                            }
                        }

                        if (!gotRequest || request == null)
                            break;

                        if (token.IsCancellationRequested ||
                            request.Generation != generation ||
                            request.WorkGeneration != workGeneration ||
                            !IsCurrentGeneration(request.Generation))
                        {
                            RemoveLoadingKey(request);
                            continue;
                        }

                        Task<ThumbnailData?> task =
                            GenerateOneThumbnailAsync(
                                request.Index,
                                request.Generation,
                                request.WorkGeneration,
                                token);

                        running[task] = request;
                    }

                    // ----------------------------------------------------
                    // 実行中Taskがない
                    // ----------------------------------------------------
                    if (running.Count == 0)
                    {
                        if (completedBuffer.Count > 0)
                        {
                            List<ThumbnailData> flush = new List<ThumbnailData>(completedBuffer);
                            completedBuffer.Clear();

                            await AddThumbnailBatchAsync(flush, generation, token);
                        }

                        lock (_thumbnailLock)
                        {
                            if (token.IsCancellationRequested ||
                                workGeneration !=
                                    Volatile.Read(
                                        ref _thumbnailWorkGeneration) ||
                                _thumbnailQueue.Count == 0)
                            {
                                return;
                            }
                        }

                        continue;
                    }

                    // ----------------------------------------------------
                    // 1つ完了するまで待つ。
                    //
                    // ここは古い世代でも待つ可能性があるが、
                    // 新しい世代のスケジューラは別Taskとして既に動く。
                    // これが「キャンセル待ちをしない」の核心。
                    // ----------------------------------------------------
                    Task<ThumbnailData?> completed = await Task.WhenAny(running.Keys);

                    ThumbnailRequest completedRequest =
                        running[completed];
                    running.Remove(completed);

                    ThumbnailData? data = null;

                    try
                    {
                        data = await completed;
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch
                    {
                    }
                    finally
                    {
                        RemoveLoadingKey(completedRequest);
                    }

                    if (data != null)
                    {
                        completedBuffer.Add(data);
                    }

                    // ----------------------------------------------------
                    // 古い世代になったら、完成済み画像もUIへ反映しない。
                    // ----------------------------------------------------
                    if (token.IsCancellationRequested ||
                        !IsCurrentGeneration(generation) ||
                        workGeneration !=
                            Volatile.Read(ref _thumbnailWorkGeneration))
                    {
                        DisposeThumbnails(completedBuffer);
                        completedBuffer.Clear();
                        break;
                    }

                    if (completedBuffer.Count >= GetMaxParallel())
                    {
                        List<ThumbnailData> flush = new List<ThumbnailData>(completedBuffer);
                        completedBuffer.Clear();
                        await AddThumbnailBatchAsync(flush, generation, token);
                    }
                }
            }
            catch
            {
                DisposeThumbnails(completedBuffer);
                completedBuffer.Clear();
            }
            finally
            {
                // --------------------------------------------------------
                // キャンセルされた古い世代の画像は反映しない。
                // 現在世代の正常終了時だけ残りを反映する。
                // --------------------------------------------------------
                if (!token.IsCancellationRequested &&
                    IsCurrentGeneration(generation) &&
                    workGeneration ==
                        Volatile.Read(ref _thumbnailWorkGeneration) &&
                    completedBuffer.Count > 0)
                {
                    try
                    {
                        List<ThumbnailData> flush = new List<ThumbnailData>(completedBuffer);
                        completedBuffer.Clear();

                        await AddThumbnailBatchAsync(flush, generation, token);
                    }
                    catch
                    {
                        DisposeThumbnails(completedBuffer);
                        completedBuffer.Clear();
                    }
                }
                else
                {
                    DisposeThumbnails(completedBuffer);
                    completedBuffer.Clear();
                }

                lock (_thumbnailLock)
                {
                    _runningSchedulerWorkGenerations.Remove(workGeneration);
                }

                // --------------------------------------------------------
                // 新しい世代のスケジューラは、スクロール処理側が
                // すでに起動しているので、ここから再起動しない。
                // --------------------------------------------------------
            }
        }

        // ============================================================
        // 生成中キーを削除
        // ============================================================
        private void RemoveLoadingKey(ThumbnailRequest request)
        {
            lock (_thumbnailLock)
            {
                _thumbnailLoadingPriorities.Remove(
                    (request.Generation,
                     request.WorkGeneration,
                     request.Index));
                _thumbnailQueuedPriority.Remove(
                    (request.Generation,
                     request.WorkGeneration,
                     request.Index));
            }
        }

        // ============================================================
        // ThumbnailRequest
        // ============================================================
        private sealed class ThumbnailRequest
        {
            public int Generation { get; }
            public int WorkGeneration { get; }
            public int Index { get; }

            public ThumbnailRequest(
                int generation,
                int workGeneration,
                int index)
            {
                Generation = generation;
                WorkGeneration = workGeneration;
                Index = index;
            }
        }

        // ============================================================
        // 1枚のサムネイル生成
        // ============================================================
        private async Task<ThumbnailData?>
            GenerateOneThumbnailAsync(
                int index,
                int generation,
                int workGeneration,
                CancellationToken token)
        {
            if (index < 0 ||
                index >= _imageFiles.Count ||
                token.IsCancellationRequested ||
                !IsCurrentGeneration(generation))
                return null;

            string filePath = _imageFiles[index];

            try
            {
                Image? thumbnail =
                    await CreateThumbnailAsync(
                        filePath,
                        _imageList.ImageSize.Width,
                        token,
                        generation);

                if (thumbnail == null)
                    return null;

                if (token.IsCancellationRequested ||
                    workGeneration !=
                        Volatile.Read(ref _thumbnailWorkGeneration))
                {
                    thumbnail.Dispose();
                    return null;
                }

                return new ThumbnailData(index, thumbnail, workGeneration);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        // ============================================================
        // サムネイル生成
        // ============================================================
        private async Task<Image?>
            CreateThumbnailAsync(
                string filePath,
                int size,
                CancellationToken token,
                int generation)
        {
            if (token.IsCancellationRequested ||
                !IsCurrentGeneration(generation))
            {
                return null;
            }


            Image? thumbnail = null;


            try
            {
                thumbnail =
                    await Task.Run(
                        () =>
                            CreateThumbnail(
                                filePath,
                                size,
                                size),
                        token);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch
            {
                return null;
            }


            if (thumbnail == null)
                return null;


            if (token.IsCancellationRequested ||
                !IsCurrentGeneration(generation))
            {
                thumbnail.Dispose();

                return null;
            }

            return thumbnail;
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

            int currentWorkGeneration = Volatile.Read(ref _thumbnailWorkGeneration);

            List<ThumbnailData> currentThumbnails =
                thumbnails
                    .Where(x =>
                        x.WorkGeneration == currentWorkGeneration)
                    .ToList();

            if (currentThumbnails.Count != thumbnails.Count)
            {
                foreach (ThumbnailData stale
                    in thumbnails.Where(x =>
                        x.WorkGeneration != currentWorkGeneration))
                {
                    try { stale.Thumbnail.Dispose(); } catch { }
                }
            }

            thumbnails = currentThumbnails;

            if (thumbnails.Count == 0)
                return;

            bool ownershipTransferred = false;

            try
            {
                await InvokeAsync(() =>
                {
                    try
                    {
                        if (IsDisposed ||
                            !IsCurrentGeneration(generation) ||
                            token.IsCancellationRequested)
                        {
                            return;
                        }

                        int firstImageIndex =
                            _imageList.Images.Count;

                        Image[] images =
                            thumbnails
                                .Select(x => x.Thumbnail)
                                .ToArray();

                        // ------------------------------------------------
                        // 重要：ListViewを更新停止した状態で
                        // ImageListとインデックスを一括更新する。
                        // ------------------------------------------------
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

                        // ------------------------------------------------
                        // 重要：EndUpdate()後に必ず再描画する。
                        //
                        // VirtualMode + ImageListでは、Invalidate()だけでは
                        // 画面にすぐ反映されない場合がある。
                        // 「グレーのまま、クリックすると画像が出る」
                        // 症状を防ぐため、バッチごとにUpdate()する。
                        // BeginUpdate/EndUpdateでバッチ単位にしているので、
                        // 1枚ごとのチラつきにはならない。
                        // ------------------------------------------------
                        listViewThumbnails.Invalidate();
                        //listViewThumbnails.Update();
                        int processed = _thumbnailIndexMap.Count;

                        StatusLabel1.Text = $"サムネイル読み込み中... " + $"{processed:N0} / " + $"{_imageFiles.Count:N0} 枚";

                    }
                    catch (ObjectDisposedException)
                    {
                        // 終了処理中などにコントロールが破棄された場合は無視する。
                    }
                });
            }
            catch
            {
                // フォーム終了などによる反映失敗は無視する。
            }
            finally
            {
                if (!ownershipTransferred)
                    DisposeThumbnails(thumbnails);
            }
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

            // この画像がどのスクロール世代で生成されたか。
            // 古い世代の完成画像はUIへ反映しない。
            public int WorkGeneration { get; }


            public ThumbnailData(int index, Image thumbnail, int workGeneration)
            {
                Index = index;
                Thumbnail = thumbnail;
                WorkGeneration = workGeneration;
            }
        }

        // ============================================================
        // サムネイル破棄
        // ============================================================
        private void DisposeThumbnails(IEnumerable<ThumbnailData> thumbnails)
        {
            foreach (ThumbnailData data
                in thumbnails)
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
        // ============================================================
        private Image? CreateThumbnail(string filePath, int width, int height)
        {
            try
            {
                using (var stream =
                    new FileStream(
                        filePath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read))
                using (var original =
                    Image.FromStream(
                        stream,
                        false,
                        false))
                {
                    var bitmap = new Bitmap(width, height);

                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        float scale = Math.Min((float)width / original.Width, (float)height / original.Height);
                        int drawWidth = (int)(original.Width * scale);
                        int drawHeight = (int)(original.Height * scale);
                        int x = (width - drawWidth) / 2;
                        int y = (height - drawHeight) / 2;
                        g.Clear(Color.White);
                        g.DrawImage(
                            original,
                            new Rectangle(
                                x,
                                y,
                                drawWidth,
                                drawHeight));
                    }

                    return bitmap;
                }
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

            lock (_thumbnailLock)
            {
                _thumbnailQueue.Clear();
                _thumbnailQueuedPriority.Clear();
            }

            _lastThumbnailInvalidateTick = 0;
            listViewThumbnails.BeginUpdate();
            try
            {
                listViewThumbnails.VirtualListSize = 0;
                listViewThumbnails.Invalidate();
                _imageFiles.Clear();

                // 現在の ImageList の設定を保存
                Size imageSize = _imageList.ImageSize;
                ColorDepth colorDepth = _imageList.ColorDepth;

                // ListView から古い ImageList を外す
                listViewThumbnails.LargeImageList = null;

                // 古い ImageList を丸ごと破棄
                ImageList oldImageList = _imageList;
                oldImageList.Dispose();

                // 新しい ImageList を作る
                _imageList = new ImageList
                {
                    ImageSize = imageSize,
                    ColorDepth = colorDepth
                };

                // プレースホルダーを作る
                _placeholderImage = CreatePlaceholder(
                    imageSize.Width,
                    imageSize.Height);

                _imageList.Images.Add("placeholder", _placeholderImage);

                // 新しい ImageList を ListView に設定
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

            // --------------------------------------------------------
            // 現在の読み込みをキャンセル
            // --------------------------------------------------------
            _cts?.Cancel();

            // --------------------------------------------------------
            // ImageListサイズ変更
            //
            // サイズを先に変更してからClearImages()を呼ぶことで、
            // 新しいサイズのプレースホルダーを1個だけ作る。
            // --------------------------------------------------------
            _imageList.ImageSize = new Size(newSize, newSize);
            ClearImages();

            // --------------------------------------------------------
            // 現在のフォルダを再読み込み
            // --------------------------------------------------------
            string? selectedPath = treeViewFolders.SelectedNode?.Tag?.ToString();

            if (string.IsNullOrEmpty(selectedPath))
                return;

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

            // 指定されたパスまでTreeViewを展開して探す
            TreeNode? node =
                FindAndExpandTreeNode(path);

            if (node == null)
            {
                MessageBox.Show(
                    "指定されたフォルダーをTreeViewで表示できません。",
                    "フォルダーが見つかりません",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // 該当フォルダーを選択状態にする
            treeViewFolders.SelectedNode = node;

            // TreeView内で見える位置までスクロール
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

            // --------------------------------------------------------
            // ドライブ名を取得
            // 例：C:\
            // --------------------------------------------------------
            string root = Path.GetPathRoot(fullPath) ?? "";

            if (string.IsNullOrEmpty(root))
                return null;

            TreeNode? currentNode = null;

            // --------------------------------------------------------
            // ドライブノードを探す
            // --------------------------------------------------------
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

            // ルートそのものなら終了
            if (string.Equals(
                fullPath.TrimEnd('\\'),
                root.TrimEnd('\\'),
                StringComparison.OrdinalIgnoreCase))
            {
                return currentNode;
            }

            // --------------------------------------------------------
            // C:\Users\xxx\Pictures...
            // の残りのパスを順番にたどる
            // --------------------------------------------------------
            string relativePath = fullPath.Substring(root.Length);

            string[] parts =
                relativePath.Split(
                    Path.DirectorySeparatorChar,
                    StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                // 現在のノードを展開
                currentNode.Expand();

                TreeNode? nextNode = null;

                foreach (TreeNode child in currentNode.Nodes)
                {
                    string? childPath =
                        child.Tag?.ToString();

                    if (string.Equals(
                        childPath,
                        Path.Combine(
                            currentNode.Tag?.ToString() ?? "",
                            part),
                        StringComparison.OrdinalIgnoreCase))
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
        // TreeViewから指定されたパスのノードを探す
        // ============================================================
        private TreeNode? FindTreeNodeByPath(TreeNodeCollection nodes, string targetPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (string.Equals(
                    node.Tag?.ToString(),
                    targetPath,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }

                TreeNode? result = FindTreeNodeByPath(node.Nodes, targetPath);

                if (result != null)
                    return result;
            }

            return null;
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

            // キー入力による「ボヨン」というシステム音を抑止
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
                return;

            int selectedIndex = listViewThumbnails.SelectedIndices[0];

            if (selectedIndex < 0 || selectedIndex >= _imageFiles.Count)
                return;

            string fullPath = _imageFiles[selectedIndex];

            if (string.IsNullOrEmpty(fullPath))
                return;

            try
            {
                switch (number)
                {
                    case 1:
                        // フルパス
                        Clipboard.SetText(fullPath);

                        logTxt.Text =
                            "フルパスをコピーしました:" +
                            Environment.NewLine +
                            fullPath;
                        break;


                    case 2:
                        // ファイル名
                        string fileName = Path.GetFileName(fullPath);

                        Clipboard.SetText(fileName);

                        logTxt.Text =
                            "ファイル名をコピーしました:" +
                            Environment.NewLine +
                            fileName;
                        break;


                    case 3:
                        // フォルダーパス
                        string folderPath =
                            Path.GetDirectoryName(fullPath) ?? "";

                        Clipboard.SetText(folderPath);

                        logTxt.Text =
                            "フォルダーパスをコピーしました:" +
                            Environment.NewLine +
                            folderPath;
                        break;


                    case 4:
                        // 画像
                        using (Image sourceImage = Image.FromFile(fullPath))
                        {
                            // Image.FromFile の元画像をそのまま
                            // クリップボードへ渡さない
                            // （ファイルロックを避けるため Bitmap を作成）
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


                    case 5:
                        // 拡張子
                        string extension =
                            Path.GetExtension(fullPath);

                        Clipboard.SetText(extension);

                        logTxt.Text =
                            "拡張子をコピーしました:" +
                            Environment.NewLine +
                            extension;
                        break;


                    case 6:
                        // ファイルサイズ
                        FileInfo fileInfo = new FileInfo(fullPath);

                        string fileSize =
                            FormatFileSize(fileInfo.Length);

                        Clipboard.SetText(fileSize);

                        logTxt.Text =
                            "ファイルサイズをコピーしました:" +
                            Environment.NewLine +
                            fileSize;
                        break;


                    case 7:
                        // 画像サイズ
                        using (Image image = Image.FromFile(fullPath))
                        {
                            string imageSize =
                                $"{image.Width} × {image.Height}";

                            Clipboard.SetText(imageSize);

                            logTxt.Text =
                                "画像サイズをコピーしました:" +
                                Environment.NewLine +
                                imageSize;
                        }
                        break;


                    case 8:
                        // 更新日時
                        FileInfo updateInfo = new FileInfo(fullPath);

                        string updateDate =
                            updateInfo.LastWriteTime
                                .ToString("yyyy/MM/dd HH:mm:ss");

                        Clipboard.SetText(updateDate);

                        logTxt.Text =
                            "更新日時をコピーしました:" +
                            Environment.NewLine +
                            updateDate;
                        break;


                    case 9:
                        // 作成日時
                        FileInfo createInfo = new FileInfo(fullPath);

                        string createDate =
                            createInfo.CreationTime
                                .ToString("yyyy/MM/dd HH:mm:ss");

                        Clipboard.SetText(createDate);

                        logTxt.Text =
                            "作成日時をコピーしました:" +
                            Environment.NewLine +
                            createDate;
                        break;
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
        // ファイルサイズ取得(6キーを押したとき)
        // ============================================================
        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes:N0} bytes";

            if (bytes < 1024 * 1024)
                return $"{bytes / 1024.0:N2} KB";

            if (bytes < 1024L * 1024L * 1024L)
                return $"{bytes / (1024.0 * 1024.0):N2} MB";

            return $"{bytes / (1024.0 * 1024.0 * 1024.0):N2} GB";
        }

        // ============================================================
        // サムネイルのマウス押下
        // ============================================================
        private void ListViewThumbnails_MouseDown(object? sender, MouseEventArgs e)
        {
            // 右クリック
            if (e.Button == MouseButtons.Right)
            {
                // 右クリックした場所の画像を取得
                ListViewItem? item = listViewThumbnails.GetItemAt(e.X, e.Y);
                // 画像がない場所なら何もしない
                if (item == null)
                {
                    _dragIndex = -1;
                    return;
                }

                // 右クリックした画像を選択状態にする
                item.Selected = true;
                item.Focused = true;

                return;
            }

            // 左クリック → ドラッグ開始用
            if (e.Button != MouseButtons.Left)
            {
                _dragIndex = -1;
                return;
            }

            _dragStartPoint = e.Location;

            // VirtualModeでも、マウス位置から実際に押した画像を取得する
            ListViewItem? dragitem = listViewThumbnails.GetItemAt(e.X, e.Y);

            _dragIndex = dragitem?.Index ?? -1;
        }


        // ============================================================
        // サムネイルのドラッグ実行
        // フルパスを取得できる
        // ============================================================

        private void ListViewThumbnails_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_dragIndex < 0 ||
                _dragIndex >= _imageFiles.Count)
            {
                return;
            }

            Size dragSize = SystemInformation.DragSize;

            Rectangle dragRect =
                new Rectangle(
                    _dragStartPoint.X - dragSize.Width / 2,
                    _dragStartPoint.Y - dragSize.Height / 2,
                    dragSize.Width,
                    dragSize.Height);

            // 少しマウスを動かしただけではドラッグ開始しない
            if (dragRect.Contains(e.Location))
                return;

            string fullPath = _imageFiles[_dragIndex];

            if (string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
            {
                return;
            }

            try
            {
                var data = new DataObject();

                data.SetData(DataFormats.FileDrop, new string[]
                    {
                        fullPath
                    });

                // Excel側には「ファイルをコピーする」ドラッグとして渡す
                listViewThumbnails.DoDragDrop(data, DragDropEffects.Copy);
            }
            catch (InvalidOperationException)
            {
                // ドラッグ開始時の状態による例外は無視
            }
            finally
            {
                _dragIndex = -1;
            }
        }

        // ============================================================
        // コンテキストメニュー表示前
        // 画像の上で右クリックした場合だけメニューを表示する
        // ============================================================
        private void ConMenu1_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Point point = listViewThumbnails.PointToClient(Cursor.Position);

            ListViewItem? item = listViewThumbnails.GetItemAt(point.X, point.Y);

            // 画像がない場所ならメニューを表示しない
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

        // ============================================================
        // 1 = フルパス
        // ============================================================
        private void ConTxtMenu1_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(1);
        }

        // ============================================================
        // 2 = ファイル名
        // ============================================================
        private void ConTxtMenu2_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(2);
        }

        // ============================================================
        // 3 = パス
        // ============================================================
        private void ConTxtMenu3_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(3);
        }

        // ============================================================
        // 4 = 画像そのもの
        // ============================================================
        private void ConTxtMenu4_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(4);
        }

        // ============================================================
        // 5 = 拡張子
        // ============================================================
        private void ConTxtMenu5_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(5);
        }

        // ============================================================
        // 6 = ファイルサイズ
        // ============================================================
        private void ConTxtMenu6_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(6);
        }

        // ============================================================
        // 7 = 画像サイズ
        // ============================================================
        private void ConTxtMenu7_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(7);
        }

        // ============================================================
        // 8 = 更新日時
        // ============================================================
        private void ConTxtMenu8_Click(object? sender, EventArgs e)
        {
            CopyImageInfo(8);
        }

        // ============================================================
        // 9 = 作成日時
        // ============================================================
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
                return;

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

            // Form3がすでに開いているか
            if (_previewForm == null || _previewForm.IsDisposed)
            {
                // まだ開いていない
                _previewForm = new Form3(fullPath);

                _previewForm.FormClosed +=
                    PreviewForm_FormClosed;

                _previewForm.Show(this);
            }
            else
            {
                // すでに開いているので画像だけ変更
                _previewForm.SetImage(fullPath);

                // Form3を前面へ
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

        // ============================================================
        // スクロール検知用メッセージフィルターを解除
        // ============================================================

        private void DetachScrollMessageFilter()
        {
            try
            {
                if (_scrollMessageFilter != null)
                {
                    Application.RemoveMessageFilter(_scrollMessageFilter);

                    _scrollMessageFilter = null;
                }
            }
            catch
            {
                // 終了時の例外は無視
            }
        }

        // ============================================================
        // 終了時の各種破棄処理
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
                    //Directory.CreateDirectory(Application.LocalUserAppDataPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(LastFolderFilePath)!);

                    File.WriteAllText(LastFolderFilePath, selectedPath);
                }
            }
            catch
            {
                // 終了時の保存失敗は無視
            }

            // ============================================================
            // 終了時のImageList破棄
            // ============================================================
            DetachScrollMessageFilter();

            _cts?.Cancel();
            _thumbnailWorkCts?.Cancel();

            _cts?.Dispose();
            _thumbnailWorkCts?.Dispose();

            // ImageList を ListView から外す
            listViewThumbnails.LargeImageList = null;

            // ImageList ごと破棄する
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
        // バージョン情報を表示
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
            // 全選択
            pathTxt.SelectAll();
        }

        // ============================================================
        // 設定ファイル保存フォルダをエクスプローラで開く
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
        // フォルダーツリーを更新の処理
        // ============================================================
        private void RefreshFolderTree()
        {
            // 現在選択しているフォルダーのパスを保存
            string? currentPath =
                treeViewFolders.SelectedNode?.Tag?.ToString();

            // フォルダーツリーを再構築
            LoadDriveNodes();

            // 現在のフォルダーを復元
            if (!string.IsNullOrEmpty(currentPath))
            {
                TreeNode? restoredNode =
                    FindAndExpandTreeNode(currentPath);

                // フォルダーがまだ存在している
                if (restoredNode != null)
                {
                    treeViewFolders.SelectedNode = restoredNode;
                    restoredNode.EnsureVisible();

                    // 現在フォルダーの画像一覧も更新
                    pathTxt.Text = currentPath;
                    _ = LoadFolderAsync(currentPath);

                    return;
                }

                // ----------------------------------------------------
                // フォルダーが削除・移動されていた場合
                // 親フォルダーへ移動する
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

                            // pathTxt とサムネイルも親フォルダーに合わせる
                            pathTxt.Text = parentPath;
                            _ = LoadFolderAsync(parentPath);

                            return;
                        }
                    }
                }
                catch
                {
                    // 親フォルダーの取得に失敗した場合は何もしない
                }
            }
        }

        // ============================================================
        // フォルダー更新を押したとき
        // ============================================================
        private void folderUpdate_Click(object sender, EventArgs e)
        {
            RefreshFolderTree();
        }

        // ============================================================
        // 使い方を押したとき
        // ============================================================
        private void useMenu_Click(object sender, EventArgs e)
        {
            try
            {
                // 既定のPDFアプリで開く(Acrobat Reader とか)
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "使い方.pdf",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
#if DEBUG
                Extxt.Text = ex.Message;
                MessageBox.Show("外部アプリで開けませんでした。\n" + ex.Message, "外部アプリオープン失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#else
                MessageBox.Show("外部アプリで開けませんでした。", "外部アプリオープン失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif

            }
        }

    }
}