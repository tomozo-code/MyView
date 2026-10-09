using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// --------------------------------------------------------
// 画像ビューフォーム
// --------------------------------------------------------

// サムネイルをダブルクリックで表示される
// 左ドラッグで枠描画し、枠内クリックで拡大
// 描画した枠は、8点をドラッグすることで調整
// 枠内の画像はコピー可能
// ctrl + 上スクロールで拡大、ctrl + 下スクロールで縮小
// 右ドラッグで画像内移動
// 上スクロールで前の画像、下スクロールで次の画像
// 画像に名前を付けて保存
// クリップボードの画像を貼り付け

namespace MyView
{
    public partial class Form3 : Form
    {
        // 画像パス
        private readonly string _imagePath;

        // 現在表示しているフォルダ内の画像一覧
        private string[] _imageFiles = Array.Empty<string>();

        // 現在表示している画像の番号
        private int _currentImageIndex = -1;

        // 表示する画像
        // PictureBox.Imageには設定しない。
        // Paintイベントで自分で描画する。
        private Image? _image;

        // クリップボードから貼り付けた画像かどうか
        private bool _isClipboardImage = false;

        // ズーム倍率
        private float _zoom = 1.0f;

        // ズーム倍率の最小・最大
        private const float MinZoom = 0.1f;
        private const float MaxZoom = 10.0f;

        // 1回のホイール操作で変化する倍率
        private const float ZoomStep = 1.2f;

        // 画像の左上位置
        //
        // pictureBox1 の左上を (0, 0) とした座標。
        // 現段階では中央表示を基本とする。
        // 次の段階で右ドラッグ移動に使用する。
        private PointF _imageOffset;

        // 右ドラッグによる画像移動中かどうか
        private bool _isPanning = false;

        // 右ドラッグを開始したときのマウス位置
        private Point _panStartMouse;

        // 右ドラッグ開始時の画像位置
        private PointF _panStartOffset;


        // 左ドラッグ範囲選択中かどうか
        private bool _isSelecting = false;

        // 左ドラッグ範囲選択を開始した位置
        private Point _selectionStart;

        // 左ドラッグ現在の選択範囲
        private Rectangle _selectionRectangle = Rectangle.Empty;

        // 左クリックが「選択範囲のクリック」かどうか
        private bool _selectionClickCandidate = false;

        // 左クリック開始位置
        private Point _selectionClickStart;

        // 選択範囲のサイズ変更
        private enum SelectionHandle
        {
            None,

            TopLeft,
            Top,
            TopRight,

            Left,
            Right,

            BottomLeft,
            Bottom,
            BottomRight
        }

        // 現在ドラッグしているハンドル
        private SelectionHandle _selectionHandle = SelectionHandle.None;

        // 選択範囲をサイズ変更中かどうか
        private bool _isResizingSelection = false;

        // サイズ変更開始時の選択範囲
        private Rectangle _selectionResizeStartRectangle;

        // サイズ変更開始時のマウス位置
        private Point _selectionResizeStartMouse;

        // ハンドルの大きさ
        private const int SelectionHandleSize = 10;

        // 選択範囲の最小サイズ
        private const int SelectionMinSize = 5;


        // 表示画像が変更されたことをForm1へ通知
        public event Action<string>? ImageChanged;

        // ============================================================
        // コンストラクタ
        // ============================================================
        public Form3(string imagePath)
        {
            InitializeComponent();

            // Form3でKeyDownを拾えるように
            this.KeyPreview = true;

            this.Width = 800;
            this.Height = 700;
            this.MinimumSize = new Size(300, 300);

            this.WindowState = FormWindowState.Maximized;

            toolStripContainer1.Dock = DockStyle.Fill;

            panel1.Dock = DockStyle.Fill;

            _imagePath = imagePath;

            // 同じフォルダにある画像を取得
            LoadImageFileList(imagePath);

            pictureBox1.Dock = DockStyle.Fill;

            // 自前で画像を描画するため、SizeModeはNormalにする
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;

            // 背景色
            pictureBox1.BackColor = Color.DimGray;

            // ダブルバッファリングでちらつきを抑える
            typeof(Control)
                .GetProperty(
                    "DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(pictureBox1, true);

            // マウスホイールイベント
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;

            // 画像表示
            SetImage(imagePath);

        }

        // ============================================================
        // フォームをロードしたとき
        // ============================================================
        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(_imagePath))
                    return;

