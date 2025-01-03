using GesLune.Models;
using GesLune.Repositories;
using System.Data;

namespace GesLune.ViewModels.Acteurs
{
    public class ActeurReleveViewModel : ViewModelBase
    {
        public DataTable Releve {  get; set; }
        private Model_Acteur _Acteur;
        public ActeurReleveViewModel(Model_Acteur Acteur)
        {
            _Acteur = Acteur;
            Releve = ActeurRepository.GetReleve(_Acteur.Acteur_Id);
            OnPropertyChanged(nameof(Releve));
        }
    }
}
