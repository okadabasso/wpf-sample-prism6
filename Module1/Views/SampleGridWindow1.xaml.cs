using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NLog;
namespace Module1.Views
{
    /// <summary>
    /// Interaction logic for SampleGridWindow1.xaml
    /// </summary>
    public partial class SampleGridWindow1 : UserControl
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public SampleGridWindow1()
        {
            InitializeComponent();
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            logger.Trace($"{nameof(SampleGridWindow1)} {nameof(DataGrid_BeginningEdit)}");
            logger.Trace($"{sender}");
        }
        private static T FindVisualChild<T>(DependencyObject item)
          where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(item);
            var result = item as T;
            //the for-loop contains a null check; we stop when we find the result. 
            //so the stop condition for this method is embedded in the initialization
            //of the result variable.
            for (int i = 0; i < childCount && result == null; i++)
            {
                result = FindVisualChild<T>(VisualTreeHelper.GetChild(item, i));
            }
            return result;
        }

        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            logger.Trace($"{nameof(SampleGridWindow1)} {nameof(DataGrid_GotFocus)}");
            logger.Trace($"{sender}");

        }
    }
}
