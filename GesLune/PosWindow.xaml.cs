using GesLune.Sdk.Models;
using GesLune.Sdk.ViewModels;
using GesLune.Windows;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace GesLune
{
    public partial class PosWindow : Window
    {
        private readonly PosViewModel _viewModel;
        public PosWindow()
        {
            InitializeComponent();
            _viewModel = new PosViewModel();
            this.DataContext = _viewModel;
            RefreshArticleButtons();
            RefreshCategorieButtons();
            _viewModel.CategoriesChanged += (s,e) => RefreshCategorieButtons();
            _viewModel.ArticlesChanged += (s,e) => RefreshArticleButtons();
            _viewModel.ExceptionThrown += (s, e) => MessageBox.Show(e.Message);
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            //SystemSounds.Asterisk.Play();   // Information sound
        }

        private void SearchArticleButton_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void CategoriePageSuivanteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveNextPageCategorie();
        }

        private void CategoriePagePrecedenteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MovePreviousPageCategorie();
        }

        private void RefreshCategorieButtons()
        {
            CategoriesGrid.Children.Clear();
            //List<Button> CategorieButtons = [];
            foreach (var categorie in _viewModel.Categories)
            {
                Button button = new()
                {
                    Margin = new Thickness(5),
                    Content = categorie.Categorie_Nom
                };
                button.Click += (s, e) => _viewModel.SelectCategorie(categorie);
                CategoriesGrid.Children.Add(button);
                //CategorieButtons.Add(button);
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) _viewModel.AddArticleWithCodeCommand.Execute(null);
        }

        private void RefreshArticleButtons()
        {
            ArticlesGrid.Children.Clear();
            foreach (var article in _viewModel.Articles)
            {
                Button button = new()
                {
                    Margin = new Thickness(5),
                    Content = article.Article_Nom
                };
                button.Click += (s, e) => _viewModel.AddArticleCommand.Execute(article);
                ArticlesGrid.Children.Add(button);
            }
        }
    }
}
