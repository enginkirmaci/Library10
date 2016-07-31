using System;
using System.Diagnostics;
using Windows.Foundation.Metadata;

namespace Library10.Core.Development
{
    public class DevTools
    {
        public static bool isAttached
        {
            get
            {
                return Debugger.IsAttached;
            }
        }

        private static bool? _isMobile;
        public static bool IsMobile
        {
            get
            {
                if (!_isMobile.HasValue)
                    _isMobile = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");

                return _isMobile.Value;
            }
        }

        public static void WriteLine(object message)
        {
            Debug.WriteLine(message);
        }

        private static DateTime StartTime { get; set; }

        public static void StartTimer()
        {
            StartTime = DateTime.Now;
        }

        public static void StopTimer()
        {
            TimeSpan tsDuration = DateTime.Now.Subtract(StartTime);
            WriteLine(tsDuration.TotalMilliseconds.ToString());
        }
    }
}