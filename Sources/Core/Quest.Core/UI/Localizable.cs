using System.Globalization;
using System.Threading;

namespace Quest.Core.UI
{
    public static class Localizable
    {
        public static void SetCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
    }
}
