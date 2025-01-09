using GesLune.Models;
using GesLune.ViewModels.Articles;
using System.Windows;

namespace GesLune.Windows.Articles
{
    public partial class ArticleSaisieWindow : Window
    {
        private readonly ArticleSaisieViewModel viewModel;
        public ArticleSaisieWindow(Model_Article model)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteCode();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.AjouterCode();
        }
    }
}
