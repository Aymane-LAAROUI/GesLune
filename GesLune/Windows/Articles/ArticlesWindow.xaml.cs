using GesLune.Models;
using GesLune.ViewModels.Articles;
using System.Windows;

namespace GesLune.Windows.Articles
{
    public partial class ArticlesWindow : Window
    {
        private readonly ArticlesViewModel viewModel;
        public ArticlesWindow()
        {
            InitializeComponent();
            viewModel = new();
            MainDataGrid.Items.Clear();
            this.DataContext = viewModel;
        }

        private void Fermer_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Supprimer_Button_Click(object sender, RoutedEventArgs e)
        {
            // Check if a row is selected in the DataGrid
            if (MainDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get the selected row as DataRowView
            if (MainDataGrid.SelectedItem is not Model_Article selectedModel)
            {
                MessageBox.Show("Erreur lors de la sélection de la ligne.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirm deletion
            var result = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            // Get the DataRow and remove it from the DataTable
            //var dataRow = selectedRowView.Row;
            //int id = Convert.ToInt32(dataRow["Article_Id"]);
            viewModel.Delete(selectedModel.Article_Id);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.AfficherFicheStock();
        }
    }
}
