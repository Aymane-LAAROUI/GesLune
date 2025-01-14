using GesLune.Sdk.Commands;
using GesLune.Sdk.Models;
using GesLune.Sdk.Repositories;
using System.Windows;

namespace GesLune.Sdk.ViewModels
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
        public RelayCommand SaisieNavigationCommand { get; private set; }
        public NavigationCommand SelectCategorieCommand { get; private set; }

        public CategoriesViewModel()
        {
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
            SelectCategorieCommand = new(OnSelectCategorie, (e) => true);
        }

        private void OnSelectCategorie(object? obj)
        {
            if (obj is Model_Categorie category)
            {
                Selected_Categorie = category;
                // You can add additional logic here, such as navigation or logging
            }
        }

        private bool CanSaisieNavigate() => true;

        private void SaisieNavigate()
        {
            //Model_Categorie model = Selected_Categorie
            //    ?? new()
            //    {

            //    };
            //CategorieSaisieWindow saisieWindow = new(model);
            //saisieWindow.ShowDialog();
            //LoadData();
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
