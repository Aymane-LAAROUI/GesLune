using GesLune.ViewModels.Stocks;
using System.Windows;

namespace GesLune.Windows.Articles
{
    public partial class ArticleFicheStockWindow : Window
    {
        private readonly ArticleFicheStockViewModel viewModel;
        public ArticleFicheStockWindow(int Article_Id)
        {
            InitializeComponent();
            viewModel = new(Article_Id);
            this.DataContext = viewModel;
        }
    }
}
