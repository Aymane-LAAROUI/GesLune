using GesLune.Sdk.Models;
using GesLune.Sdk.ViewModels;
using GesLune.Windows;
using System.Media;
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

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            //SystemSounds.Asterisk.Play();   // Information sound
        }

        private void SearchArticleButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PageSuivanteButton_Click(object sender, RoutedEventArgs e) => _viewModel.MoveNextPage();

        private void PagePrecedenteButton_Click(object sender, RoutedEventArgs e) => _viewModel.MovePreviousPage();

        private void EspeceButton_Click(object sender, RoutedEventArgs e)
        {
            Model_Paiement_Type model = new()
            {
                Paiement_Type_Id = 1,
            };
            _viewModel.AddPaiementWithType(model);
            SystemSounds.Asterisk.Play();
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
            SystemSounds.Asterisk.Play();
        }

        private void AnnulerPaiementButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelPaiement();
            SystemSounds.Asterisk.Play();
        }
        private void ChoixClientButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ActeurSelectionWindow(2);
            window.ShowDialog();
            if (window.DialogResult != true) return;
            if (window.SelectedActeur == null) return;
            _viewModel.SelectClient(window.SelectedActeur);

        }

        private void PlusQteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddQuantity();
            SystemSounds.Asterisk.Play();
        }

        private void MoinsQteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MinusQuantity();
            SystemSounds.Asterisk.Play();
        }

        private void ImprimerButton_Click(object sender, RoutedEventArgs e) => _viewModel.Print();

        private void SupprimerLigneButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteLigne();
            SystemSounds.Asterisk.Play();
        }
    }
}
