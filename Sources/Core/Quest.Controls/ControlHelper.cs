using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quest.Controls
{
    public class ControlHelper
    {
        public ControlHelper(ScrollableControl ctrl)
        {
            _ctrl = ctrl;
            StopDrawing();
        }

        private void StopDrawing()
        {
            SendMessage(_ctrl.Handle, WmSetredraw, false, 0);
            _scrollValue = _ctrl.VerticalScroll.Value;
            _ctrl.SuspendLayout();
        }

        public void ResumeDrawing()
        {
            _ctrl.VerticalScroll.Value = Math.Min(_ctrl.VerticalScroll.Maximum, _scrollValue);
            _ctrl.ResumeLayout();
            SendMessage(_ctrl.Handle, WmSetredraw, true, 0);
            _ctrl.Refresh();
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        private int _scrollValue;
        private readonly ScrollableControl _ctrl;
        private const int WmSetredraw = 11;
    }
}
