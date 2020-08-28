using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using WpfSamples.Notifications;
using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace WpfSamples.Actions
{
    class ShowSubWindowAction : TriggerAction<DependencyObject>
    {
        private static readonly ConcurrentDictionary<Type, Func<Window>> activators = new ConcurrentDictionary<Type, Func<Window>>();
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }
            var notification = args.Context as SubWindowOpenNotification;

            var activator = activators.GetOrAdd(notification.ContentType, t =>
            {
                var constructor = t.GetConstructor(Type.EmptyTypes);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<Window>>(
                        System.Linq.Expressions.Expression.New(constructor)
                ); 
                
                return lambda.Compile();
            });
            var window = activator();
            Action<IInteractionRequestAware> setNotificationAndClose = (iira) =>
            {
                iira.Notification = notification;
                iira.FinishInteraction = () => window.Close();
            };

            MvvmHelpers.ViewAndViewModelAction(window.Content, setNotificationAndClose);
            window.Show();
        }
    }
}
