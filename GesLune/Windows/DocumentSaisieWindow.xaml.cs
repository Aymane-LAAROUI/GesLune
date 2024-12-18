using GesLune.ViewModels;
using System.Data;
using System.Windows;

namespace GesLune.Windows
{
    public partial class DocumentSaisieWindow : Window
    {
        private DocumentSaisieViewModel viewModel;
        public DocumentSaisieWindow(DataRow? ligne = null)
        {
            InitializeComponent();
            viewModel = new(ligne ?? new DataTable().NewRow());
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Enregistrer();
        }
    }
}
