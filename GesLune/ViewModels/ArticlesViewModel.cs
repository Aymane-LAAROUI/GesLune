using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels
{
    public class ArticlesViewModel : ViewModelBase
    {
        public List<Model_Article> Articles { get; set; } = [];
        
        public ArticlesViewModel() 
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Articles = ArticleRepository.GetAll();
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
