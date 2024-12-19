using GesLune.Models;
using GesLune.ViewModels;
using System.Data;
using System.Windows;

namespace GesLune.Windows
{
    public partial class DocumentSaisieWindow : Window
    {
        private DocumentSaisieViewModel viewModel;
        public DocumentSaisieWindow(Model_Document? document = null)
        {
            InitializeComponent();
            viewModel = new(document);
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Enregistrer();
        }

        private void Supprimer_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
