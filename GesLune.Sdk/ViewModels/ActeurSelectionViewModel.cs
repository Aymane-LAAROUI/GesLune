using GesLune.Sdk.Commands;
using GesLune.Sdk.Models;
using GesLune.Sdk.Repositories;

namespace GesLune.Sdk.ViewModels
{
    public class ActeurSelectionViewModel : ViewModelBase
    {
        public IEnumerable<Model_Acteur> Acteurs { get; set; } = [];
        public NavigationCommand SaisieNavigationCommand { get; private set; }
        private readonly int Acteur_Type_Id;
        public ActeurSelectionViewModel(int Acteur_Type_Id)
        {
            this.Acteur_Type_Id = Acteur_Type_Id;
            LoadData();
            SaisieNavigationCommand = new(SaisieNavigate, CanSaisieNavigate);
        }
        private bool CanSaisieNavigate(object? obj) => true;

        private void SaisieNavigate(object? obj)
        {
            //Model_Acteur model = 
            //    new()
            //    {
            //        Acteur_Type_Id = this.Acteur_Type_Id,
            //    };
            //ActeurSaisieWindow saisieWindow = new(model);
            //saisieWindow.ShowDialog();
            //LoadData();
        }

        private void LoadData()
        {
            if (Acteur_Type_Id == 0)
                Acteurs = ActeurRepository.GetAll();
            else
                Acteurs = ActeurRepository.GetByTypeId(Acteur_Type_Id);
            OnPropertyChanged(nameof(Acteurs));
        }
    }
}
