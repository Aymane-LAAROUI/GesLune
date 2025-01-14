using GesLune.Sdk.ViewModels;
using GesLune.Sdk.Models;
using System.Windows;

namespace GesLune
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;
        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = (LoginViewModel) DataContext;
            _viewModel.LoginSucceded += ViewModel_LoginSucceded;
            _viewModel.LoginFailed += ViewModel_LoginFailed;
        }

        private void ViewModel_LoginFailed(object? sender, string e)
        {
            throw new NotImplementedException();
        }

        private void ViewModel_LoginSucceded(object? sender, Model_Utilisateur e)
        {
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;
        }
    }
}
