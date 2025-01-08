using GesLune.Repositories;
using System.Data;

namespace GesLune.ViewModels.Stocks
{
    public class EtatStockViewModel : ViewModelBase
    {
        //public int Etat_Stock_Article_Id { get; set; }
        //public string Etat_Stock_Article_Nom {  get; set; } = string.Empty;
        //public int Etat_Stock_Quantity { get; set; }
        public DataTable EtatStock { get; set; }
        public EtatStockViewModel()
        {
            EtatStock = ArticleRepository.GetEtatStock();
            OnPropertyChanged(nameof(EtatStock));
        }

    }
}
