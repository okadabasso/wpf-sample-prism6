using System.Windows;

namespace WpfSamples.Views
{
    /// <summary>
    /// Interaction logic for LayoutCheckWindow.xaml
    /// </summary>
    public partial class LayoutCheckWindow : Window
    {
        public LayoutCheckWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.NewSize.Width > SystemParameters.WorkArea.Width)
            {
                MaxWidth = SystemParameters.WorkArea.Width;
            }
            if (e.NewSize.Height > SystemParameters.WorkArea.Height)
            {
                MaxHeight= SystemParameters.WorkArea.Height;
            }

        }
    }
}
