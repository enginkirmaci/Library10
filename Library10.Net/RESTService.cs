using Library10.Core.Serialization;
using Library10.Net.Constants;
using Library10.Net.Entities;
using Library10.Net.Enums;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Library10.Net
{
    public class RESTService
    {
        public HttpClient HttpClient { get; set; }

        public RESTService()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(RESTConstants.MediaType));
        }

        public void SetBasicAuthentication(string username, string password)
        {
            var parametre = StringToAsciiBase64(string.Format(RESTConstants.BasicAuthParametreFormat, username, password));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(RESTConstants.BasicAuthScheme, parametre);
        }

        public async Task<T2> Execute<T, T2>(RESTCommand<T, T2> command)
        {
            try
            {
                switch (command.Type)
                {
                    case RESTCommandType.GET:
                        return await GetAsync(command);

                    case RESTCommandType.PUT:
                        return await PutAsync(command);

                    case RESTCommandType.POST:
                        return await PostAsync(command);
                }
            }
            catch
            {
            }

            return default(T2);
        }

        public async Task<T2> GetAsync<T, T2>(RESTCommand<T, T2> command)
        {
            var response = await HttpClient.GetAsync(command.Url);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStringAsync();

            return Parse<T2>(stream);
        }

        public async Task<T2> PutAsync<T, T2>(RESTCommand<T, T2> command)
        {
            var response = await HttpClient.PutAsync(command.Url, Parse(command.Body));
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStringAsync();

            return Parse<T2>(stream);
        }

        public async Task<T2> PostAsync<T, T2>(RESTCommand<T, T2> command)
        {
            var response = await HttpClient.PostAsync(command.Url, Parse(command.Body));

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStringAsync();

            return Parse<T2>(stream);
        }

        #region Private Methods

        private StringContent Parse<T>(T value)
        {
            return new StringContent(Serializer.SerializeToJson(value), Encoding.UTF8, RESTConstants.MediaType);
        }

        private T Parse<T>(string stream)
        {
            return Serializer.DeserializeJson<T>(stream);
        }

        private static string StringToAsciiBase64(string s)
        {
            byte[] retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix)
            {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return Convert.ToBase64String(retval);
        }

        #endregion Private Methods
    }
}