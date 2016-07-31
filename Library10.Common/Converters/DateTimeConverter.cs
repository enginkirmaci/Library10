using System;
using System.Globalization;

namespace Library10.Common.Converters
{
    public class DateTimeConverter
    {
        private const double Minute = 60.0;
        private const double Hour = 60.0 * Minute;
        private const double Day = 24 * Hour;
        private const double Week = 7 * Day;
        private const double Month = 30.5 * Day;
        private const double Year = 365 * Day;

        public static string ToTurkishRelativeTime(DateTime value)
        {
            string result;

            DateTime given = value.ToLocalTime();

            DateTime current = DateTime.Now;

            TimeSpan difference = current - given;

            var PluralHourStrings = "{0} saat önce";

            var PluralMinuteStrings = "{0} dakika önce";

            var PluralSecondStrings = "{0} saniye önce";

            if (IsFutureDateTime(current, given))
            {
                // Future dates and times are not supported, but to prevent crashing an app
                // if the time they receive from a server is slightly ahead of the phone's clock
                // we'll just default to the minimum, which is "2 seconds ago".
                result = GetPluralTimeUnits(2, PluralSecondStrings);
            }

            if (difference.TotalSeconds > Year)
            {
                // "over a year ago"
                result = "Bir yıldan uzun bir süre önce";
            }
            else if (difference.TotalSeconds > (1.5 * Month))
            {
                // "x months ago"
                int nMonths = (int)((difference.TotalSeconds + Month / 2) / Month);
                result = GetPluralMonth(nMonths);
            }
            else if (difference.TotalSeconds >= (3.5 * Week))
            {
                // "about a month ago"
                result = "Yaklaşık bir ay önce";
            }
            else if (difference.TotalSeconds >= Week)
            {
                int nWeeks = (int)(difference.TotalSeconds / Week);
                if (nWeeks > 1)
                {
                    // "x weeks ago"
                    result = string.Format(CultureInfo.CurrentUICulture, "{0} hafta önce", nWeeks);
                }
                else
                {
                    // "about a week ago"
                    result = "Yaklaşık bir hafta önce";
                }
            }
            else if (difference.TotalSeconds >= (5 * Day))
            {
                // "last <dayofweek>"
                result = GetLastDayOfWeek(given.DayOfWeek);
            }
            else if (difference.TotalSeconds >= Day)
            {
                // "on <dayofweek>"
                result = GetOnDayOfWeek(given.DayOfWeek);
            }
            else if (difference.TotalSeconds >= (2 * Hour))
            {
                // "x hours ago"
                int nHours = (int)(difference.TotalSeconds / Hour);
                result = GetPluralTimeUnits(nHours, PluralHourStrings);
            }
            else if (difference.TotalSeconds >= Hour)
            {
                // "about an hour ago"
                result = "Yaklaşık bir saat önce";
            }
            else if (difference.TotalSeconds >= (2 * Minute))
            {
                // "x minutes ago"
                int nMinutes = (int)(difference.TotalSeconds / Minute);
                result = GetPluralTimeUnits(nMinutes, PluralMinuteStrings);
            }
            else if (difference.TotalSeconds >= Minute)
            {
                // "about a minute ago"
                result = "Yaklaşık bir dakika önce";
            }
            else
            {
                // "x seconds ago" or default to "2 seconds ago" if less than two seconds.
                int nSeconds = ((int)difference.TotalSeconds > 1.0) ? (int)difference.TotalSeconds : 2;
                result = GetPluralTimeUnits(nSeconds, PluralSecondStrings);
            }

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets the number representing the day of the week from a given
        /// <see cref="T:System.DateTime"/>
        /// object, relative to the first day of the week
        /// according to the current culture.
        /// </summary>
        /// <param name="dt">Date information.</param>
        /// <returns>
        /// A number representing the day of the week.
        /// e.g. if Monday is the first day of the week according to the
        /// relative culture, Monday returns 0, Tuesday returns 1, etc.
        /// </returns>
        public static int GetRelativeDayOfWeek(DateTime dt)
        {
            return ((int)dt.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7;
        }

        #region DateTime comparison methods

        /// <summary>
        /// Indicates if a given
        /// <see cref="T:System.DateTime"/>
        /// object represents a future instance when compared to another
        /// <see cref="T:System.DateTime"/>
        /// object.
        /// </summary>
        /// <param name="relative">Relative date and time.</param>
        /// <param name="given">Given date and time.</param>
        /// <returns>
        /// True if given date and time represents a future instance.
        /// </returns>
        public static bool IsFutureDateTime(DateTime relative, DateTime given)
        {
            return relative < given;
        }

        /// <summary>
        /// Indicates if a given
        /// <see cref="T:System.DateTime"/>
        /// object is at least one week behind from another
        /// <see cref="T:System.DateTime"/>
        /// object.
        /// </summary>
        /// <param name="relative">Relative date and time.</param>
        /// <param name="given">Given date and time.</param>
        /// <returns>
        /// True if given date and time is at least one week behind.
        /// </returns>
        public static bool IsAtLeastOneWeekOld(DateTime relative, DateTime given)
        {
            return ((int)(relative - given).TotalMinutes >= 7 * Day);
        }

        /// <summary>
        /// Indicates if a given
        /// <see cref="T:System.DateTime"/>
        /// object corresponds to a past day in the same week as another
        /// <see cref="T:System.DateTime"/>
        /// object.
        /// </summary>
        /// <param name="relative">Relative date and time.</param>
        /// <param name="given">Given date and time.</param>
        /// <returns>
        /// True if given date and time is a past day in the relative week.
        /// </returns>
        public static bool IsPastDayOfWeek(DateTime relative, DateTime given)
        {
            return GetRelativeDayOfWeek(relative) > GetRelativeDayOfWeek(given);
        }

        #endregion DateTime comparison methods

        #region Private Methods

        /// <summary>
        /// Returns a localized text string to express months in plural.
        /// </summary>
        /// <param name="month">Number of months.</param>
        /// <returns>Localized text string.</returns>
        private static string GetPluralMonth(int month)
        {
            if (month >= 2 && month <= 4)
            {
                return string.Format(CultureInfo.CurrentUICulture, "{0} ay önce", month);
            }
            else if (month >= 5 && month <= 12)
            {
                return string.Format(CultureInfo.CurrentUICulture, "{0} ay önce", month);
            }
            else
            {
                throw new ArgumentException("InvalidNumberOfMonths");
            }
        }

        private static string GetPluralTimeUnits(int units, string resource)
        {
            return string.Format(CultureInfo.CurrentUICulture, resource, units);
        }

        /// <summary>
        /// Returns a localized text string for the "ast" + "day of week as {0}".
        /// </summary>
        /// <param name="dow">Last Day of week.</param>
        /// <returns>Localized text string.</returns>
        private static string GetLastDayOfWeek(DayOfWeek dow)
        {
            string result;

            switch (dow)
            {
                case DayOfWeek.Monday:
                    result = "geçen Pazartesi";
                    break;

                case DayOfWeek.Tuesday:
                    result = "geçen Salı";
                    break;

                case DayOfWeek.Wednesday:
                    result = "geçen Çarşamba";
                    break;

                case DayOfWeek.Thursday:
                    result = "geçen Perşembe";
                    break;

                case DayOfWeek.Friday:
                    result = "geçen Cuma";
                    break;

                case DayOfWeek.Saturday:
                    result = "geçen Cumartesi";
                    break;

                case DayOfWeek.Sunday:
                default:
                    result = "geçen Pazar";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns a localized text string to express "on {0}"
        /// where {0} is a day of the week, e.g. Sunday.
        /// </summary>
        /// <param name="dow">Day of week.</param>
        /// <returns>Localized text string.</returns>
        private static string GetOnDayOfWeek(DayOfWeek dow)
        {
            string result;

            switch (dow)
            {
                case DayOfWeek.Monday:
                    result = "Pazartesi";
                    break;

                case DayOfWeek.Tuesday:
                    result = "Salı";
                    break;

                case DayOfWeek.Wednesday:
                    result = "Çarşamba";
                    break;

                case DayOfWeek.Thursday:
                    result = "Perşembe";
                    break;

                case DayOfWeek.Friday:
                    result = "Cuma";
                    break;

                case DayOfWeek.Saturday:
                    result = "Cumartesi";
                    break;

                case DayOfWeek.Sunday:
                default:
                    result = "Pazar";
                    break;
            }

            return result;
        }

        #endregion Private Methods
    }
}