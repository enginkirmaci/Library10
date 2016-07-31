using System;

namespace Library10.Common.Extensions
{
    public static class UrlExtensions
    {
        public static Uri ToUri(this string url, bool addHTTP = false)
        {
            if (addHTTP)
                return new Uri(string.Format("http://{0}", url), UriKind.Absolute);

            return new Uri(url, UriKind.Absolute);
        }
    }
}