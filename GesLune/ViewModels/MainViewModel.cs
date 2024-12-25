using GesLune.Commands;
using GesLune.Models;
using GesLune.Repositories;
using GesLune.Windows;

namespace GesLune.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IEnumerable<Model_Document_Type> _documentMenuItems = [];
        public IEnumerable<Model_Document_Type> DocumentMenuItems
        {
            get => _documentMenuItems;
            set
            {
                _documentMenuItems = value;
                OnPropertyChanged(nameof(DocumentMenuItems));
            }
        }

        private IEnumerable<Model_Acteur_Type> _acteurMenuItems = [];
        public IEnumerable<Model_Acteur_Type> ActeurMenuItems
        {
            get => _acteurMenuItems;
            set
            {
                _acteurMenuItems = value;
                OnPropertyChanged(nameof(ActeurMenuItems));
            }
        }

        public NavigationCommand ActeurNavigationCommand { get; private set; }
        public NavigationCommand DocumentNavigationCommand { get; private set; }

        public MainViewModel() 
        {
            LoadDocumentMenuItems();
            LoadActeurMenuItems();
            ActeurNavigationCommand = new(ActeurNavigate,CanActeurNavigate);
            DocumentNavigationCommand = new(DocumentNavigate, CanDocumentNavigate);
        }

        private void DocumentNavigate(object obj)
        {
            new DocumentsWindow((int)obj).ShowDialog();
        }

        private bool CanDocumentNavigate(object obj) => true;

        private void ActeurNavigate(object id)
        {
            new ActeursWindow((int)id).ShowDialog();
        }

        private bool CanActeurNavigate(object id) => true;

        private void LoadDocumentMenuItems()
        {
            DocumentMenuItems = DocumentRepository.GetTypes();
        }

        private void LoadActeurMenuItems()
        {
            ActeurMenuItems = ActeurRepository.GetTypes();
        }
    }
}