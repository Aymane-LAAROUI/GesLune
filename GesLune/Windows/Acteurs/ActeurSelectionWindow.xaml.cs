using GesLune.Models;
using GesLune.ViewModels.Acteurs;
using System.Windows;

namespace GesLune.Windows.Acteurs
{
    /// <summary>
    /// Logique d'interaction pour ActeurSelectionWindow.xaml
    /// </summary>
    public partial class ActeurSelectionWindow : Window
    {
        private readonly ActeurSelectionViewModel viewModel;
        public Model_Acteur? SelectedActeur {  get; private set; }
        public ActeurSelectionWindow()
        {
            InitializeComponent();
            viewModel = new();
            //viewModel.SelectedActeurChanged += ViewModel_SelectedActeurChanged;
            this.DataContext = viewModel;
        }

        //private void ViewModel_SelectedActeurChanged(object? sender, Model_Acteur? e)
        //{
        //    SelectedActeur = e;
        //    this.DialogResult = true;
        //    this.Close();
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.SelectedItem != null && MainDataGrid.SelectedItem is Model_Acteur model)
            {
                SelectedActeur = model;
                //viewModel.Select(model);
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
