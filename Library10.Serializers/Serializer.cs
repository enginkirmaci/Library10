using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Library10.Core.Serialization
{
    public class Serializer
    {
        private Serializer()
        {
        }

        public static T Deserialize<T>(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return Deserialize<T>(ms);
            }
        }

        public static T Deserialize<T>(MemoryStream ms)
        {
            return DeserializeJson<T>(Encoding.UTF8.GetString(ms.ToArray()));
        }

        public static T Deserialize<T>(Stream s)
        {
            using (var reader = new StreamReader(s, Encoding.UTF8))
            {
                return DeserializeJson<T>(reader.ReadToEnd());
            }
        }

        public static byte[] Serialize<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(SerializeToJson(obj));
        }

        /// <summary>
        /// Serializes an object to the respectable JSON string.
        /// </summary>
        public static string SerializeToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes a JSON string to the specified object.
        /// </summary>
        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}