using Dapper;
using GesLune.Models;
using GesLune.Windows;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Data;

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
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            Object parameters = new 
            {
                Utilisateur_Login = username,
                Utilisateur_Password = password
            };
            Model_Utilisateur? utilisateur = connection.QueryFirstOrDefault<Model_Utilisateur>
                ("sp_authenticate_utilisateur",parameters,commandType: CommandType.StoredProcedure);
            if (utilisateur != null)
            {
                var mainWindow = new DocumentsWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                LoginSucceded?.Invoke(this, EventArgs.Empty);
            }

            else
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect!");
        }
    }
}