                // 画像を表示
                // GIFアニメーションにも対応
                SetImage(_imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像読み込みエラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 同じフォルダの画像一覧を取得
        // ============================================================
        private void LoadImageFileList(string imagePath)
        {
            string? folder = Path.GetDirectoryName(imagePath);

            if (string.IsNullOrEmpty(folder))
                return;

            string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff", ".ico", ".wmf", ".emf" };

            _imageFiles = Directory
                .GetFiles(folder)
                .Where(file =>
                    extensions.Contains(
                        Path.GetExtension(file),
                        StringComparer.OrdinalIgnoreCase))
                //.OrderBy(file => Path.GetFileName(file), StringComparer.OrdinalIgnoreCase)
                .OrderBy(file => Path.GetFileName(file), new NaturalStringComparer())
                .ToArray();

            // 現在の画像の位置を探す
            _currentImageIndex = Array.FindIndex(
                _imageFiles,
                file => string.Equals(
                    file,
                    imagePath,
                    StringComparison.OrdinalIgnoreCase));
        }

        // ============================================================
        // 画像を表示
        // ============================================================
        public void SetImage(string imagePath)
        {
            if (!File.Exists(imagePath))
                return;

            try
            {
                // ----------------------------------------------------
                // 表示する画像が別フォルダの場合、
                // 画像一覧もそのフォルダのものに更新する
                // ----------------------------------------------------
                string? newFolder = Path.GetDirectoryName(imagePath);

                bool folderChanged =
                    _imageFiles.Length == 0 ||
                    string.IsNullOrEmpty(newFolder) ||
                    !string.Equals(
                        Path.GetDirectoryName(_imageFiles[0]),
                        newFolder,
                        StringComparison.OrdinalIgnoreCase);

                if (folderChanged)
                {
                    LoadImageFileList(imagePath);
                }

                // 現在表示する画像の位置を更新
                _currentImageIndex = Array.FindIndex(
                    _imageFiles,
                    file => string.Equals(
                        file,
                        imagePath,
                        StringComparison.OrdinalIgnoreCase));

                // 現在表示している画像のアニメーションを停止
                if (_image != null)
                {
                    Image oldImage = _image;

                    if (ImageAnimator.CanAnimate(oldImage))
                    {
                        ImageAnimator.StopAnimate(oldImage, PictureBoxAnimationHandler);
                    }

                    _image = null;
                    // 古い画像を解放
                    oldImage.Dispose();
                }

                // ----------------------------------------------------
                // 新しい画像を読み込む
                //
                // GIFの場合は元のImageをそのまま使用する。
                // BitmapへコピーするとGIFアニメーションが失われるため。
                // ----------------------------------------------------
                Image image = Image.FromFile(imagePath);

                _image = image;

                // フォルダ内の画像を表示している場合、名前を付けて保存を無効にする場合は「false」
                //_isClipboardImage = false;
                _isClipboardImage = true;


                // ----------------------------------------------------
                // GIFアニメーションの場合
                // ----------------------------------------------------
                if (ImageAnimator.CanAnimate(image))
                {
                    ImageAnimator.Animate(image, PictureBoxAnimationHandler);
                }

                // 初期ズーム
                //_zoom = 1.0f;

                // フォーム内に収まるように自動調整
                FitImageToWindow();

                // 画像を中央に配置
                CenterImage();

                // タイトルをファイル名にする
                Text = Path.GetFileName(imagePath);
                // ステータスバーにフルパス表示
                viewStatusTxt.Text = imagePath;

                // 再描画
                pictureBox1.Invalidate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像読み込みエラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 画像を中央に配置
        // ============================================================
        private void CenterImage()
        {
            if (_image == null)
                return;

            float imageWidth = _image.Width * _zoom;

            float imageHeight = _image.Height * _zoom;

            float x = (pictureBox1.ClientSize.Width - imageWidth) / 2.0f;

            float y = (pictureBox1.ClientSize.Height - imageHeight) / 2.0f;

            _imageOffset = new PointF(x, y);
        }

        // ============================================================
        // 画像をフォーム内に収まるように自動調整
        // ============================================================
        private void FitImageToWindow()
        {
            if (_image == null)
                return;

            int viewWidth = pictureBox1.ClientSize.Width;
            int viewHeight = pictureBox1.ClientSize.Height;

            // 表示領域がまだ確定していない場合
            if (viewWidth <= 0 || viewHeight <= 0)
                return;

            // 横方向・縦方向、それぞれの倍率を計算
            float zoomX = viewWidth / (float)_image.Width;
            float zoomY = viewHeight / (float)_image.Height;

            // 小さいほうの倍率を採用
            // → 画像全体が画面内に収まる
            float newZoom = Math.Min(zoomX, zoomY);

            // 最小・最大倍率の範囲に収める
            newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);

            _zoom = newZoom;

            // 中央に配置
            CenterImage();

            // 再描画
            pictureBox1.Invalidate();
        }

        // ============================================================
        // マウスホイール
        //
        // Ctrl + ホイール → ズーム
        // 通常のホイール → 前後の画像へ移動
        // ============================================================
        private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (_image == null)
                return;

            // Ctrl + ホイール
            if ((ModifierKeys & Keys.Control) != 0)
            {
                // マウス位置
                PointF mousePosition = e.Location;

                // 現在の倍率
                float oldZoom = _zoom;

                // 新しい倍率
                float newZoom;

                if (e.Delta > 0)
                {
                    // 拡大
                    newZoom = oldZoom * ZoomStep;
                }
                else if (e.Delta < 0)
                {
                    // 縮小
                    newZoom = oldZoom / ZoomStep;
                }
                else
                {
                    return;
                }

                // 倍率を制限
                newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);

                // 変化しなかった場合
                if (Math.Abs(newZoom - oldZoom) < 0.0001f)
                    return;

                // マウスカーソル位置を基準にズーム
                float imageX = (mousePosition.X - _imageOffset.X) / oldZoom;
                float imageY = (mousePosition.Y - _imageOffset.Y) / oldZoom;
                _zoom = newZoom;
                _imageOffset.X = mousePosition.X - imageX * newZoom;
                _imageOffset.Y = mousePosition.Y - imageY * newZoom;
                pictureBox1.Invalidate();
                return;
            }

            // 通常のホイール
            // → 前後の画像へ移動
            if (e.Delta > 0)
            {
                // 上スクロール → 前の画像
                ShowPreviousImage();
            }
            else if (e.Delta < 0)
            {
                // 下スクロール → 次の画像
                ShowNextImage();
            }
        }

