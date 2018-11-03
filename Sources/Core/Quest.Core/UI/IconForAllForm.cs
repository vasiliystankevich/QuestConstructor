using System.Reflection;
using System.Windows.Forms;

namespace Quest.Core.UI
{
    public static class IconForAllForm
    {
        public static void SetIcon(System.Drawing.Icon icon)
        {
            var typeForm = typeof(Form);
            var iconField = typeForm.GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static);
            iconField?.SetValue(null, icon);
        }
    }
}
