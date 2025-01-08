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

        private Model_Paiement_Type _Selected_Type;
        public Model_Paiement_Type Selected_Type
        {
            get => _Selected_Type;
            set
            {
                if (value != null)
                {
                    _Selected_Type = value;
                    Paiement.Paiement_Type_Id = value.Paiement_Type_Id;
                    OnPropertyChanged(nameof(Selected_Type));
                }
            }
        }
        public List<Model_Paiement_Type> Types { get; private set; }
        public PaiementSaisieViewModel(Model_Paiement model)
        {
            Paiement = model ?? new Model_Paiement();
            LoadTypes();
            _Selected_Type = Types.Find(e => e.Paiement_Type_Id == Paiement.Paiement_Type_Id) ?? Types.First();
        }

        public void LoadTypes()
        {
            Types = PaiementRepository.GetTypes();
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
