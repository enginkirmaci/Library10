using Library10.Core.Configuration;

namespace Library10.Core.BaseClasses
{
    public class BaseSettings : SettingsHelper
    {
        public static class General
        {
            public static int AppId { get; set; }
            public static string AppMarketId { get; set; }
            public static string AppNameCapitalized { get; set; }
            public static string AppName { get; set; }
            public static string AppVersion { get; set; }
            public static string AppUrl { get; set; }
            public static string PrivacyPolicyUrl { get; set; }
            public static string CompanyName { get; set; }
            public static string CompanyUrl { get; set; }
            public static string FeedbackUrl { get; set; }
            public static string ConnectionString { get; set; }
            public static string ServiceUrl { get; set; }
            public static string TwitterUrl { get; set; }
            public static string FacebookUrl { get; set; }
            public static string LinkedinUrl { get; set; }
        }

        public static class Product
        {
            public static string ProductKey { get; set; }
        }
    }
}