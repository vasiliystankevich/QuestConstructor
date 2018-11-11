using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Quest.Controls.QuestConstructor
{
    public class OpenFolderDialog
    {
        public DialogResult ShowDialog(IWin32Window owner)
        {
            return Environment.OSVersion.Version.Major >= 6 ? ShowVistaDialog(owner) : ShowLegacyDialog(owner);
        }

        private DialogResult ShowVistaDialog(IWin32Window owner)
        {
            var folderDialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Multiselect = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = DefaultFolder
            };
            var result = folderDialog.ShowDialog(owner.Handle);
            folderDialog.Dispose();
            return result == CommonFileDialogResult.Ok ? DialogResult.OK : DialogResult.Cancel;
        }

        private DialogResult ShowLegacyDialog(IWin32Window owner)
        {
            using (var frm = new SaveFileDialog())
            {
                frm.CheckFileExists = false;
                frm.CheckPathExists = true;
                frm.CreatePrompt = false;
                frm.Filter = "|" + Guid.Empty;
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
