using GesLune.Models;
using GesLune.Repositories;

namespace GesLune.ViewModels
{
    public class ArticleSelectionViewModel : ViewModelBase
    {
        public IEnumerable<Model_Article> Articles { get; set; }

        public ArticleSelectionViewModel()
        {
            Articles = ArticleRepository.GetAll();
            OnPropertyChanged(nameof(Articles));
        }
    }
}
