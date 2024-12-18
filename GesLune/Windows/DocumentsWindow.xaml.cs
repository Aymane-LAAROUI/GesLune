﻿using GesLune.ViewModels;
using System.Data;
using System.Windows;

namespace GesLune.Windows
{
    public partial class DocumentsWindow : Window
    {
        private DocumentsViewModel viewModel;
        public DocumentsWindow()
        {
            InitializeComponent();
            viewModel = new();
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
            if (MainDataGrid.SelectedItem is not DataRowView selectedRowView)
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
            var dataRow = selectedRowView.Row;
            int id = Convert.ToInt32(dataRow["Document_Id"]);
            viewModel.Delete(id);
        }

        private void Ouvrir_Button_Click(object sender, RoutedEventArgs e)
        {
            // Check if a row is selected in the DataGrid
            if (MainDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Get the selected row as DataRowView
            if (MainDataGrid.SelectedItem is not DataRowView selectedRowView)
            {
                MessageBox.Show("Erreur lors de la sélection de la ligne.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get the DataRow and remove it from the DataTable
            var dataRow = selectedRowView.Row;
            var saisieWindow = new DocumentSaisieWindow(dataRow);
            saisieWindow.ShowDialog();
        }

        private void Nouveau_Button_Click(object sender, RoutedEventArgs e)
        {
            var saisieWindow = new DocumentSaisieWindow();
            saisieWindow.ShowDialog();
        }
    }
}
