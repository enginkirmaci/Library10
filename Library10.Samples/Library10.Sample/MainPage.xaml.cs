using Library10.Core.UI;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Library10.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            AppSettings.ApplicationSettings.StartCount = 15;

            DialogHelper.Show(AppSettings.General.AppName);
            AppSettings.RateReminderShown = true;
            Debug.WriteLine(AppSettings.RateReminderShown);

            DialogHelper.Show("button test", "TEST!", new UICommand[] {
                new UICommand()
                {
                    Id = 0,
                    Label = "Yes",
                    Invoked = new UICommandInvokedHandler(cmd => TEST(cmd))
                },
                new UICommand()
                {
                    Id = 1,
                    Label = "No",
                    Invoked = new UICommandInvokedHandler(cmd => TEST(cmd))
                }
            });
        }

        private void TEST(IUICommand cmd)
        {
            Debug.WriteLine(cmd.Label);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var test = AppSettings.ApplicationSettings.StartCount;
        }
    }
}