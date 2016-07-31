using Library10.Net.Enums;
using Library10.Net.Extensions;
using System.Collections.Generic;

namespace Library10.Net.Entities
{
    public class RESTCommand<T, T2>
    {
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public string Url
        {
            get
            {
                return string.Format("{0}/{1}{2}", ServiceUrl, Query, BuildParameters());
            }
        }

        public string ServiceUrl { get; set; }
        public string Query { get; set; }
        public string QueryParameter { get; set; }
        public RESTCommandType Type { get; set; }
        public T Body { get; set; }

        public void AddParameter(string key, object value)
        {
            parameters.Add(key, value.ToString());
        }

        private string BuildParameters()
        {
            var result = string.IsNullOrEmpty(QueryParameter) ? string.Empty : "/" + QueryParameter;

            if (parameters != null && parameters.Count != 0)
                result += parameters.ToQueryString();

            return result;
        }
    }
}