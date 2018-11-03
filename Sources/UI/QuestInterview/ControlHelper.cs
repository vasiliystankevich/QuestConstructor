using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System
{
    public class ControlHelper
    {
        private int scrollValue;
        private ScrollableControl ctrl;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        public ControlHelper(ScrollableControl ctrl)
        {
            this.ctrl = ctrl;
            StopDrawing();
        }

        private void StopDrawing()
        {
            SendMessage(ctrl.Handle, WM_SETREDRAW, false, 0);
            scrollValue = ctrl.VerticalScroll.Value;
            ctrl.SuspendLayout();
        }

        public void ResumeDrawing()
        {
            ctrl.VerticalScroll.Value = Math.Min(ctrl.VerticalScroll.Maximum, scrollValue);
            ctrl.ResumeLayout();
            SendMessage(ctrl.Handle, WM_SETREDRAW, true, 0);
            ctrl.Refresh();
        }
    }
}
