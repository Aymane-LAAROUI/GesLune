using GesLune.Models;
using System.Windows;
using GesLune.Repositories;
using GesLune.Commands;
using GesLune.Windows.Acteurs;

namespace GesLune.ViewModels.Acteurs
{
    public class ActeursViewModel : ViewModelBase
    {
        private IEnumerable<Model_Acteur> _acteurs = [];
        public IEnumerable<Model_Acteur> Acteurs
        {
            get => _acteurs;
            set
            {
                if (_acteurs != value)
                {
                    _acteurs = value;
                    OnPropertyChanged(nameof(Acteurs));
                }
            }
        }
        private IEnumerable<Model_Acteur_Type> _filtres = [];
        public IEnumerable<Model_Acteur_Type> Filtres
        {
            get => _filtres;
            set
            {
                if (value != _filtres)
                {
                    _filtres = value;
                    OnPropertyChanged(nameof(Filtres));
                }
            }
        }
        private Model_Acteur_Type? _selectedFilter;
        public Model_Acteur_Type? SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilter));
                    LoadData();
                }
            }
        }
        public Model_Acteur? Selected_Acteur { get; set; }
        public NavigationCommand SaisieNavigationCommand { get; private set; }

        public ActeursViewModel(int selectedFiltreId)
        {
            LoadFiltres(selectedFiltreId);
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }

        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            Model_Acteur model = Selected_Acteur
                ?? new()
                {
                    Acteur_Type_Id = SelectedFilter?.Acteur_Type_Id ?? 1
                };
            ActeurSaisieWindow saisieWindow = new(model);
            saisieWindow.ShowDialog();
            LoadData();
        }

        private void LoadFiltres(int selectedFiltreId)
        {
            try
            {
                IEnumerable<Model_Acteur_Type> filtres = [
                        new Model_Acteur_Type(0,"Tous","")
                    ];
                Filtres = filtres.Concat(ActeurRepository.GetTypes());
                SelectedFilter = Filtres.FirstOrDefault(e => e.Acteur_Type_Id == selectedFiltreId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadData()
        {
            try
            {
                Acteurs = _selectedFilter == null || _selectedFilter.Acteur_Type_Id == 0
                    ? ActeurRepository.GetAll()
                    : ActeurRepository.GetByTypeId(_selectedFilter.Acteur_Type_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(int id)
        {
            int res = ActeurRepository.Delete(id);
            LoadData();
        }

        public void ShowReleve()
        {
            if (Selected_Acteur == null) return;
            if (Selected_Acteur.Acteur_Id == 0) return;
            ActeurReleveWindow window = new(Selected_Acteur);
            //window.Acteur = Selected_Acteur;
            window.ShowDialog();
        }
    }
}