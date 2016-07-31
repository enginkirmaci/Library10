using Library10.Core.Serialization;

namespace Library10.Core.Configuration
{
    public interface IStoreConverter
    {
        string ToStore<T>(T value);

        T FromStore<T>(string value);
    }

    public interface IPropertyMapping
    {
        IStoreConverter GetConverter<T>();
    }

    public class JsonConverter : IStoreConverter
    {
        public T FromStore<T>(string value) => Serializer.DeserializeJson<T>(value);

        public string ToStore<T>(T value) => Serializer.SerializeToJson(value);
    }

    public class JsonMapping : IPropertyMapping
    {
        protected IStoreConverter jsonConverter = new JsonConverter();

        public IStoreConverter GetConverter<T>()
        {
            return jsonConverter;
        }
    }
}