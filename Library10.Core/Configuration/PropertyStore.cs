using System;
using Windows.Foundation.Collections;

namespace Library10.Core.Configuration
{
    public class PropertyStore
    {
        protected IPropertyMapping Converters { get; }

        protected IPropertySet Values { get; }

        public PropertyStore(IPropertySet values, IPropertyMapping converters)
        {
            if (values == null || converters == null)
                throw new ArgumentNullException();

            Values = values;
            Converters = converters;
        }

        public bool Exists(string key)
        {
            return Values.ContainsKey(key);
        }

        public void Remove(string key)
        {
            if (Values.ContainsKey(key))
                Values.Remove(key);
        }

        public void Write<T>(string key, T value)
        {
            var converter = Converters.GetConverter<T>();
            Values[key] = converter.ToStore(value);
        }

        public T Read<T>(string key, T fallback)
        {
            try
            {
                if (!Values.ContainsKey(key))
                {
                    return fallback;
                }

                var converter = Converters.GetConverter<T>();
                return converter.FromStore<T>(Values[key].ToString());
            }
            catch
            {
                return fallback;
            }
        }
    }
}