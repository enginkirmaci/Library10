using Library10.Common.Converters;
using System;
using Windows.UI.Xaml.Data;

namespace Library10.Core.Converters
{
    public class RelativeTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts a
        /// <see cref="T:System.DateTime"/>
        /// object into a string the represents the elapsed time
        /// relatively to the present.
        /// </summary>
        /// <param name="value">The given date and time.</param>
        /// <param name="targetType">
        /// The type corresponding to the binding property, which must be of
        /// <see cref="T:System.String"/>.
        /// </param>
        /// <param name="parameter">(Not used).</param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// When not specified, the converter uses the current culture
        /// as specified by the system locale.
        /// </param>
        /// <returns>The given date and time as a string.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Target value must be a System.DateTime object.
            if (!(value is DateTime))
            {
                throw new ArgumentException("InvalidDateTimeArgument");
            }

            return DateTimeConverter.ToTurkishRelativeTime((DateTime)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}