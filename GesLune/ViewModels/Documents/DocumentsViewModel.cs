using GesLune.Models;
using System.Windows;
using GesLune.Repositories;
using GesLune.Windows;
using System.Diagnostics;

namespace GesLune.ViewModels.Documents
{
    public class DocumentsViewModel : ViewModelBase
    {
        public IEnumerable<Model_Document> _documents = [];
        public IEnumerable<Model_Document> Documents
        {
            get => _documents;
            set
            {
                if (_documents != value)
                {
                    _documents = value;
                    OnPropertyChanged(nameof(Documents));
                }
            }
        }

        private IEnumerable<Model_Document_Type> _filtres = [];
        public IEnumerable<Model_Document_Type> Filtres
        {
            get => _filtres;
            set
            {
                if (value != _filtres)
                {
                    _filtres = value;
                    OnPropertyChanged(nameof(Filtres));
                }
            }
        }

        private Model_Document_Type? _selectedFilter;
        public Model_Document_Type? SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilter));
                    LoadData();
                }
            }
        }
        public DocumentsViewModel(int selectedFiltreId = 0)
        {
            //_documents = new DataTable();
            LoadFiltres(selectedFiltreId);
            LoadData();
        }
        public void LoadFiltres(int selectedFiltreId)
        {
            try
            {

                IEnumerable<Model_Document_Type> filtres = [
                        new Model_Document_Type()
                        {
                            Document_Type_Id = 0,
                            Document_Type_Nom = "Tous",
                            Document_Type_Nom_Abrege = ""
                        }
                    ];

                Filtres = filtres.Concat(DocumentRepository.GetTypes());
                SelectedFilter = Filtres.FirstOrDefault(e => e.Document_Type_Id == selectedFiltreId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData()
        {
            try
            {
                Documents = _selectedFilter == null || _selectedFilter.Document_Type_Id == 0
                    ? DocumentRepository.GetAll()
                    : DocumentRepository.GetByTypeId(_selectedFilter.Document_Type_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Ajouter()
        {
            Model_Document model = new()
            {
                Document_Type_Id = SelectedFilter?.Document_Type_Id ?? 0,
            };
            var saisieWindow = new DocumentSaisieWindow(model);
            saisieWindow.ShowDialog();
            LoadData();
        }
        public void Delete(int id)
        {
            if (DocumentRepository.EstEncaisse(id))
            {
                MessageBox.Show("Déjà encaissée!");
                return;
            }
            int res1 = DocumentRepository.DeleteDocumentLignes(id);
            int res2 = DocumentRepository.Delete(id);
            MessageBox.Show("Opération réussie");
            LoadData();
        }

    }
}
