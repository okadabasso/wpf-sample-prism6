using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Windows.Controls;
using WpfSamples.Infrastructure.Presentation.Views;
using NLog;
using System.Windows.Threading;
namespace WpfSamples.Infrastructure.Presentation.Actions
{
    public class GridCellSelectAllAction : TriggerAction<TextBox>
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        protected override void Invoke(object parameter)
        {
            logger.Trace($"{nameof(GridCellSelectAllAction)} {nameof(Invoke)}");
            var textBox = AssociatedObject;
            logger.Trace($"text={textBox.Text}");
            textBox.SelectAll();

            logger.Trace($"{nameof(GridCellSelectAllAction)} {nameof(Invoke)}");
        }
    }
}
