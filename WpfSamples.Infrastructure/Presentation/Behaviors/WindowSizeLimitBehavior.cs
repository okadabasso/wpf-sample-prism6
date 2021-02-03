using System;
using System.Windows;
using System.Windows.Interactivity;

namespace WpfSamples.Infrastructure.Presentation.Behaviors
{
    public class WindowSizeLimitBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SizeChanged += OnWindowSizeChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.SizeChanged -= OnWindowSizeChanged;
        }

        //イベントハンドラ
        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var window = e.Source as Window;
            if (e.NewSize.Width > SystemParameters.WorkArea.Width)
            {
                window.MaxWidth = SystemParameters.WorkArea.Width;
            }
            if (e.NewSize.Height > SystemParameters.WorkArea.Height)
            {
                window.MaxHeight = SystemParameters.WorkArea.Height;
            }
        }
    }
}
