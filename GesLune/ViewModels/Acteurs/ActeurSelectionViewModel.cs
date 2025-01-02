using GesLune.Models;
using GesLune.Repositories;

namespace GesLune.ViewModels.Acteurs
{
    public class ActeurSelectionViewModel : ViewModelBase
    {
        public IEnumerable<Model_Acteur> Acteurs { get; set; }
        //public Model_Acteur? SelectedActeur { get; set; }
        //public event EventHandler<Model_Acteur?>? SelectedActeurChanged;

        public ActeurSelectionViewModel()
        {
            Acteurs = ActeurRepository.GetAll();
            OnPropertyChanged(nameof(Acteurs));
        }

        //public void Select(Model_Acteur model)
        //{
        //    SelectedActeur = model;
        //    SelectedActeurChanged?.Invoke(this, model);
        //}
    }
}
