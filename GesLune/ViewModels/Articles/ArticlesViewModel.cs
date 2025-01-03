using GesLune.Commands;
using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Articles;
using System.Windows;

namespace GesLune.ViewModels.Articles
{
    public class ArticlesViewModel : ViewModelBase
    {
        public List<Model_Article> Articles { get; set; } = [];
        public Model_Article? Selected_Article { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public ArticlesViewModel()
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
            try
            {
                Articles = ArticleRepository.GetAll();
                OnPropertyChanged(nameof(Articles));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(int id)
        {
            int res = ArticleRepository.Delete(id);
            LoadData();
        }

    }
}
