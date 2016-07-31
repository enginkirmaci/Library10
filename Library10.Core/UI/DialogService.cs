using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Library10.Core.UI
{
    public class DialogService : IDialogService
    {
        private bool _open = false;

        public async Task ShowAsync(string content, string title, string okButtonResource)
        {
            await ShowAsync(content, title, new UICommand() { Label = okButtonResource });
        }

        public async Task ShowAsync(string content, string title = default(string), params UICommand[] commands)
        {
            while (_open)
            {
                await Task.Delay(1000);
            }
            _open = true;
            var dialog = (title == default(string)) ? new MessageDialog(content) : new MessageDialog(content, title);
            if (commands != null && commands.Any())
                foreach (var item in commands)
                    dialog.Commands.Add(item);
            await dialog.ShowAsync();
            _open = false;
        }
    }
}