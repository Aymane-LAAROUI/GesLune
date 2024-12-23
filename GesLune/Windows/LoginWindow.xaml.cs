using GesLune.ViewModels;
using System.Windows;

namespace GesLune.Windows
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel viewModel;
        public LoginWindow()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
            viewModel.LoginSucceded += ViewModel_LoginSucceded;
        }

        private void ViewModel_LoginSucceded(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            viewModel.Login(username,password);
        }
    }
}
