using System;
using System.Security.Principal;
using System.Windows;

namespace WpfSamples.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            loginId.Focus();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoginTrigger_Click(object sender, RoutedEventArgs e)
        {
            messageText.Text = "";
            if (loginId.Text != "test" || password.Password != "test")
            {
                messageText.Text = "ID test, Password test でログインできます。";
                password.Password = "";
                return;
            }
            // 仮の認証情報セット
            var identity = new GenericIdentity(loginId.Text);
            var principal = new GenericPrincipal(identity, null);
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
            this.DialogResult = true;

        }

        private void CancelTrigger_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }
    }
}
