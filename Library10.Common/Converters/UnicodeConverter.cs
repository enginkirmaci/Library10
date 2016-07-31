using System;

namespace Library10.Common.Converters
{
    public class UnicodeConverter
    {
        static public string ToTurkish(string strIn)
        {
            string[] wrongChars =
            {
                "&amp;", "Ä±", "ÄŸ", "Ã¼", "Ã¶", "ÅŸ", "Ã§", "Ä°", "Å", "Ãœ", "Ã‡", "Ã–", "ÄŸ", "â€™", "Â", "â€", "Ã®",
                "&#304;",
                "&#305;",
                "&#214;",
                "&#246;",
                "&#220;",
                "&#252;",
                "&#199;",
                "&#231;",
                "&#286;",
                "&#287;",
                "&#350;",
                "&#351;",
                "&#8356;",
                "&Ouml;",
                "&ouml;",
                "&Uuml;",
                "&uuml;",
                "&Ccedil;",
                "&ccedil;"
            };
            string[] correctChars =
            {
                "&", "ı", "ğ", "ü", "ö", "ş", "ç", "İ", "Ş", "Ü", "Ç", "Ö", "Ğ", "'", "", "\"", "î",
                "İ",
                "ı",
                "Ö",
                "ö",
                "Ü",
                "ü",
                "Ç",
                "ç",
                "Ğ",
                "ğ",
                "Ş",
                "ş",
                "₤",
                "Ö",
                "ö",
                "Ü",
                "ü",
                "Ç",
                "ç"
            };

            for (var i = 0; i < wrongChars.Length; i++)
                strIn = strIn.Replace(wrongChars[i], correctChars[i]);

            return strIn;
        }
    }
}