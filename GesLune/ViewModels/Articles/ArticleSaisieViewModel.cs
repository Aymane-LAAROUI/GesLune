using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Articles
{
    public class ArticleSaisieViewModel : ViewModelBase
    {
        public Model_Article Article { get; private set; }
        public ArticleSaisieViewModel(Model_Article model)
        {
            Article = model ?? new Model_Article();
        }

        public void Enregistrer()
        {
            try
            {
                Article = ArticleRepository.Enregistrer(Article);
                // Afficher un message de succès
                MessageBox.Show("Opération réussie", "Succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }
    }
}