        // ============================================================
        // 前の画像を表示
        // ============================================================
        private void ShowPreviousImage()
        {
            if (_imageFiles.Length == 0)
                return;

            if (_currentImageIndex <= 0)
                return;

            _currentImageIndex--;

            string imagePath = _imageFiles[_currentImageIndex];

            SetImage(imagePath);

            // Form1へ通知
            ImageChanged?.Invoke(imagePath);

        }

        // ============================================================
        // 次の画像を表示
        // ============================================================
        private void ShowNextImage()
        {
            if (_imageFiles.Length == 0)
                return;

            if (_currentImageIndex < 0)
                return;

            if (_currentImageIndex >= _imageFiles.Length - 1)
                return;

            _currentImageIndex++;

            string imagePath = _imageFiles[_currentImageIndex];

            SetImage(imagePath);

            // Form1へ通知
            ImageChanged?.Invoke(imagePath);
        }

        // ============================================================
        // PictureBoxの描画
        // ============================================================
        private void PictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            if (_image == null)
                return;

            Graphics g = e.Graphics;

            // 背景
            //g.Clear(Color.Black);

            // GIFアニメーションのフレームを更新
            if (ImageAnimator.CanAnimate(_image))
            {
                ImageAnimator.UpdateFrames(_image);
            }

            // 高品質な画像表示
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            // 拡大・縮小後の画像サイズ
            float width = _image.Width * _zoom;
            float height = _image.Height * _zoom;
            RectangleF destRect = new RectangleF(
                _imageOffset.X,
                _imageOffset.Y,
                width,
                height);

            // 画像を描画
            g.DrawImage(_image, destRect);

            // 左ドラッグの選択範囲を描画
            if (!_selectionRectangle.IsEmpty)
            {
                // 黒線を下に描く
                // → 白い画像でも見える
                using (Pen blackPen = new Pen(Color.Black, 6))
                {
                    blackPen.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(blackPen, _selectionRectangle);
                }

                // 白線を上に描く
                // → 黒い画像でも見える
                using (Pen whitePen = new Pen(Color.White, 2))
                {
                    whitePen.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(whitePen, _selectionRectangle);
                }

                // 選択範囲のサイズ変更ハンドル
                DrawSelectionHandle(g, _selectionRectangle.Left, _selectionRectangle.Top);
                DrawSelectionHandle(g, _selectionRectangle.Left + _selectionRectangle.Width / 2, _selectionRectangle.Top);

                DrawSelectionHandle(g, _selectionRectangle.Right, _selectionRectangle.Top);

                DrawSelectionHandle(g, _selectionRectangle.Left, _selectionRectangle.Top + _selectionRectangle.Height / 2);

                DrawSelectionHandle(g, _selectionRectangle.Right, _selectionRectangle.Top + _selectionRectangle.Height / 2);

                DrawSelectionHandle(g, _selectionRectangle.Left, _selectionRectangle.Bottom);

                DrawSelectionHandle(g, _selectionRectangle.Left + _selectionRectangle.Width / 2, _selectionRectangle.Bottom);

                DrawSelectionHandle(g, _selectionRectangle.Right, _selectionRectangle.Bottom);

            }

        }

        // ============================================================
        // 選択範囲のハンドルを描画
        // ============================================================
        private void DrawSelectionHandle(Graphics g, int x, int y)
        {
            int half = SelectionHandleSize / 2;

            Rectangle handleRectangle = new Rectangle(
                x - half,
                y - half,
                SelectionHandleSize,
                SelectionHandleSize);

            // 黒い外枠
            using (Brush blackBrush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(blackBrush, handleRectangle);
            }

            // 白い内側
            int innerSize = SelectionHandleSize - 4;

            Rectangle innerRectangle = new Rectangle(
                x - innerSize / 2,
                y - innerSize / 2,
                innerSize,
                innerSize);

            using (Brush whiteBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(whiteBrush, innerRectangle);
            }
        }

