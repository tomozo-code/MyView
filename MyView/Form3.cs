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
// ctrl + 上スクロールで拡大、ctrl + 下スクロールで縮小
// 右ドラッグで画像内移動
// 上スクロールで前の画像、下スクロールで次の画像

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

        // ============================================================
        // コンストラクタ
        // ============================================================
        public Form3(string imagePath)
        {
            InitializeComponent();

            this.Width = 600;
            this.Height = 600;
            this.MinimumSize = new Size(300, 300);

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

        // --------------------------------------------------------
        // フォームをロードしたとき
        // --------------------------------------------------------
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

            string[] extensions = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff", ".ico", ".wmf", ".emf" };

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

            // --------------------------------------------------------
            // 横方向・縦方向、それぞれの倍率を計算
            // --------------------------------------------------------
            float zoomX =
                viewWidth / (float)_image.Width;

            float zoomY =
                viewHeight / (float)_image.Height;

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

            // --------------------------------------------------------
            // Ctrl + ホイール
            // → 今まで通りズーム
            // --------------------------------------------------------
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

                // ----------------------------------------------------
                // マウスカーソル位置を基準にズーム
                // ----------------------------------------------------

                float imageX =
                    (mousePosition.X - _imageOffset.X) / oldZoom;

                float imageY =
                    (mousePosition.Y - _imageOffset.Y) / oldZoom;

                _zoom = newZoom;

                _imageOffset.X =
                    mousePosition.X - imageX * newZoom;

                _imageOffset.Y =
                    mousePosition.Y - imageY * newZoom;

                pictureBox1.Invalidate();

                return;
            }

            // --------------------------------------------------------
            // 通常のホイール
            // → 前後の画像へ移動
            // --------------------------------------------------------

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

            SetImage(_imageFiles[_currentImageIndex]);
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

            SetImage(_imageFiles[_currentImageIndex]);
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

            // --------------------------------------------------------
            // 左ドラッグの選択範囲を描画
            // --------------------------------------------------------
            if (!_selectionRectangle.IsEmpty)
            {
                using Pen pen = new Pen(Color.White, 2);

                pen.DashStyle = DashStyle.Dash;

                g.DrawRectangle(pen,  _selectionRectangle);
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

            // --------------------------------------------------------
            // ズーム倍率は変更しない。
            //
            // 現在の表示サイズをそのまま維持して、
            // 新しいPictureBoxの中央へ画像を移動する。
            // --------------------------------------------------------
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
            // --------------------------------------------------------
            // 右ボタン → 画像移動
            // --------------------------------------------------------
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

            // --------------------------------------------------------
            // 左ボタン
            // --------------------------------------------------------
            if (e.Button == MouseButtons.Left)
            {
                // ----------------------------------------------------
                // すでに選択範囲があり、
                // その中をクリックした場合
                // ----------------------------------------------------
                if (!_selectionRectangle.IsEmpty &&
                    _selectionRectangle.Contains(e.Location))
                {
                    _selectionClickCandidate = true;
                    _selectionClickStart = e.Location;

                    return;
                }

                // ----------------------------------------------------
                // 新しい範囲選択を開始
                // ----------------------------------------------------
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
            // --------------------------------------------------------
            // 右ドラッグ → 画像移動
            // --------------------------------------------------------
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

            // --------------------------------------------------------
            // 左ドラッグ → 範囲選択
            // --------------------------------------------------------
            if (_isSelecting)
            {
                int x = Math.Min(_selectionStart.X, e.X);
                int y = Math.Min(_selectionStart.Y, e.Y);

                int width = Math.Abs(e.X - _selectionStart.X);
                int height = Math.Abs(e.Y - _selectionStart.Y);

                _selectionRectangle = new Rectangle(x, y, width, height);

                pictureBox1.Invalidate();
            }
        }

        // ============================================================
        // マウスボタンを離したとき
        // ============================================================
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // --------------------------------------------------------
            // 右ボタン → 画像移動終了
            // --------------------------------------------------------
            if (e.Button == MouseButtons.Right)
            {
                _isPanning = false;

                pictureBox1.Cursor = Cursors.Default;

                return;
            }

            // --------------------------------------------------------
            // 左ボタン
            // --------------------------------------------------------
            if (e.Button == MouseButtons.Left)
            {
                // ----------------------------------------------------
                // 選択範囲内をクリックした
                // ----------------------------------------------------
                if (_selectionClickCandidate)
                {
                    _selectionClickCandidate = false;

                    // 選択範囲を拡大
                    ZoomToSelection();

                    pictureBox1.Cursor = Cursors.Default;

                    return;
                }

                // ----------------------------------------------------
                // 範囲選択終了
                // ----------------------------------------------------
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

            // --------------------------------------------------------
            // 選択範囲の中心座標（画面上）
            // --------------------------------------------------------
            float selectionCenterX = _selectionRectangle.X + _selectionRectangle.Width / 2.0f;

            float selectionCenterY = _selectionRectangle.Y + _selectionRectangle.Height / 2.0f;

            // --------------------------------------------------------
            // 選択範囲のサイズから新しいズーム倍率を計算
            // --------------------------------------------------------
            float zoomX = pictureBox1.ClientSize.Width / (float)_selectionRectangle.Width;

            float zoomY = pictureBox1.ClientSize.Height / (float)_selectionRectangle.Height;

            float newZoom = Math.Min(zoomX, zoomY);

            // ズーム倍率を制限
            newZoom = Math.Clamp(newZoom, MinZoom, MaxZoom);

            // --------------------------------------------------------
            // 選択範囲の中心が指している
            // 「元画像上の座標」を求める
            // --------------------------------------------------------
            float imageCenterX = (selectionCenterX - _imageOffset.X) / _zoom;

            float imageCenterY = (selectionCenterY - _imageOffset.Y) / _zoom;

            // --------------------------------------------------------
            // 新しい画像サイズ
            // --------------------------------------------------------
            float newImageWidth = _image.Width * newZoom;

            float newImageHeight = _image.Height * newZoom;

            // --------------------------------------------------------
            // 元画像上の「選択範囲の中心」が
            // 画面の中心に来るように画像位置を計算
            // --------------------------------------------------------
            float screenCenterX = pictureBox1.ClientSize.Width / 2.0f;

            float screenCenterY = pictureBox1.ClientSize.Height / 2.0f;

            float newOffsetX = screenCenterX - imageCenterX * newZoom;

            float newOffsetY = screenCenterY - imageCenterY * newZoom;

            // --------------------------------------------------------
            // 新しい表示状態を設定
            // --------------------------------------------------------
            _zoom = newZoom;

            _imageOffset = new PointF(newOffsetX, newOffsetY);

            // 選択範囲を消す
            _selectionRectangle = Rectangle.Empty;

            // 再描画
            pictureBox1.Invalidate();
        }
    }
}
