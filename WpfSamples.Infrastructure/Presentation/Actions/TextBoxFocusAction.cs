using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Windows.Controls;
using WpfSamples.Infrastructure.Presentation.Views;


namespace WpfSamples.Infrastructure.Presentation.Actions
{
    public class TextBoxFocusAction : TriggerAction<TextBox>
    {
        protected override void Invoke(object parameter)
        {
            var textBox = AssociatedObject;
            textBox.SelectAll();
        }
    }
}
