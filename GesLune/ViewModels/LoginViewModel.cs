using Dapper;
using GesLune.Models;
using GesLune.Windows;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Data;
using GesLune.Repositories;

namespace GesLune.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public event EventHandler? LoginSucceded;

        public void Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }
            Model_Utilisateur? utilisateur = UtilisateurRepository.Authenticate(username,password);
            if (utilisateur != null)
            {
                var mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                LoginSucceded?.Invoke(this, EventArgs.Empty);
            }

            else
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect!");
        }
    }
}
