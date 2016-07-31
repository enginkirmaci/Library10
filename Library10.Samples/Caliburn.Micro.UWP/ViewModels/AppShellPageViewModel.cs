using Caliburn.Micro.UWP.Controls;
using Caliburn.Micro.UWP.Views;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.UWP.ViewModels
{
    public class AppShellPageViewModel : Screen
    {
        private readonly WinRTContainer _container;
        private INavigationService _navigationService;

        public AppShellPageViewModel(WinRTContainer container)
        {
            _container = container;
        }

        public void SetupNavigationService(Frame frame)
        {
            _navigationService = _container.RegisterNavigationService(frame);
        }

        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item != null)
            {
                if (item.DestPage != null &&
                    item.DestPage != AppShellPage.Current.AppFrame.CurrentSourcePageType)
                {
                    var navigationManager = SystemNavigationManager.GetForCurrentView();
                    navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

                    if (item.DestPage == typeof(LandingView))
                        _navigationService.For<LandingViewModel>().Navigate();
                    else if (item.DestPage == typeof(BasicSubPage))
                        _navigationService.For<BasicSubPageViewModel>().Navigate();
                }
            }
        }
    }
}