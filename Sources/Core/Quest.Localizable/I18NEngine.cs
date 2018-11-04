using System;

namespace Quest.Localizable
{
    public static class I18NEngine
    {
        private static string GetStringForId(string key, string id)
        {
            //Quest.Controls
            var result = string.Empty;
            switch (key)
            {
                case "common": result = i18n.common.messages.ResourceManager.GetString(id); break;

                case "quest.core": result = i18n.quest.core.messages.ResourceManager.GetString(id); break;
                case "quest.controls": result = i18n.quest.controls.messages.ResourceManager.GetString(id); break;
                case "quest.constructor": result = i18n.quest.constructor.messages.ResourceManager.GetString(id); break;
                case "quest.interview": result = i18n.quest.interview.messages.ResourceManager.GetString(id); break;
            }
            return result;
        }

        public static string GetString(string key, string id)
        {
            var result = GetStringForId(key, id);
            if (string.IsNullOrWhiteSpace(result))
                result = GetStringForId("common", "resource_not_found");
            return result;
        }

        public static string GetLocaleString<TEnum>(this TEnum sender, string key)
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            var nameType = typeof(TEnum).Name.ToLowerInvariant();
            var nameValue = Enum.GetName(typeof(TEnum), sender)?.ToLowerInvariant();
            return GetString(key, $"{nameType}_{nameValue}");
        }

        public static T ParseEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetLocaleString(this object sender, string key, string id) { return GetString(key, id); }
    }
}
