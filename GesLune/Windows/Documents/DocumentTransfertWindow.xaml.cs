using GesLune.Models;
using GesLune.ViewModels.Documents;
using System.Windows;

namespace GesLune.Windows.Documents
{
    public partial class DocumentTransfertWindow : Window
    {
        private readonly DocumentTransfertViewModel viewModel;
        public DocumentTransfertWindow(Model_Document model)
        {
            InitializeComponent();
            viewModel = new(model);
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Transferer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
