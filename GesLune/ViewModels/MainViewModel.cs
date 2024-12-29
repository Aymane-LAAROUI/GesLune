using GesLune.Commands;
using GesLune.Models;
using GesLune.Models.UI;
using GesLune.Repositories;
using GesLune.Windows;
using GesLune.Windows.Articles;

namespace GesLune.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private IEnumerable<Model_Document_Type> _documentMenuItems = [];
        //public IEnumerable<Model_Document_Type> DocumentMenuItems
        //{
        //    get => _documentMenuItems;
        //    set
        //    {
        //        _documentMenuItems = value;
        //        OnPropertyChanged(nameof(DocumentMenuItems));
        //    }
        //}

        //private IEnumerable<Model_Acteur_Type> _acteurMenuItems = [];
        //public IEnumerable<Model_Acteur_Type> ActeurMenuItems
        //{
        //    get => _acteurMenuItems;
        //    set
        //    {
        //        _acteurMenuItems = value;
        //        OnPropertyChanged(nameof(ActeurMenuItems));
        //    }
        //}

        public List<MenuItemModel> MenuItems { get; set; } = [];
        public NavigationCommand ActeurNavigationCommand { get; private set; }
        public NavigationCommand DocumentNavigationCommand { get; private set; }
        public NavigationCommand PaiementNavigationCommand {  get; private set; }

        public MainViewModel() 
        {
            //LoadDocumentMenuItems();
            //LoadActeurMenuItems();
            ActeurNavigationCommand = new(ActeurNavigate, CanActeurNavigate);
            DocumentNavigationCommand = new(DocumentNavigate, CanDocumentNavigate);
            PaiementNavigationCommand = new(PaiementNavigate, CanPaiementNavigate);
            LoadMenuItems();
        }

        private void DocumentNavigate(object? obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            new DocumentsWindow((int)obj).ShowDialog();
        }

        private bool CanDocumentNavigate(object? obj) => true;

        private void ActeurNavigate(object? id)
        {
            ArgumentNullException.ThrowIfNull(id);
            new ActeursWindow((int)id).ShowDialog();
        }

        private bool CanActeurNavigate(object? id) => true;
        
        private void ArticleNavigate(object? id)
        {
            new ArticlesWindow().ShowDialog();
        }

        private bool CanArticleNavigate(object? id) => true;

        private void PaiementNavigate(object? obj)
        {
            new PaiementsWindow().ShowDialog();
        }

        private bool CanPaiementNavigate(object? obj) => true;

        private void LoadMenuItems()
        {
            // Init the List
            MenuItems.Clear();

            // Init Top Main MenuItems
            MenuItemModel fichier = new() { Text = "Fichier"};
            MenuItemModel Parametrage = new() { Text = "Paramètrage" };
            MenuItemModel Traitement = new() { Text = "Traitement" };

            // Fill each Main MenuItem
            // Parametrage: (Acteurs)
            ActeurRepository.GetTypes().ForEach(
                e => Parametrage.Items.Add(
                    new MenuItemModel()
                    {
                        Text = e.Acteur_Type_Nom,
                        Command = ActeurNavigationCommand,
                        Tag = e.Acteur_Type_Id
                    }
                )
            );

            // Parametrage: (Articles)
            Parametrage.Items.Add(
                new MenuItemModel()
                {
                    Text = "Articles",
                    Command = new NavigationCommand(ArticleNavigate,CanArticleNavigate)
                }
            );

            // Traitement:
            DocumentRepository.GetTypes().ForEach(
                e => Traitement.Items.Add(
                    new MenuItemModel()
                    {
                        Text = e.Document_Type_Nom,
                        Command = DocumentNavigationCommand,
                        Tag = e.Document_Type_Id
                    }
                )    
            );
            Traitement.Items.Add(
                new MenuItemModel()
                {
                    Text = "Paiements",
                    Command= PaiementNavigationCommand,
                }
                );

            // Add Main MenuItems Into the List
            MenuItems.Add( fichier );
            MenuItems.Add( Parametrage );
            MenuItems.Add( Traitement );

            //Notify the UI
            OnPropertyChanged(nameof(MenuItems));
        }
    }
}