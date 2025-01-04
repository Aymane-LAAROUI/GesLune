using GesLune.Repositories;
using System.Data;

namespace GesLune.ViewModels.Articles
{
    public class ArticleFicheStockViewModel : ViewModelBase
    {
        public DataTable FicheStock {  get; set; }
        public ArticleFicheStockViewModel(int Article_Id) 
        {
            FicheStock = ArticleRepository.GetFicheStock(Article_Id);
            OnPropertyChanged(nameof(FicheStock));
        }
    }
}
