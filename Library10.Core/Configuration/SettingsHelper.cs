using static Library10.Core.Configuration.SettingsService;

namespace Library10.Core.Configuration
{
    public class SettingsHelper
    {
        private static SettingsService Container(SettingsStrategy strategy, string key = null)
        {
            switch (strategy)
            {
                case SettingsStrategy.Local:
                    return Local;

                case SettingsStrategy.Roaming:
                    return Roaming;

                case SettingsStrategy.Temporary:
                    return Temporary;

                default:
                    return FindContainer(key);
            }
        }

        public static bool Exists(string key, SettingsStrategy strategy = SettingsStrategy.Unknown)
        {
            var settings = Container(strategy, key);
            return settings != null;
        }

        public static void Remove(string key, SettingsStrategy strategy = SettingsStrategy.Unknown)
        {
            var settings = Container(strategy, key);
            if (settings != null)
                settings.Remove(key);
        }

        public static void Write<T>(string key, T value, SettingsStrategy strategy = SettingsStrategy.Local)
        {
            var settings = Container(strategy);
            settings.Write(key, value);
        }

        public static T Read<T>(string key, T otherwise = default(T), SettingsStrategy strategy = SettingsStrategy.Unknown)
        {
            var settings = Container(strategy, key);
            if (settings != null)
                return settings.Read(key, otherwise);
            else
                return otherwise;
        }
    }
}