        // ============================================================
        // PictureBoxのサイズが変わったとき
        // ============================================================
        private void PictureBox1_Resize(object? sender, EventArgs e)
        {
            // まだ画像がない場合
            if (_image == null)
                return;

            // ズーム倍率は変更しない。
            // 現在の表示サイズをそのまま維持して、
            // 新しいPictureBoxの中央へ画像を移動する。
            CenterImage();

            // 再描画
            pictureBox1.Invalidate();
        }

        // ============================================================
        // GIFアニメーション更新
        // ============================================================
        private void PictureBoxAnimationHandler(object? sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        // ============================================================
        // フォーム終了時
        // ============================================================
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // GIFアニメーションを停止
            if (_image != null)
            {
                Image image = _image;

                if (ImageAnimator.CanAnimate(image))
                {
                    ImageAnimator.StopAnimate(image, PictureBoxAnimationHandler);
                }

                _image = null;

                image.Dispose();
            }

            base.OnFormClosed(e);
        }

        // ============================================================
        // マウスボタンを押したとき
        // ============================================================
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // 右ボタン → 画像移動
            if (e.Button == MouseButtons.Right)
            {
                _isPanning = true;

                // ドラッグ開始時のマウス位置
                _panStartMouse = e.Location;

                // ドラッグ開始時の画像位置
                _panStartOffset = _imageOffset;

                // カーソルを手の形にする
                pictureBox1.Cursor = Cursors.Hand;

                return;
            }

