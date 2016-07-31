using System;
using Windows.UI.Xaml.Data;

namespace Library10.Core.Converters
{
    public class NullBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}