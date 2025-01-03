using GesLune.Commands;
using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Articles;

namespace GesLune.ViewModels.Articles
{
    public class ArticleSelectionViewModel : ViewModelBase
    {
        public IEnumerable<Model_Article> Articles { get; set; } = [];
        public Model_Article? Selected_Article { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public ArticleSelectionViewModel()
        {
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            Model_Article model = Selected_Article ?? new();
            ArticleSaisieWindow saisieWindow = new(model);
            saisieWindow.ShowDialog();
            LoadData();
        }

        private void LoadData()
        {
            Articles = ArticleRepository.GetAll();
            OnPropertyChanged(nameof(Articles));
        }
    }
}
