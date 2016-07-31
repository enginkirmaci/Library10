//using Caliburn.Micro;
//using System.Net.NetworkInformation;
//using System.Runtime.CompilerServices;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Navigation;

//namespace Library10.Core.BaseClasses
//{
//    public class BaseShellViewModel : Screen
//    {
//        protected static NavigationMode NavigationMode = NavigationMode.New;
//        protected readonly WinRTContainer Container;
//        protected INavigationService NavigationService;

//        protected bool HasNetwork { get { return NetworkInterface.GetIsNetworkAvailable(); } }

//        public BaseShellViewModel(WinRTContainer container)
//        {
//            Container = container;
//        }

//        protected bool Set<T>(ref T storage, T value, [CallerMemberName()]string propertyName = null)
//        {
//            if (!object.Equals(storage, value))
//            {
//                storage = value;
//                NotifyOfPropertyChange(propertyName);
//                return true;
//            }
//            return false;
//        }

//        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
//        {
//            NavigationMode = e.NavigationMode;
//        }

//        protected virtual void SetupNavigationService(Frame frame)
//        {
//            NavigationService = Container.RegisterNavigationService(frame);
//            NavigationService.Navigating += NavigationService_Navigating;
//        }
//    }
//}