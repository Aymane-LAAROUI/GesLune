using GesLune.Sdk.Commands;
using GesLune.Sdk.Models;
using GesLune.Sdk.Repositories;

namespace GesLune.Sdk.ViewModels
{
    public class PosViewModel : ViewModelBase
    {
        // -- FIELDS -- //
        private string _searchArticle = string.Empty;

        // -- PROPERTIES -- //
        public string Title { get; } = "PosLune";
        public List<Model_Article> Articles { get; set; } = [];
        public List<Model_Categorie> Categories { get; set; } = [];
        public Model_Categorie? SelectedCategorie { get; set; }
        public Model_Ticket Ticket { get; set; } = new();
        public List<Model_Document_Ligne> Lignes { get; set; } = [];
        public Model_Document_Ligne? SelectedLigne { get; set; }
        public List<Model_Paiement_Type> ModesPaiement { get; set; } = [];
        public int ArticlePage { get; set; }
        public int CategoriePage { get; set; }
        public string SearchArticle
        {
            get => _searchArticle;
            set
            {
                _searchArticle = value;
                if (string.IsNullOrEmpty(value))
                {
                    Articles.Clear();
                    OnPropertyChanged(nameof(SearchArticle));
                }
                else
                    Articles = ArticleRepository.GetByName(value);
                OnArticlesChanged();
            }
        }
        public double Total
        {
            get => Lignes.Sum(e => e.Document_Ligne_Total);
        }

        // -- COMMANDS -- //
        public GenericCommand<Model_Categorie> SelectCategorieCommand{ get; private set; }
        public GenericCommand<Model_Article> AddArticleCommand{ get; private set; }
        public RelayCommand DeleteLigneCommand { get; private set; }
        public RelayCommand AddArticleWithCodeCommand { get; private set; }

        // -- EVENTS -- //
        public event EventHandler? ArticlesChanged;
        public event EventHandler? CategoriesChanged;



        // -- CTOR -- //
        public PosViewModel()
        {
            InitCategories();
            InitModesPaiement();
            //InitTicket();
            SelectCategorieCommand = new(SelectCategorie);
            AddArticleCommand = new(AddLigne);
            DeleteLigneCommand = new(DeleteLigne);
            AddArticleWithCodeCommand = new(AddLigneByArticleCode);
        }




        // -- INIT METHODS -- //

        public void InitCategories()
        {
            CategoriePage = 1;
            Categories = CategorieRepository.GetAll(1,8);
            OnCategoriesChanged();
        }

        public void InitModesPaiement()
        {
            ModesPaiement = PaiementRepository.GetTypes();
        }

        public void InitTicket()
        {
            Model_Acteur? client = ActeurRepository.GetById(2);
            Ticket = new()
            {
                Document_Acteur_Id = 2,
                Document_Acteur_Nom = client?.Acteur_Nom ?? String.Empty,
                Document_Acteur_Adresse = client?.Acteur_Adresse,
            };
        }



