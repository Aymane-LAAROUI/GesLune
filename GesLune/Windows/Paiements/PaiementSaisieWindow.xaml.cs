using GesLune.Models;
using GesLune.ViewModels.Paiements;
using System.Windows;

namespace GesLune.Windows.Paiements
{
    /// <summary>
    /// Logique d'interaction pour PaiementSaisieWindow.xaml
    /// </summary>
    public partial class PaiementSaisieWindow : Window
    {
        private readonly PaiementSaisieViewModel viewModel;
        public PaiementSaisieWindow(Model_Paiement model)
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

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectActeur();
        }
    }
}
