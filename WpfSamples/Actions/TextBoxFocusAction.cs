using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using WpfSamples.Notifications;
using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Windows.Controls;
using WpfSamples.Views;


namespace WpfSamples.Actions
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
