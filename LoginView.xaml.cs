using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTodoListApp
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            //Environment.SetEnvironmentVariable("todoList_password", "test123", EnvironmentVariableTarget.User);
            //Environment.SetEnvironmentVariable("todoList_user", "brady", EnvironmentVariableTarget.User);

            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {
            string passwordEntered = PasswordBox.Password;
            string userEntered = UsernameBox.Text;

            string? envUsr = Environment.GetEnvironmentVariable("todoList_user");
            string? envPw = Environment.GetEnvironmentVariable("todoList_password");

            if (envPw != null && envUsr != null) {
                if (passwordEntered == envPw && userEntered == envUsr) {
                    Window window = Window.GetWindow(this);
                    window.Content = new TodoList();
                } else {
                    MessageBox.Show("Incorrect credentials entered!");
                }
            } else {
                MessageBox.Show("No user exists");
            }
        }

        private void OnPasswordChanged(object sender, EventArgs e) {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(PasswordBox.Password) && !string.IsNullOrEmpty(UsernameBox.Text);
        }

        private void OnUserChanged(object sender, EventArgs e) {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(PasswordBox.Password) && !string.IsNullOrEmpty(UsernameBox.Text);
        }
    }
}
