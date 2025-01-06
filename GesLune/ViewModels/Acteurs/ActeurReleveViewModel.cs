using GesLune.Models;
using GesLune.Repositories;
using System.Data;

namespace GesLune.ViewModels.Acteurs
{
    public class ActeurReleveViewModel : ViewModelBase
    {
        private readonly Model_Acteur _Acteur;
        public DataTable Releve {  get; set; } = new();
        public DateTime DateDu { get; set; } = new(1900, 1, 1);
        public DateTime DateAu { get; set; } = DateTime.Now;
        public ActeurReleveViewModel(Model_Acteur Acteur)
        {
            _Acteur = Acteur;
            LoadData();
        }

        public void LoadData()
        {
            Releve = ActeurRepository.GetReleve(_Acteur.Acteur_Id,DateDu,DateAu);
            OnPropertyChanged(nameof(Releve));
        }

    }
}
