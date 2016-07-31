//using Caliburn.Micro;
//using System.Net.NetworkInformation;
//using System.Runtime.CompilerServices;

//namespace Library10.Core.BaseClasses
//{
//    public class BaseViewModel : Screen
//    {
//        protected bool HasNetwork { get { return NetworkInterface.GetIsNetworkAvailable(); } }

//        protected bool IsLoaded { get; set; }

//        protected readonly INavigationService NavigationService;

//        public BaseViewModel(INavigationService navigationService)
//        {
//            this.NavigationService = navigationService;
//        }

//        protected override void OnViewAttached(object view, object context)
//        {
//            base.OnViewAttached(view, context);

//            IsLoaded = false;
//        }

//        protected override void OnViewLoaded(object view)
//        {
//            base.OnViewLoaded(view);

//            IsLoaded = true;
//        }

//        protected override void OnDeactivate(bool close)
//        {
//            base.OnDeactivate(close);
//        }

//        public bool Set<T>(ref T storage, T value, [CallerMemberName()]string propertyName = null)
//        {
//            if (!object.Equals(storage, value))
//            {
//                storage = value;
//                NotifyOfPropertyChange(propertyName);
//                return true;
//            }
//            return false;
//        }
//    }
//}