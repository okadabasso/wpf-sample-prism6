using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using WpfSamples.Infrastructure.Presentation.Notifications;

namespace WpfSamples.Infrastructure.Presentation.Actions
{
    public class ShowSubWindowAction : TriggerAction<DependencyObject>
    {
        private static readonly ConcurrentDictionary<Type, Func<Window>> activators = new ConcurrentDictionary<Type, Func<Window>>();
        private Func<Window> GetWindowActivator(Type contentType)
        {
            var activator = activators.GetOrAdd(contentType, t =>
            {
                var constructor = t.GetConstructor(Type.EmptyTypes);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<Window>>(
                        System.Linq.Expressions.Expression.New(constructor)
                );

                return lambda.Compile();
            });

            return activator;
        }

        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }
            var notification = args.Context as SubWindowOpenNotification;

            var activator = GetWindowActivator(notification.ContentType);
            var window = activator();
            Action<IInteractionRequestAware> setNotificationAndClose = (iira) =>
            {
                iira.Notification = notification;
                iira.FinishInteraction = () =>
                {
                    args.Callback();
                    window.Close();
                };
                
            };
            
            MvvmHelpers.ViewAndViewModelAction(window.Content, setNotificationAndClose);
            window.Show();
        }
    }
}
