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
    public class ShowPopupWindowAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }
            var notification = args.Context as PopupNotification;

            var window = new PopupWindow();
            Action<IInteractionRequestAware> setNotificationAndClose = (iira) =>
            {
                iira.Notification = notification;
                iira.FinishInteraction = () =>
                {
                    args.Callback();
                    window.Close();
                    window = null;
                };

            };

            MvvmHelpers.ViewAndViewModelAction(window.Content, setNotificationAndClose);
            window.Show();
        }
    }
}
