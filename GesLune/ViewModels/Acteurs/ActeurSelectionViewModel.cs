using GesLune.Models;
using GesLune.Repositories;

namespace GesLune.ViewModels.Acteurs
{
    public class ActeurSelectionViewModel : ViewModelBase
    {
        public IEnumerable<Model_Acteur> Acteurs { get; set; }
        //public Model_Acteur? SelectedActeur { get; set; }
        //public event EventHandler<Model_Acteur?>? SelectedActeurChanged;

        public ActeurSelectionViewModel(int Acteur_Type_Id = 0)
        {
            if (Acteur_Type_Id == 0)
                Acteurs = ActeurRepository.GetAll();
            else
                Acteurs = ActeurRepository.GetByTypeId(Acteur_Type_Id);
            OnPropertyChanged(nameof(Acteurs));
        }

        //public void Select(Model_Acteur model)
        //{
        //    SelectedActeur = model;
        //    SelectedActeurChanged?.Invoke(this, model);
        //}
    }
}
