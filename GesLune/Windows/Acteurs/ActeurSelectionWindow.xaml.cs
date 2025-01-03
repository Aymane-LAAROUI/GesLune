using GesLune.Models;
using GesLune.ViewModels.Acteurs;
using System.Windows;

namespace GesLune.Windows.Acteurs
{
    public partial class ActeurSelectionWindow : Window
    {
        private readonly ActeurSelectionViewModel viewModel;
        public Model_Acteur? SelectedActeur {  get; private set; }
        public ActeurSelectionWindow(int Acteur_Type_Id)
        {
            InitializeComponent();
            viewModel = new(Acteur_Type_Id);
            this.DataContext = viewModel;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.SelectedItem != null && MainDataGrid.SelectedItem is Model_Acteur model)
            {
                SelectedActeur = model;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
