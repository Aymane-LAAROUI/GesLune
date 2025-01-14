using GesLune.Sdk.Commands;
using GesLune.Sdk.Models;
using GesLune.Sdk.Repositories;

namespace GesLune.Sdk.ViewModels
{
    public class PosViewModel : ViewModelBase
    {

        // -- PROPERTIES -- //
        public string Title { get; } = "PosLune";
        public List<Model_Article> Articles { get; set; } = [];
        public List<Model_Categorie> Categories { get; set; } = [];
        public Model_Categorie? SelectedCategorie { get; set; }
        public Model_Ticket Ticket { get; set; } = new();
        public List<Model_Document_Ligne> Lignes { get; set; } = [];
        public Model_Document_Ligne? SelectedLigne { get; set; }
        public List<Model_Paiement_Type> ModesPaiement { get; set; } = [];
        public int PageActuelle { get; set; }

        // -- COMMANDS -- //
        public GenericCommand<Model_Categorie> SelectCategorieCommand{ get; set; }
        public GenericCommand<Model_Article> AddArticleCommand{ get; set; }


        // -- CTOR -- //
        public PosViewModel()
        {
            InitCategories();
            InitModesPaiement();
            //InitTicket();
            SelectCategorieCommand = new(SelectCategorie);
            AddArticleCommand = new(AddLigne);
        }

        // -- INIT METHODS -- //

        public void InitCategories()
        {
            Categories = CategorieRepository.GetAll();
        }

        public void InitModesPaiement()
        {
            ModesPaiement = PaiementRepository.GetTypes();
        }

        public void InitTicket()
        {
            Ticket = new();
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
            DocumentRepository.EnregistrerLigne(SelectedLigne);
            OnPropertyChanged(nameof(SelectedLigne));
        }

        public void MinusQuantity()
        {
            if (SelectedLigne == null) return ;
            if (SelectedLigne.Document_Ligne_Quantity > 0)
            {
                SelectedLigne.Document_Ligne_Quantity--;
                SelectedLigne.Document_Ligne_Total = SelectedLigne.Document_Ligne_Quantity * SelectedLigne.Document_Ligne_Prix_Unitaire;
                DocumentRepository.EnregistrerLigne(SelectedLigne);
                OnPropertyChanged(nameof(SelectedLigne));
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
        }

        public void SelectCategorie(Model_Categorie categorie)
        {
            SelectedCategorie = categorie;
            PageActuelle = 1;
            Articles = ArticleRepository.GetByCategorieId(categorie.Categorie_Id,PageActuelle,15);
            //NombrePages = Articles.Count / 15;
            OnPropertyChanged(nameof(Articles));
            OnPropertyChanged(nameof(PageActuelle));
            //OnPropertyChanged(nameof(NombrePages));
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

        public void MoveNextPage()
        {
            if (SelectedCategorie == null) return;
            PageActuelle++;
            Articles = ArticleRepository.GetByCategorieId(SelectedCategorie.Categorie_Id,PageActuelle,15);
            OnPropertyChanged(nameof(PageActuelle));
            OnPropertyChanged(nameof(Articles));
        }

        public void MovePreviousPage()
        {
            if (SelectedCategorie == null) return;
            if (PageActuelle == 1) return;
            PageActuelle--;
            Articles = ArticleRepository.GetByCategorieId(SelectedCategorie.Categorie_Id, PageActuelle, 15);
            OnPropertyChanged(nameof(PageActuelle));
            OnPropertyChanged(nameof(Articles));
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
            Lignes = DocumentRepository.GetLignes(Ticket.Document_Id);
            OnPropertyChanged(nameof (Lignes));
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
