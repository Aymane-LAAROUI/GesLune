using GesLune.Commands;
using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Categories;
using System.Windows;

namespace GesLune.ViewModels.Categories
{
    public class CategoriesViewModel : ViewModelBase
    {
        private IEnumerable<Model_Categorie> _Categories = [];
        public IEnumerable<Model_Categorie> Categories
        {
            get => _Categories;
            set
            {
                if (_Categories != value)
                {
                    _Categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }
        public Model_Categorie? Selected_Categorie { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public CategoriesViewModel()
        {
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            Model_Categorie model = Selected_Categorie
                ?? new()
                {

                };
            CategorieSaisieWindow saisieWindow = new(model);
            saisieWindow.ShowDialog();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                Categories = CategorieRepository.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(int id)
        {
            int res = CategorieRepository.Delete(id);
            LoadData();
        }
    }
}
