using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Categories
{
    public class CategorieSaisieViewModel : ViewModelBase
    {
        public Model_Categorie Categorie { get; private set; }

        public CategorieSaisieViewModel(Model_Categorie model)
        {
            Categorie = model ?? new Model_Categorie();
        }

        public void Enregistrer()
        {
            try
            {
                Categorie = CategorieRepository.Enregistrer(Categorie);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }
    }
}
