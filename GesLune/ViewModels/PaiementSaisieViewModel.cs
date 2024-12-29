using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels
{
    public class PaiementSaisieViewModel : ViewModelBase
    {
        public Model_Paiement Paiement { get; private set; }
        private List<Model_Acteur> _Acteurs = [];
        public List<Model_Acteur> Acteurs
        {
            get => _Acteurs;
            set
            {
                _Acteurs = value;
                OnPropertyChanged(nameof(Acteurs));
            }
        }
        public Model_Acteur? Selected_Acteur
        {
            get => Acteurs.Find(e => e.Acteur_Id == Paiement.Paiement_Acteur_Id);
            set
            {
                if (value != null)
                {
                    Paiement.Paiement_Acteur_Id = value.Acteur_Id;
                    Paiement.Paiement_Acteur_Nom = value.Acteur_Nom;
                }
            }
        }
        public PaiementSaisieViewModel(Model_Paiement model)
        {
            Paiement = model ?? new Model_Paiement();
            LoadActeurs();
        }

        public void Enregistrer()
        {
            try
            {
                Paiement = PaiementRepository.Enregistrer(Paiement);
                // Afficher un message de succès
                MessageBox.Show("Opération réussie", "Succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }
        private void LoadActeurs()
        {
            Acteurs = ActeurRepository.GetAll(10);
            Selected_Acteur = Acteurs.FirstOrDefault();
        }
    }
}
