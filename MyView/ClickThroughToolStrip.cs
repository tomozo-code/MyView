
using System;
using System.Windows.Forms;

// --------------------------------------------------------
// 非アクティブのウィンドウツールバーをクリックしたとき
// --------------------------------------------------------

namespace MyView
{
    public class ClickThroughToolStrip : ToolStrip
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEACTIVATE = 0x0021;
            const int MA_ACTIVATE = 1;

            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_ACTIVATE;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
