using GesLune.Models;
using GesLune.ViewModels.Utilisateurs;
using System.Windows;

namespace GesLune.Windows.Utilisateurs
{
    /// <summary>
    /// Logique d'interaction pour UtilisateurSaisieWindow.xaml
    /// </summary>
    public partial class UtilisateurSaisieWindow : Window
    {
        private readonly UtilisateurSaisieViewModel viewModel;
        public UtilisateurSaisieWindow(Model_Utilisateur model)
        {
            InitializeComponent();
            viewModel = new(model);
            this.DataContext = viewModel;
        }

        private void Enregistrer_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Enregistrer();
        }

        private void Fermer_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
