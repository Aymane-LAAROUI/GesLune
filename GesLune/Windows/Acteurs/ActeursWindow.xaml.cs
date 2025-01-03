using GesLune.Models;
using GesLune.ViewModels.Acteurs;
using System.Windows;

namespace GesLune.Windows
{
    public partial class ActeursWindow : Window
    {
        private readonly ActeursViewModel viewModel;

        public ActeursWindow(int selectedFiltreId)
        {
            InitializeComponent();
            viewModel = new(selectedFiltreId);
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
            if (MainDataGrid.SelectedItem is not Model_Acteur selectedModel)
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
            viewModel.Delete(selectedModel.Acteur_Id);
        }

        private void Releve_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowReleve();
        }
    }
}
