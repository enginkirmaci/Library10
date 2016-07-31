using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Library10.Core.UI
{
    public interface IDialogService
    {
        Task ShowAsync(string content, string title, string okButtonResource);

        Task ShowAsync(string content, string title, params UICommand[] commands);
    }
}