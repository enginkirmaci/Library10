namespace Caliburn.Micro.UWP.ViewModels
{
    public class BasicSubPageViewModel : Screen
    {
        public string Message { get; set; }

        public BasicSubPageViewModel(WinRTContainer container)
        {
            Message = "Hello Sub Page";
        }
    }
}