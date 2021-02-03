using System.Windows;
using WpfSamples.Infrastructure.Presentation.Behaviors;

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

            var windowSizeLimiter = new WindowSizeLimitBehavior();
            windowSizeLimiter.Attach(this);

        }

    }
}
