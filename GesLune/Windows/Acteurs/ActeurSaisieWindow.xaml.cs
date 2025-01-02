using GesLune.Models;
using GesLune.ViewModels.Acteurs;
using System.Windows;

namespace GesLune.Windows.Acteurs
{
    /// <summary>
    /// Logique d'interaction pour ActeurSaisieWindow.xaml
    /// </summary>
    public partial class ActeurSaisieWindow : Window
    {
        private readonly ActeurSaisieViewModel viewModel;
        public ActeurSaisieWindow(Model_Acteur model)
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
