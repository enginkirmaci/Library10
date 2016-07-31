using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Library10.Core.UI
{
    public class VisualHelper
    {
        public static T FindVisualChildInsideFrame<T>(DependencyObject depObj, string frameName = null, string controlName = null) where T : FrameworkElement
        {
            var frame = FindVisualChild<Frame>(depObj, frameName);

            if (frame != null && frame.Content is Page)
            {
                //Quick Finding
                if (!string.IsNullOrWhiteSpace(controlName))
                {
                    var item = (frame.Content as Page).FindName(controlName);

                    if (item != null && item is T)
                        return item as T;
                }

                return FindVisualChild<T>(frame.Content as Page, controlName);
            }

            return null;
        }

        public static T FindVisualChild<T>(DependencyObject depObj, string controlName = null) where T : FrameworkElement
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child, controlName);

                    if (!string.IsNullOrWhiteSpace(controlName) && childItem != null)
                    {
                        if (((FrameworkElement)childItem).Name == controlName)
                            return childItem;
                    }
                    else if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }
    }
}