using Library10.Core.BaseClasses;
using Library10.Core.Configuration;

namespace Library10.Sample
{
    public class AppSettings : BaseSettings
    {
        private AppSettings()
        {
        }

        public class ApplicationSettings
        {
            public static int StartCount { get { return Read("StartCount", 0); } set { Write("StartCount", value, SettingsStrategy.Local); } }
        }

        public static class NewsSettings
        {
            public static int FontSize { get { return Read("FontSize", 14); } set { Write("FontSize", value, SettingsStrategy.Roaming); } }
        }

        public static void Initialize()
        {
            General.AppName = "haberler+";

            //General = new GeneralSettings()
            //{
            //    AppName = "haberler+",
            //    AppId = 5,
            //    AppMarketId = "5f33aefb-0222-4428-a388-91ee7a6db0b0",
            //    AppNameCapitalized = "Haberler+",
            //    AppVersion = "2.0.2.3",
            //    AppUrl = "enginkirmaci.com/projects/haberler",
            //    PrivacyPolicyUrl = "enginkirmaci.com/blogs/privacy-policy-haberler",
            //    CompanyName = "Engin Kırmacı",
            //    CompanyUrl = "enginkirmaci.com",
            //    ConnectionString = "haberler.db",
            //    //ServiceUrl = "http://localhost:63728",
            //    ServiceUrl = "http://ws.enginkirmaci.com",
            //    TwitterUrl = "twitter.com/enginkirmaci",
            //    FacebookUrl = "facebook.com/engin.kirmaci",
            //    LinkedinUrl = "linkedin.com/in/enginkirmaci"
            //};

            //Products = new ProductSettings()
            //{
            //    ProductKey = "remove_ads"
            //};
        }

        #region Session

        public static bool RateReminderShown { get { return Read("RateReminderShown", false); } set { Write("RateReminderShown", value, SettingsStrategy.Temporary); } }

        #endregion Session
    }
}