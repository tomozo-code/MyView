
using System;
using System.Windows.Forms;

// --------------------------------------------------------
// 非アクティブのウィンドウメニューをクリックしたとき
// --------------------------------------------------------

namespace MyView
{
    public class ClickThroughMenuStrip : MenuStrip
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEACTIVATE = 0x0021;
            const int MA_ACTIVATE = 1;

            if (m.Msg == WM_MOUSEACTIVATE)
            {
                // フォームをアクティブにする
                // クリック自体は通常の処理に任せる
                m.Result = (IntPtr)MA_ACTIVATE;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