        // -- EVENT HELPER METHODS -- //
        private void OnArticlesChanged()
        {
            OnPropertyChanged(nameof(Articles));
            ArticlesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnCategoriesChanged()
        {
            OnPropertyChanged(nameof(Categories));
            CategoriesChanged?.Invoke(this, EventArgs.Empty);
        }



        // -- METHODS -- //
        public void SelectClient(Model_Acteur client)
        {
            Ticket.Document_Acteur_Id = client.Acteur_Id;
            Ticket.Document_Acteur_Nom = client.Acteur_Nom;
            Ticket.Document_Acteur_Adresse = client.Acteur_Adresse;
            SaveTicket();
        }

        public void AddQuantity()
        {
            if (SelectedLigne == null) return;
            SelectedLigne.Document_Ligne_Quantity++;
            SelectedLigne.Document_Ligne_Total = SelectedLigne.Document_Ligne_Quantity * SelectedLigne.Document_Ligne_Prix_Unitaire;
            if (DocumentRepository.EnregistrerLigne(SelectedLigne) > 0)
                RefreshLignes();
        }

        public void MinusQuantity()
        {
            if (SelectedLigne == null) return ;
            if (SelectedLigne.Document_Ligne_Quantity > 0)
            {
                SelectedLigne.Document_Ligne_Quantity--;
                SelectedLigne.Document_Ligne_Total = SelectedLigne.Document_Ligne_Quantity * SelectedLigne.Document_Ligne_Prix_Unitaire;
                if (DocumentRepository.EnregistrerLigne(SelectedLigne) > 0)
                    RefreshLignes();
            }
            else
                DeleteLigne();
        }

        public void AddLigne(Model_Article article)
        {
            if (Ticket.Document_Id == 0) SaveTicket();
            Model_Document_Ligne ligne = new()
            {
                Document_Article_Id = article.Article_Id,
                Document_Ligne_Article_Nom = article.Article_Nom,
                Document_Ligne_Prix_Unitaire = article.Article_Prix,
                Document_Ligne_Quantity = 1,
                Document_Ligne_Total = article.Article_Prix,
                Document_Id = Ticket.Document_Id,
            };
            DocumentRepository.EnregistrerLigne(ligne);
            RefreshLignes();
            SelectedLigne = Lignes.OrderByDescending(e => e.Document_Ligne_Id).First();
            //MessageBox.Show($"{SelectedLigne.Document_Ligne_Id}");
            OnPropertyChanged(nameof(SelectedLigne));
        }

        public void AddLigneByArticleCode()
        {
            string code = SearchArticle.Trim();
            SearchArticle = string.Empty;
            if (string.IsNullOrEmpty(code)) return;
            Model_Article? article = ArticleRepository.GetByCode(code); 
            if (article == null)
            {
                OnExceptionThrown(new("Code article ou code à barre introuvable"));
                return;
            }
            AddLigne(article);
        }

        public void SelectCategorie(Model_Categorie categorie)
        {
            SelectedCategorie = categorie;
            ArticlePage = 1;
            SearchArticle = string.Empty;
            Articles = ArticleRepository.GetByCategorieId(categorie.Categorie_Id,ArticlePage,15);
            OnArticlesChanged();
            OnPropertyChanged(nameof(ArticlePage));
        }

        public void AddPaiementWithType(Model_Paiement_Type paiementType)
        {
            if (Ticket.Document_Id == 0) SaveTicket();
            Model_Paiement paiement = new()
            {
                Paiement_Type_Id = paiementType.Paiement_Type_Id,
                Paiement_Acteur_Id = Ticket.Document_Acteur_Id,
                Paiement_Acteur_Nom = Ticket.Document_Acteur_Nom,
                Paiement_Document_Id = Ticket.Document_Id,
                Paiement_Montant = Lignes.Sum(e => e.Document_Ligne_Total),
            };
            PaiementRepository.Enregistrer(paiement);
        }

        public void CancelPaiement()
        {
            PaiementRepository.DeleteByDocument_Id(Ticket.Document_Id);
        }

        public void Print()
        {
            //TODO
        }

        public void MoveNextPageArticle()
        {
            if (SelectedCategorie == null) return;
            ArticlePage++;
            Articles = ArticleRepository.GetByCategorieId(SelectedCategorie.Categorie_Id,ArticlePage,15);
            OnPropertyChanged(nameof(ArticlePage));
            OnArticlesChanged();
        }

        public void MovePreviousPageArticle()
        {
            if (SelectedCategorie == null) return;
            if (ArticlePage == 1) return;
            ArticlePage--;
            Articles = ArticleRepository.GetByCategorieId(SelectedCategorie.Categorie_Id, ArticlePage, 15);
            OnPropertyChanged(nameof(ArticlePage));
            OnArticlesChanged();
        }

        public void MoveNextPageCategorie()
        {
            //if (SelectedCategorie == null) return;
            CategoriePage++;
            Categories = CategorieRepository.GetAll(CategoriePage,8);
            OnPropertyChanged(nameof(CategoriePage));
            OnCategoriesChanged();
        }

        public void MovePreviousPageCategorie()
        {
            if (CategoriePage == 1) return;
            CategoriePage--;
            Categories = CategorieRepository.GetAll(CategoriePage, 8);
            OnPropertyChanged(nameof(CategoriePage));
            OnCategoriesChanged();
        }

        public void DeleteLigne()
        {
            if (SelectedLigne == null) return;
            DocumentRepository.DeleteLigne(SelectedLigne.Document_Ligne_Id);
            RefreshLignes();
        }



        // -- HELPER METHODS -- //
        private void RefreshLignes()
        {
            if (SelectedLigne == null)
                Lignes = DocumentRepository.GetLignes(Ticket.Document_Id);
            else
            {
                int ligne_id = SelectedLigne.Document_Ligne_Id;
                Lignes = DocumentRepository.GetLignes(Ticket.Document_Id);
                SelectedLigne = Lignes.Find(e => e.Document_Ligne_Id == ligne_id);
                OnPropertyChanged(nameof (SelectedLigne));
            }
            OnPropertyChanged(nameof (Lignes));
            OnPropertyChanged(nameof(Total));
        }

        private void SaveTicket()
        {
            var ticket = DocumentRepository.Enregistrer(Ticket);
            if (ticket != null)
            {
                Ticket = (Model_Ticket)ticket;
            }
            OnPropertyChanged(nameof (Ticket));
        }
    }
}
