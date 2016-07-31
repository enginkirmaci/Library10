namespace Caliburn.Micro.UWP.ViewModels
{
    public class LandingViewModel : Screen
    {
        public string PageHeaderText { get { return "Hello"; } set { var asd = value; } }

        public LandingViewModel(WinRTContainer container)
        {
        }
    }
}