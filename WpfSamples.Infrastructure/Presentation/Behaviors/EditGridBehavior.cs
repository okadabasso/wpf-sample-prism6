using NLog;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfSamples.Infrastructure.Presentation.Behaviors
{
    public class EditGridBehavior
    {
        private readonly static ILogger logger = LogManager.GetCurrentClassLogger();
        // 添付プロパティの初期値は false。
        // コールバックを指定しておく。
        public static readonly DependencyProperty EditGrid = DependencyProperty.RegisterAttached("EditGrid",
            typeof(bool),
            typeof(EditGridBehavior),
            new UIPropertyMetadata(false, KeyEventChanged));
        public static bool GetEditGrid(DependencyObject obj)
        {
            return (bool)obj.GetValue(EditGrid);
        }
        public static void SetEditGrid(DependencyObject obj, bool value)
        {
            obj.SetValue(EditGrid, value);
        }
        public static void KeyEventChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DataGrid element = sender as DataGrid;
            if (element == null)
            {
                return;
            }
            if (GetEditGrid(element))
            {
                element.PreviewKeyDown += DataGrid_PreviewKeyDown;
            }
            else
            {
                element.PreviewKeyDown -= DataGrid_PreviewKeyDown;
            }
        }
        private static void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            logger.Trace("EditGridBehavior.DataGrid_PreviewKeyDown");
            var dataGrid = sender as DataGrid;
            switch (e.Key)
            {
                case Key.Up:
                case Key.Left:
                case Key.Right:
                case Key.Down:
                case Key.LeftCtrl:
                case Key.RightCtrl:
                case Key.LeftShift:
                case Key.RightShift:
                    break;
                case Key.Tab:
                    dataGrid.CommitEdit();
                    break;
                default:
                    logger.Trace($"{e.Key}");
                    dataGrid.BeginEdit(e);
                    break;
            }
        }
    }
}
