using GesLune.Sdk.Models;
using GesLune.Sdk.ViewModels;
using System.Windows;

namespace GesLune
{
    public partial class PosWindow : Window
    {
        private readonly PosViewModel _viewModel;
        public PosWindow()
        {
            InitializeComponent();
            _viewModel = (PosViewModel)DataContext;
        }

        private void SearchArticleButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PageSuivanteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveNextPage();
        }

        private void PagePrecedenteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MovePreviousPage();
        }

        private void EspeceButton_Click(object sender, RoutedEventArgs e)
        {
            Model_Paiement_Type model = new()
            {
                Paiement_Type_Id = 1,
            };
            _viewModel.AddPaiementWithType(model);
        }

        private void CreditButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ChequeButton_Click(object sender, RoutedEventArgs e)
        {
            Model_Paiement_Type model = new()
            {
                Paiement_Type_Id = 3,
            };
            _viewModel.AddPaiementWithType(model);
        }

        private void AnnulerPaiementButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelPaiement();
        }
    }
}
