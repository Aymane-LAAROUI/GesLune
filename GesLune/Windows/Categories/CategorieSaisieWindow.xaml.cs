using GesLune.Models;
using GesLune.ViewModels.Categories;
using System.Windows;

namespace GesLune.Windows.Categories
{
    /// <summary>
    /// Logique d'interaction pour CategorieSaisieWindow.xaml
    /// </summary>
    public partial class CategorieSaisieWindow : Window
    {
        private readonly CategorieSaisieViewModel viewModel;
        public CategorieSaisieWindow(Model_Categorie model)
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
