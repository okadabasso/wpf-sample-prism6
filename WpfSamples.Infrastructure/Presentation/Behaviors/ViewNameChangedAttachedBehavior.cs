using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfSamples.Infrastructure.Presentation.Behaviors
{
    public class ViewNameChangedAttachedBehavior
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static string GetViewName(DependencyObject obj)
        {
            return (string)obj.GetValue(ViewNameProperty);
        }

        public static void SetViewName(DependencyObject obj, string value)
        {
            obj.SetValue(ViewNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for Close.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewNameProperty =
            DependencyProperty.RegisterAttached(
                "ViewName",
                typeof(string),
                typeof(ViewNameChangedAttachedBehavior),
                new PropertyMetadata("", OnViewNameChanged));

        private static void OnViewNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if (!window.IsActive)
                {
                    return;
                }
                if (double.IsNaN(window.Left) && double.IsNaN(window.Top))
                {
                    return;
                }
                var left = (SystemParameters.WorkArea.Width - window.Width) / 2;
                var top = (SystemParameters.WorkArea.Height - window.Height) / 2;
                if(left == window.Left && top == window.Top)
                {
                    return;
                }

                window.Dispatcher.BeginInvoke(new Action(() => {
                    window.Visibility = Visibility.Hidden;

                }), DispatcherPriority.Send);

                window.Dispatcher.BeginInvoke(new Action(() => {
                    try
                    {
                        Thread.Sleep(100);
                        if (window.Width > SystemParameters.WorkArea.Width)
                        {
                            window.Width = SystemParameters.WorkArea.Width;
                        }
                        if (window.Height > SystemParameters.WorkArea.Height)
                        {
                            window.Height = SystemParameters.WorkArea.Height;
                        }
                        window.Left = (SystemParameters.WorkArea.Width - window.Width) / 2;
                        window.Top = (SystemParameters.WorkArea.Height - window.Height) / 2;

                        window.Visibility = Visibility.Visible;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }

                }), DispatcherPriority.ApplicationIdle);

            }
        }
    }
}
