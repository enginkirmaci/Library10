using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Library10.Core.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!string.IsNullOrEmpty(parameter as string))
                return (value.ToString() == parameter as string) ? Visibility.Visible : Visibility.Collapsed;

            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((Visibility)value) == Visibility.Visible;
        }
    }
}