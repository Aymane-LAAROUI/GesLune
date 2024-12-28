using GesLune.Models;
using GesLune.ViewModels;
using System.Windows;

namespace GesLune.Windows.Articles
{
    /// <summary>
    /// Logique d'interaction pour ArticleSaisieWindow.xaml
    /// </summary>
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
    }
}
