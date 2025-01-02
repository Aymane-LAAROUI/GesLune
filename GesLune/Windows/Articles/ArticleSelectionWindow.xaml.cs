using GesLune.Models;
using GesLune.ViewModels.Articles;
using System.Windows;

namespace GesLune.Windows.Articles
{
    /// <summary>
    /// Logique d'interaction pour ArticleSelectionWindow.xaml
    /// </summary>
    public partial class ArticleSelectionWindow : Window
    {
        private readonly ArticleSelectionViewModel viewModel;
        public IEnumerable<Model_Article> SelectedArticles { get; private set; } = [];
        public ArticleSelectionWindow()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{MainDataGrid.SelectedItems} mnha {MainDataGrid.SelectedItems.Count}");

            if (MainDataGrid.SelectedItems != null && MainDataGrid.SelectedItems.Count > 0)
            {
                // Convert SelectedItems to a list of Model_Article
                var models = MainDataGrid.SelectedItems.Cast<Model_Article>().ToList();

                SelectedArticles = models;

                // Perform any additional operations
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
