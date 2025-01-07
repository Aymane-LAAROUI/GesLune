using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Articles
{
    public class ArticleSaisieViewModel : ViewModelBase
    {
        public Model_Article Article { get; private set; }
        public List<Model_Categorie> Categories { get; set; }
        private Model_Categorie _Categorie { get; set; }
        public Model_Categorie Selected_Categorie
        {
            get => _Categorie;
            set
            {
                _Categorie = value;
                Article.Article_Categorie_Id = value.Categorie_Id;
                OnPropertyChanged(nameof(Selected_Categorie));
            }
        }
        public ArticleSaisieViewModel(Model_Article? model)
        {
            Article = model ?? new Model_Article();
            Categories = CategorieRepository.GetAll();
            if (model == null || model.Article_Categorie_Id == 0)
            {
                Selected_Categorie = Categories.First();
                MessageBox.Show("ra 5awi");
            }
            else
            {
                Selected_Categorie = Categories.Find(e => e.Categorie_Id == model.Article_Categorie_Id)!;
                MessageBox.Show($"{Selected_Categorie.Categorie_Nom}");
            }
            OnPropertyChanged(nameof(Selected_Categorie));
        }

        public void Enregistrer()
        {
            try
            {
                Article.Article_Categorie_Id = Selected_Categorie.Categorie_Id;
                MessageBox.Show($"{Selected_Categorie.Categorie_Id}");
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
