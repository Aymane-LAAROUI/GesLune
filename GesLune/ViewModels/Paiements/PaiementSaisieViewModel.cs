using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows.Acteurs;
using System.Windows;

namespace GesLune.ViewModels.Paiements
{
    public class PaiementSaisieViewModel : ViewModelBase
    {
        public Model_Paiement Paiement { get; private set; }
        private Model_Acteur? _Acteur;
        public Model_Acteur? Selected_Acteur
        {
            get => _Acteur;
            set
            {
                if (value != null)
                {
                    _Acteur = value;
                    Paiement.Paiement_Acteur_Id = value.Acteur_Id;
                    Paiement.Paiement_Acteur_Nom = value.Acteur_Nom;
                    OnPropertyChanged(nameof(Selected_Acteur));
                }
            }
        }
        public PaiementSaisieViewModel(Model_Paiement model)
        {
            Paiement = model ?? new Model_Paiement();
        }

        public void Enregistrer()
        {
            try
            {
                Paiement = PaiementRepository.Enregistrer(Paiement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }

        public void SelectActeur()
        {
            ActeurSelectionWindow selectionWindow = new(0);
            if (selectionWindow.ShowDialog() == true)
            {
                if (selectionWindow.SelectedActeur is null)
                {
                    MessageBox.Show($"{selectionWindow.SelectedActeur?.Acteur_Id}");
                }
                Selected_Acteur = selectionWindow.SelectedActeur;
            }
        }
    }
}
