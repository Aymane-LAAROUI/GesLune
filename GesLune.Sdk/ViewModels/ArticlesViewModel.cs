using GesLune.Sdk.Commands;
using GesLune.Sdk.Models;
using GesLune.Sdk.Repositories;
using System.Windows;

namespace GesLune.Sdk.ViewModels.Articles
{
    public class ArticlesViewModel : ViewModelBase
    {
        public List<Model_Article> Articles { get; set; } = [];
        public Model_Article? Selected_Article { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }
        public NavigationCommand LoadDataCommand { get; private set; }

        public ArticlesViewModel()
        {
            //LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
            LoadDataCommand = new(LoadData,(e)=>true);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            //Model_Article model = Selected_Article ?? new();
            //ArticleSaisieWindow saisieWindow = new(model);
            //saisieWindow.ShowDialog();
            //LoadData();
        }

        private void LoadData(object? rech = null)
        {
            try
            {
                if(rech == null) 
                    Articles = ArticleRepository.GetAll();
                else
                    Articles = ArticleRepository.GetByName((string)rech);

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

        public void AfficherFicheStock()
        { 
            //if (Selected_Article == null) return;
            //if (!Selected_Article.Article_Stockable) return;
            //new ArticleFicheStockWindow(Selected_Article.Article_Id).ShowDialog();
        }

    }
}
