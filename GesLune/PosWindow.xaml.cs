using GesLune.Sdk.Models;
using GesLune.Sdk.ViewModels;
using System.Media;
using System.Windows;
using GesLune.KeyBoards;

namespace GesLune
{
    public partial class PosWindow : Window
    {
        private readonly PosViewModel _viewModel;
        public PosWindow()
        {
            InitializeComponent();
            _viewModel = (PosViewModel)DataContext;
            _viewModel.LigneAdded += (s, e) => SetQte();
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            //SystemSounds.Asterisk.Play();   // Information sound
        }

        private void SearchArticleButton_Click(object sender, RoutedEventArgs e)
        {
            ArabicKeyBoardWindow window = new();
            if (window.ShowDialog() == true)
            {
                _viewModel.SearchArticle = window.Query;
            }
        }

        private void PageSuivanteButton_Click(object sender, RoutedEventArgs e) => _viewModel.MoveNextPageArticle();

        private void PagePrecedenteButton_Click(object sender, RoutedEventArgs e) => _viewModel.MovePreviousPageArticle();

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

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                _viewModel.AddArticleWithCodeCommand.Execute(null);
            }
        }

        private void QteButton_Click(object sender, RoutedEventArgs e)
        {
            SetQte();
        }

        private void QuitterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetQte()
        {
            NumericKeyBoardWindow window = new(1);
            if (window.ShowDialog() == true) _viewModel.SetQuantity(window.Query);
        }
    }
}