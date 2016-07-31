using Caliburn.Micro.UWP.ViewModels;
using Caliburn.Micro.UWP.Views;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Caliburn.Micro.UWP
{
    sealed partial class App
    {
        private WinRTContainer _container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container
                .PerRequest<AppShellPageViewModel>()
                .PerRequest<BasicSubPageViewModel>()
                .PerRequest<LandingViewModel>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootViewFor<AppShellPageViewModel>();

            AppShellPage shell = Window.Current.Content as AppShellPage;
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
    }
}