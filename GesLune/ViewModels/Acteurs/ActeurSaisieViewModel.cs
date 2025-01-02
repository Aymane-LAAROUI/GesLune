using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Acteurs
{
    public class ActeurSaisieViewModel : ViewModelBase
    {
        public Model_Acteur Acteur { get; private set; }
        private List<Model_Ville> _Villes = [];
        public List<Model_Ville> Villes
        {
            get => _Villes;
            set
            {
                _Villes = value;
                OnPropertyChanged(nameof(Villes));
            }
        }
        public Model_Ville? Selected_Ville
        {
            get => Villes.Find(e => e.Ville_Id == Acteur.Acteur_Ville_Id);
            set
            {
                if (value != null)
                    Acteur.Acteur_Ville_Id = value.Ville_Id;
            }
        }
        public ActeurSaisieViewModel(Model_Acteur model)
        {
            Acteur = model ?? new Model_Acteur();
            LoadVilles();
        }

        public void Enregistrer()
        {
            try
            {
                Acteur = ActeurRepository.Enregistrer(Acteur);
                // Afficher un message de succès
                //MessageBox.Show("Opération réussie", "Succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }
        private void LoadVilles()
        {
            Villes = VilleRepository.GetAll();
            Selected_Ville = Villes.FirstOrDefault();
        }
    }
}
