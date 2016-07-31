using Windows.Foundation.Collections;
using Windows.Storage;

namespace Library10.Core.Configuration
{
    public class SettingsService : PropertyStore
    {
        private static SettingsService _local;

        public static SettingsService Local
        {
            get
            {
                if (_local == null)
                    _local = new SettingsService(ApplicationData.Current.LocalSettings.Values);

                return _local;
            }
        }

        private static SettingsService _roaming;

        public static SettingsService Roaming
        {
            get
            {
                if (_roaming == null)
                    _roaming = new SettingsService(ApplicationData.Current.RoamingSettings.Values);

                return _roaming;
            }
        }

        private static SettingsService _temporary;

        public static SettingsService Temporary
        {
            get
            {
                if (_temporary == null)
                    _temporary = new SettingsService(new PropertySet());

                return _temporary;
            }
        }

        private static readonly IPropertyMapping Mapping;

        static SettingsService()
        {
            // static constructor
            Mapping = new JsonMapping();
        }

        private SettingsService(IPropertySet values) : base(values, Mapping)
        { /* private constructor */ }

        internal static SettingsService FindContainer(string key)
        {
            if (Local.Exists(key))
                return Local;
            else if (Roaming.Exists(key))
                return Roaming;
            else if (Temporary.Exists(key))
                return Temporary;
            else
                return null;
        }
    }
}