            // 左ボタン
            if (e.Button == MouseButtons.Left)
            {
                // すでに選択範囲がある場合
                if (!_selectionRectangle.IsEmpty)
                {
                    // ハンドルをクリックしたか
                    SelectionHandle handle =
                        GetSelectionHandle(e.Location);

                    if (handle != SelectionHandle.None)
                    {
                        // サイズ変更開始
                        _isResizingSelection = true;
                        _selectionHandle = handle;

                        // 開始時の状態を保存
                        _selectionResizeStartRectangle = _selectionRectangle;

                        _selectionResizeStartMouse = e.Location;

                        pictureBox1.Cursor = GetSelectionCursor(handle);

                        return;
                    }

                    // 選択範囲の中をクリック → 従来通り「拡大」
                    if (_selectionRectangle.Contains(e.Location))
                    {
                        _selectionClickCandidate = true;
                        _selectionClickStart = e.Location;

                        return;
                    }
                }

                // 新しい範囲選択を開始
                _isSelecting = true;
                _selectionStart = e.Location;
                _selectionRectangle = Rectangle.Empty;

                pictureBox1.Cursor = Cursors.Cross;

                pictureBox1.Invalidate();
            }
        }

        // ============================================================
        // マウスを移動したとき
        // ============================================================
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // 右ドラッグ → 画像移動
            if (_isPanning)
            {
                // ドラッグ開始位置からの移動量
                float dx = e.X - _panStartMouse.X;
                float dy = e.Y - _panStartMouse.Y;

                // 画像位置を移動
                _imageOffset = new PointF(_panStartOffset.X + dx, _panStartOffset.Y + dy);
                pictureBox1.Invalidate();
                return;
            }

            // 選択範囲のサイズ変更
            if (_isResizingSelection)
            {
                ResizeSelection(e.Location);

                pictureBox1.Invalidate();

                return;
            }

            // --------------------------------------------------------
            // 選択範囲内をクリックした後に
            // マウスを動かした場合
            // → 「クリック」ではなく「新しい範囲選択」に変更する
            // --------------------------------------------------------
            if (_selectionClickCandidate)
            {
                int dx = Math.Abs(e.X - _selectionClickStart.X);
                int dy = Math.Abs(e.Y - _selectionClickStart.Y);

                // 少しでもドラッグしたら新しい選択を開始
                if (dx > 3 || dy > 3)
                {
                    _selectionClickCandidate = false;
                    _isSelecting = true;
                    _selectionStart = _selectionClickStart;
                    _selectionRectangle = Rectangle.Empty;
                    pictureBox1.Cursor = Cursors.Cross;
                }
            }

            // 左ドラッグ → 範囲選択
            if (_isSelecting)
            {
                int x = Math.Min(_selectionStart.X, e.X);
                int y = Math.Min(_selectionStart.Y, e.Y);

                int width = Math.Abs(e.X - _selectionStart.X);
                int height = Math.Abs(e.Y - _selectionStart.Y);

                _selectionRectangle = new Rectangle(x, y, width, height);

                pictureBox1.Invalidate();
            }

            // 選択範囲のハンドル上ではカーソルを変更
            if (!_isSelecting &&
                !_selectionClickCandidate &&
                !_isResizingSelection &&
                !_selectionRectangle.IsEmpty)
            {
                SelectionHandle handle = GetSelectionHandle(e.Location);

                pictureBox1.Cursor = GetSelectionCursor(handle);
            }

        }

        // ============================================================
        // マウスボタンを離したとき
        // ============================================================
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // 右ボタン → 画像移動終了
            if (e.Button == MouseButtons.Right)
            {
                _isPanning = false;
                pictureBox1.Cursor = Cursors.Default;
                return;
            }

            // 左ボタン
            if (e.Button == MouseButtons.Left)
            {

                // 選択範囲のサイズ変更終了
                if (_isResizingSelection)
                {
                    _isResizingSelection = false;
                    _selectionHandle = SelectionHandle.None;

                    pictureBox1.Cursor = Cursors.Default;
                    pictureBox1.Invalidate();

                    return;
                }


                // 選択範囲内をクリックした
                if (_selectionClickCandidate)
                {
                    _selectionClickCandidate = false;

                    // 選択範囲を拡大
                    ZoomToSelection();

                    pictureBox1.Cursor = Cursors.Default;

                    return;
                }

                // 範囲選択終了
                if (_isSelecting)
                {
                    _isSelecting = false;
                    pictureBox1.Cursor = Cursors.Default;
                    pictureBox1.Invalidate();
                }
            }
        }

        // ============================================================
        // 選択範囲を画面いっぱいに拡大
        // ============================================================
        private void ZoomToSelection()
        {
            if (_image == null)
                return;

            // 選択範囲が小さすぎる場合は何もしない
            if (_selectionRectangle.Width < 5 || _selectionRectangle.Height < 5)
            {
                return;
            }

            // 選択範囲の中心座標（画面上）
            float selectionCenterX = _selectionRectangle.X + _selectionRectangle.Width / 2.0f;
            float selectionCenterY = _selectionRectangle.Y + _selectionRectangle.Height / 2.0f;

            // 選択範囲のサイズから新しいズーム倍率を計算
            float zoomX = pictureBox1.ClientSize.Width / (float)_selectionRectangle.Width;
            float zoomY = pictureBox1.ClientSize.Height / (float)_selectionRectangle.Height;
            float newZoom = Math.Min(zoomX, zoomY);

            // ズーム倍率を制限
            newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);

            // 選択範囲の中心が指している
            // 「元画像上の座標」を求める
            float imageCenterX = (selectionCenterX - _imageOffset.X) / _zoom;
            float imageCenterY = (selectionCenterY - _imageOffset.Y) / _zoom;

            // 新しい画像サイズ
            float newImageWidth = _image.Width * newZoom;
            float newImageHeight = _image.Height * newZoom;

            // 元画像上の「選択範囲の中心」が
            // 画面の中心に来るように画像位置を計算
            float screenCenterX = pictureBox1.ClientSize.Width / 2.0f;
            float screenCenterY = pictureBox1.ClientSize.Height / 2.0f;
            float newOffsetX = screenCenterX - imageCenterX * newZoom;
            float newOffsetY = screenCenterY - imageCenterY * newZoom;

            // 新しい表示状態を設定
            _zoom = newZoom;
            _imageOffset = new PointF(newOffsetX, newOffsetY);

            // 選択範囲を消す
            _selectionRectangle = Rectangle.Empty;

            // 再描画
            pictureBox1.Invalidate();
        }

        // ============================================================
        // キー操作
        // ============================================================
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            // EscキーでForm3を閉じる
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            if (_image == null)
                return;

            // Ctrl + C → 画像をコピー
            if (e.Control && e.KeyCode == Keys.C)
            {
                // 画像をクリップボードへコピー
                CopyImageToClipboard();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // Ctrl + V → クリップボードの画像を表示
            if (e.Control && e.KeyCode == Keys.V)
            {
                // クリップボードから画像を貼り付け
                PasteImageFromClipboard();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // Ctrl + S → 貼り付け画像を名前を付けて保存
            if (e.Control && e.KeyCode == Keys.S)
            {
                // クリップボードから貼り付けた画像のみ名前を付けて保存
                // ただし、今はフォルダ内のファイルも適用するようにしている
                if (_isClipboardImage)
                {
                    // trueなら
                    // 画像に名前を付けて保存
                    SaveClipboardImage();
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // Ctrl + L → 左へ90°回転
            if (e.Control && e.KeyCode == Keys.L)
            {
                // 左へ90°回転
                RotateImageLeft();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // Ctrl + R → 右へ90°回転
            if (e.Control && e.KeyCode == Keys.R)
            {
                // 右へ90°回転
                RotateImageRight();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // pageUp・BackSpaceキー
            //if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.Back)
            if (e.KeyCode == Keys.PageUp || e.KeyCode == Keys.Back)
            {
                // 上スクロール → 前の画像
                ShowPreviousImage();
                return;
            }

            // pageDown・Spaceキー
            //if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.Space)
            if (e.KeyCode == Keys.PageDown || e.KeyCode == Keys.Space)
            {
                // 下スクロール → 次の画像
                ShowNextImage();
                return;
            }

            // +キー
            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                // 拡大
                ZoomImage(ZoomStep);

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // -キー
            if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            {
                // 縮小
                ZoomImage(1.0f / ZoomStep);

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

        }

        // ============================================================
        // 矢印キー操作
        // ============================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_image != null)
            {

                // --------------------------------------------------------
                // Ctrl + 矢印 → 画像を移動(1px)
                // Ctrl + Shift + 矢印 → 画像を移動(10px)
                // --------------------------------------------------------
                bool shift = (keyData & Keys.Shift) == Keys.Shift;
                int step = shift ? 10 : 1;

                if ((keyData & Keys.Control) == Keys.Control)
                {
                    Keys key = keyData & Keys.KeyCode;

                    if (key == Keys.Up)
                    {
                        MoveImageByKeyboard(0, -step);
                        return true;
                    }

                    if (key == Keys.Down)
                    {
                        MoveImageByKeyboard(0, step);
                        return true;
                    }

                    if (key == Keys.Left)
                    {
                        MoveImageByKeyboard(-step, 0);
                        return true;
                    }

                    if (key == Keys.Right)
                    {
                        MoveImageByKeyboard(step, 0);
                        return true;
                    }
                }


                // --------------------------------------------------------
                // 通常の矢印キー
                // --------------------------------------------------------
                // ↑・←キー
                if (keyData == Keys.Up || keyData == Keys.Left)
                {
                    ShowPreviousImage();
                    return true;
                }

                // ↓・→キー
                if (keyData == Keys.Down || keyData == Keys.Right)
                {
                    ShowNextImage();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ============================================================
        // キーボードで画像を移動
        // ============================================================
        private void MoveImageByKeyboard(int dx, int dy)
        {
            _imageOffset = new PointF(
                _imageOffset.X + dx,
                _imageOffset.Y + dy);

            pictureBox1.Invalidate();
        }

        // ============================================================
        // キーボードによるズーム
        // ============================================================
        private void ZoomImage(float zoomFactor)
        {
            if (_image == null)
                return;

            float oldZoom = _zoom;

            float newZoom = oldZoom * zoomFactor;

            // ズーム倍率を制限
            newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);

            // 変化しなければ何もしない
            if (Math.Abs(newZoom - oldZoom) < 0.0001f)
                return;

            // 画面中央を基準にズーム
            float centerX = pictureBox1.ClientSize.Width / 2.0f;

            float centerY = pictureBox1.ClientSize.Height / 2.0f;

            // 画面中央が指している元画像上の座標
            float imageX = (centerX - _imageOffset.X) / oldZoom;

            float imageY = (centerY - _imageOffset.Y) / oldZoom;

            // 新しい倍率
            _zoom = newZoom;

            // 画面中央を基準に画像位置を調整
            _imageOffset.X = centerX - imageX * newZoom;

            _imageOffset.Y = centerY - imageY * newZoom;

            // 再描画
            pictureBox1.Invalidate();
        }

        // ============================================================
        // 画像をクリップボードへコピー
        // ============================================================
        private void CopyImageToClipboard()
        {
            if (_image == null)
                return;

            try
            {
                // 選択範囲がない → 画像全体をコピー
                if (_selectionRectangle.IsEmpty || _selectionRectangle.Width < 1 || _selectionRectangle.Height < 1)
                {
                    Bitmap fullImage = new Bitmap(_image.Width, _image.Height);

                    using (Graphics g = Graphics.FromImage(fullImage))
                    {
                        g.DrawImage(
                            _image,
                            new Rectangle(
                                0,
                                0,
                                _image.Width,
                                _image.Height));
                    }

                    Clipboard.SetImage(fullImage);

                    return;
                }

                // 選択範囲あり → 画面上の座標を元画像の座標へ変換
                float imageLeft = (_selectionRectangle.Left - _imageOffset.X) / _zoom;

                float imageTop = (_selectionRectangle.Top - _imageOffset.Y) / _zoom;

                float imageRight = (_selectionRectangle.Right - _imageOffset.X) / _zoom;

                float imageBottom = (_selectionRectangle.Bottom - _imageOffset.Y) / _zoom;

                int left = Math.Max(0, (int)Math.Floor(imageLeft));

                int top = Math.Max(0, (int)Math.Floor(imageTop));

                int right = Math.Min(_image.Width, (int)Math.Ceiling(imageRight));

                int bottom = Math.Min(_image.Height, (int)Math.Ceiling(imageBottom));

                int width = right - left;
                int height = bottom - top;

                // 画像の外だけを選択している場合
                if (width <= 0 || height <= 0)
                    return;

                // 元画像から選択範囲を切り出す
                Bitmap copiedImage = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(copiedImage))
                {
                    g.DrawImage(
                        _image,
                        new Rectangle(
                            0,
                            0,
                            width,
                            height),
                        new Rectangle(
                            left,
                            top,
                            width,
                            height),
                        GraphicsUnit.Pixel);
                }

                // クリップボードへ
                Clipboard.SetImage(copiedImage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像コピーエラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // クリップボードから画像を貼り付け
        // ============================================================
        private void PasteImageFromClipboard()
        {
            if (!Clipboard.ContainsImage())
                return;

            try
            {
                Image? clipboardImage = Clipboard.GetImage();

                if (clipboardImage == null)
                    return;

                // クリップボードのImageをそのまま使わず、
                // 自分でBitmapを作って所有する
                Image newImage = new Bitmap(clipboardImage);

                // 現在の画像を解放
                if (_image != null)
                {
                    Image oldImage = _image;

                    if (ImageAnimator.CanAnimate(oldImage))
                    {
                        ImageAnimator.StopAnimate(oldImage, PictureBoxAnimationHandler);
                    }

                    _image = null;
                    oldImage.Dispose();
                }

                // 新しい画像を設定
                _image = newImage;
                _isClipboardImage = true;

                // 選択範囲を解除
                _selectionRectangle = Rectangle.Empty;
                _isSelecting = false;
                _selectionClickCandidate = false;

                // 画面に合わせて表示
                FitImageToWindow();
                CenterImage();

                // タイトル・ステータス表示
                Text = "クリップボード画像";
                viewStatusTxt.Text = "クリップボードから貼り付けた画像";

                // 再描画
                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像貼り付けエラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 画像に名前を付けて保存
        // ============================================================
        private void SaveClipboardImage()
        {
            if (_image == null)
                return;

            using SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "画像を保存",
                FileName = "ClipboardImage.png",
                Filter =
                    "PNG画像 (*.png)|*.png|" +
                    "JPEG画像 (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "BMP画像 (*.bmp)|*.bmp|" +
                    "TIFF画像 (*.tif;*.tiff)|*.tif;*.tiff",
                FilterIndex = 1,
                AddExtension = true,
                OverwritePrompt = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                System.Drawing.Imaging.ImageFormat format;

                switch (Path.GetExtension(dialog.FileName).ToLowerInvariant())
                {
                    case ".jpg":
                    case ".jpeg":
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;

                    case ".bmp":
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;

                    case ".tif":
                    case ".tiff":
                        format = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;

                    default:
                        format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                }

                // _imageを直接保存せず、Bitmapとしてコピーして保存
                using Bitmap bitmap = new Bitmap(_image);

                bitmap.Save(dialog.FileName, format);

                // 保存後はタイトルをファイル名に変更
                Text = Path.GetFileName(dialog.FileName);
                viewStatusTxt.Text = dialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像保存エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 名前を付けて保存ボタンを押したとき
        // ============================================================
        private void saveBtn_Click(object sender, EventArgs e)
        {
            // クリップボードから貼り付けた画像のみ名前を付けて保存
            // ただし、今はフォルダ内のファイルも適用するようにしている
            if (_isClipboardImage)
            {
                // trueなら
                // 画像に名前を付けて保存
                SaveClipboardImage();
            }
        }

        // ============================================================
        // コピーボタンを押したとき
        // ============================================================
        private void copyBtn_Click(object sender, EventArgs e)
        {
            // 画像をクリップボードへコピー
            CopyImageToClipboard();
        }

        // ============================================================
        // 貼り付けボタンを押したとき
        // ============================================================
        private void pasteBtn_Click(object sender, EventArgs e)
        {
            // クリップボードから画像を貼り付け
            PasteImageFromClipboard();
        }

        // ============================================================
        // 選択範囲のどのハンドルにマウスがあるか
        // ============================================================
        private SelectionHandle GetSelectionHandle(Point location)
        {
            if (_selectionRectangle.IsEmpty)
                return SelectionHandle.None;

            int half = SelectionHandleSize;

            int left = _selectionRectangle.Left;
            int right = _selectionRectangle.Right;
            int top = _selectionRectangle.Top;
            int bottom = _selectionRectangle.Bottom;

            int centerX = _selectionRectangle.Left + _selectionRectangle.Width / 2;

            int centerY = _selectionRectangle.Top + _selectionRectangle.Height / 2;

            if (IsPointInHandle(location, left, top, half))
                return SelectionHandle.TopLeft;

            if (IsPointInHandle(location, centerX, top, half))
                return SelectionHandle.Top;

            if (IsPointInHandle(location, right, top, half))
                return SelectionHandle.TopRight;

            if (IsPointInHandle(location, left, centerY, half))
                return SelectionHandle.Left;

            if (IsPointInHandle(location, right, centerY, half))
                return SelectionHandle.Right;

            if (IsPointInHandle(location, left, bottom, half))
                return SelectionHandle.BottomLeft;

            if (IsPointInHandle(location, centerX, bottom, half))
                return SelectionHandle.Bottom;

            if (IsPointInHandle(location, right, bottom, half))
                return SelectionHandle.BottomRight;

            return SelectionHandle.None;
        }

        // ============================================================
        // ハンドル内にマウスがあるか
        // ============================================================
        private bool IsPointInHandle(Point location, int x, int y, int size)
        {
            Rectangle rectangle = new Rectangle(x - size, y - size, size * 2, size * 2);

            return rectangle.Contains(location);
        }

        // ============================================================
        // ハンドルに対応するカーソル
        // ============================================================
        private Cursor GetSelectionCursor(SelectionHandle handle)
        {
            switch (handle)
            {
                case SelectionHandle.TopLeft:
                case SelectionHandle.BottomRight:
                    return Cursors.SizeNWSE;

                case SelectionHandle.TopRight:
                case SelectionHandle.BottomLeft:
                    return Cursors.SizeNESW;

                case SelectionHandle.Top:
                case SelectionHandle.Bottom:
                    return Cursors.SizeNS;

                case SelectionHandle.Left:
                case SelectionHandle.Right:
                    return Cursors.SizeWE;

                default:
                    return Cursors.Default;
            }
        }

        // ============================================================
        // 選択範囲をサイズ変更
        // ============================================================
        private void ResizeSelection(Point mouseLocation)
        {
            Rectangle start = _selectionResizeStartRectangle;

            int left = start.Left;
            int top = start.Top;
            int right = start.Right;
            int bottom = start.Bottom;

            switch (_selectionHandle)
            {
                case SelectionHandle.TopLeft:
                    left = mouseLocation.X;
                    top = mouseLocation.Y;
                    break;

                case SelectionHandle.Top:
                    top = mouseLocation.Y;
                    break;

                case SelectionHandle.TopRight:
                    right = mouseLocation.X;
                    top = mouseLocation.Y;
                    break;

                case SelectionHandle.Left:
                    left = mouseLocation.X;
                    break;

                case SelectionHandle.Right:
                    right = mouseLocation.X;
                    break;

                case SelectionHandle.BottomLeft:
                    left = mouseLocation.X;
                    bottom = mouseLocation.Y;
                    break;

                case SelectionHandle.Bottom:
                    bottom = mouseLocation.Y;
                    break;

                case SelectionHandle.BottomRight:
                    right = mouseLocation.X;
                    bottom = mouseLocation.Y;
                    break;
            }

            // 最小サイズを維持
            if (right - left < SelectionMinSize)
            {
                if (_selectionHandle == SelectionHandle.TopLeft ||
                    _selectionHandle == SelectionHandle.Left ||
                    _selectionHandle == SelectionHandle.BottomLeft)
                {
                    left = right - SelectionMinSize;
                }
                else
                {
                    right = left + SelectionMinSize;
                }
            }

            if (bottom - top < SelectionMinSize)
            {
                if (_selectionHandle == SelectionHandle.TopLeft ||
                    _selectionHandle == SelectionHandle.Top ||
                    _selectionHandle == SelectionHandle.TopRight)
                {
                    top = bottom - SelectionMinSize;
                }
                else
                {
                    bottom = top + SelectionMinSize;
                }
            }

            _selectionRectangle = Rectangle.FromLTRB(left, top, right, bottom);
        }

        // ============================================================
        // 画像を左へ90度回転
        // ============================================================
        private void RotateImageLeft()
        {
            if (_image == null)
                return;

            try
            {
                // GIFアニメーションを停止
                if (ImageAnimator.CanAnimate(_image))
                {
                    ImageAnimator.StopAnimate(_image, PictureBoxAnimationHandler);
                }

                // 画像を左へ90度回転
                _image.RotateFlip(RotateFlipType.Rotate270FlipNone);

                // 選択範囲を解除
                _selectionRectangle = Rectangle.Empty;
                _isSelecting = false;
                _selectionClickCandidate = false;
                _isResizingSelection = false;
                _selectionHandle = SelectionHandle.None;

                // 回転後の画像を画面に合わせる
                FitImageToWindow();
                CenterImage();

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像回転エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 画像を右へ90度回転
        // ============================================================
        private void RotateImageRight()
        {
            if (_image == null)
                return;

            try
            {
                // GIFアニメーションを停止
                if (ImageAnimator.CanAnimate(_image))
                {
                    ImageAnimator.StopAnimate(_image, PictureBoxAnimationHandler);
                }

                // 画像を右へ90度回転
                _image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // 選択範囲を解除
                _selectionRectangle = Rectangle.Empty;
                _isSelecting = false;
                _selectionClickCandidate = false;
                _isResizingSelection = false;
                _selectionHandle = SelectionHandle.None;

                // 回転後の画像を画面に合わせる
                FitImageToWindow();
                CenterImage();

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "画像回転エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 左へ回転を押したとき
        // ============================================================
        private void roolLeftBtn_Click(object sender, EventArgs e)
        {
            RotateImageLeft();
        }

        // ============================================================
        // 右へ回転を押したとき
        // ============================================================
        private void roolRightBtn_Click(object sender, EventArgs e)
        {
            RotateImageRight();
        }

        // ============================================================
        // 閉じるを押したとき
        // ============================================================
        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 現在のフォームを閉じる
            this.Close();
        }

        // ============================================================
        // 拡大を押したとき
        // ============================================================
        private void zoomInBtn_Click(object sender, EventArgs e)
        {
            ZoomImage(ZoomStep);
        }

        // ============================================================
        // 縮小を押したとき
        // ============================================================
        private void zoomOutBtn_Click(object sender, EventArgs e)
        {
            ZoomImage(1.0f / ZoomStep);
        }
    }
}
