using System.Globalization;
using System.Threading;

namespace Quest.Core.UI
{
    public static class LocalizableComponentsExtended
    {
        public static void SetCulture(this ILocalizableComponents sender, string culture)
        {
            Thread.CurrentThread.CurrentCulture=new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            sender.LocalizableComponents();
        }
    }
}
