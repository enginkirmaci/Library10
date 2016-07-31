using System.Collections.Generic;
using System.Linq;

namespace Library10.Net.Extensions
{
    public static class QueryExtensions
    {
        public static string ToQueryString(this IDictionary<string, string> parameters)
        {
            var array = parameters.Select(i => string.Format("{0}={1}", i.Key, i.Value)).ToArray();

            return "?" + string.Join("&", array);
        }
    }
}