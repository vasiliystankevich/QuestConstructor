using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quest.Controls
{
    public class OpenFolderDialog
    {
        public DialogResult ShowDialog(IWin32Window owner)
        {

            if (Environment.OSVersion.Version.Major >= 6)
                return ShowVistaDialog(owner);
            else
                return ShowLegacyDialog(owner);
        }

        private DialogResult ShowVistaDialog(IWin32Window owner)
        {
            var frm = (NativeMethods.IFileDialog)new NativeMethods.FileOpenDialogRCW();
            uint options;
            frm.GetOptions(out options);
            options |= NativeMethods.FosPickfolders | NativeMethods.FosForcefilesystem | NativeMethods.FosNovalidate | NativeMethods.FosNotestfilecreate | NativeMethods.FosDontaddtorecent;
            frm.SetOptions(options);
            if (InitialFolder != null)
            {
                NativeMethods.IShellItem directoryShellItem;
                var riid = new Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"); //IShellItem
                if (NativeMethods.SHCreateItemFromParsingName(InitialFolder, IntPtr.Zero, ref riid, out directoryShellItem) == NativeMethods.SOk)
                {
                    frm.SetFolder(directoryShellItem);
                }
            }
            if (DefaultFolder != null)
            {
                NativeMethods.IShellItem directoryShellItem;
                var riid = new Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"); //IShellItem
                if (NativeMethods.SHCreateItemFromParsingName(DefaultFolder, IntPtr.Zero, ref riid, out directoryShellItem) == NativeMethods.SOk)
                {
                    frm.SetDefaultFolder(directoryShellItem);
                }
            }

            if (frm.Show(owner.Handle) == NativeMethods.SOk)
            {
                NativeMethods.IShellItem shellItem;
                if (frm.GetResult(out shellItem) == NativeMethods.SOk)
                {
                    IntPtr pszString;
                    if (shellItem.GetDisplayName(NativeMethods.SigdnFilesyspath, out pszString) == NativeMethods.SOk)
                    {
                        if (pszString != IntPtr.Zero)
                        {
                            try
                            {
                                Folder = Marshal.PtrToStringAuto(pszString);
                                return DialogResult.OK;
                            }
                            finally
                            {
                                Marshal.FreeCoTaskMem(pszString);
                            }
                        }
                    }
                }
            }
            return DialogResult.Cancel;
        }

        private DialogResult ShowLegacyDialog(IWin32Window owner)
        {
            using (var frm = new SaveFileDialog())
            {
                frm.CheckFileExists = false;
                frm.CheckPathExists = true;
                frm.CreatePrompt = false;
                frm.Filter = "|" + Guid.Empty.ToString();
                frm.FileName = "any";
                if (InitialFolder != null) { frm.InitialDirectory = InitialFolder; }
                frm.OverwritePrompt = false;
                frm.Title = "Select Folder";
                frm.ValidateNames = false;
                if (frm.ShowDialog(owner) == DialogResult.OK)
                {
                    Folder = Path.GetDirectoryName(frm.FileName);
                    return DialogResult.OK;
                }
                else
                {
                    return DialogResult.Cancel;
                }
            }
        }

        public string InitialFolder { get; set; }
        public string DefaultFolder { get; set; }
        public string Folder { get; private set; }


    }
}
