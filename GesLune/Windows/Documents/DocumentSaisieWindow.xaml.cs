using GesLune.Models;
using GesLune.ViewModels.Documents;
using System.Windows;

namespace GesLune.Windows
{
    public partial class DocumentSaisieWindow : Window
    {
        private readonly DocumentSaisieViewModel viewModel;
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
            // Check if a row is selected in the DataGrid
            if (MainDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get the selected row as DataRowView
            if (MainDataGrid.SelectedItem is not Model_Document_Ligne selectedLine)
            {
                MessageBox.Show("Erreur lors de la sélection de la ligne.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirm deletion
            //var result = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.No)
            //{
            //    return;
            //}

            // Get the DataRow and remove it from the DataTable
            //var dataRow = selectedRowView.Row;
            //int id = Convert.ToInt32(dataRow["Document_Id"]);
            viewModel.Delete(selectedLine.Document_Ligne_Id);
        }

        private void MainDataGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            // Check if a row is selected in the DataGrid
            if (MainDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get the selected row as DataRowView
            if (MainDataGrid.SelectedItem is not Model_Document_Ligne selectedLine)
            {
                MessageBox.Show("Erreur lors de la sélection de la ligne.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Confirm deletion
            //var result = MessageBox.Show("Voulez-vous vraiment modifier cette ligne ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.No)
            //{
            //    return;
            //}

            Task.Run(
                () => viewModel.EnregistrerLigne(selectedLine)
                );
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectActeur();
        }

        private void Rechercher_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RechercherArticles();
        }

        private void Encaisser_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Encaisser();
        }

        private void Transferer_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Transferer();
        }
    }
}