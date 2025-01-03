using GesLune.Models;
using GesLune.Repositories;
using System.Windows;

namespace GesLune.ViewModels.Documents
{
    public class DocumentTransfertViewModel : ViewModelBase
    {
        Model_Document Document { get; set; }
        private List<Model_Document_Type> _Document_Types = [];
        public List<Model_Document_Type> Document_Types
        {
            get => _Document_Types;
            set
            {
                _Document_Types = value;
                OnPropertyChanged(nameof(Document_Types));
            }
        }
        public Model_Document_Type SelectedDocumentType { get; set; }

        public DocumentTransfertViewModel(Model_Document document)
        {
            //MessageBox.Show("HEHE BOI");
            Document = document;
            Document_Types = DocumentRepository.GetTypes();
            SelectedDocumentType = Document_Types.First();
        }

        public void Transferer()
        {
            // LOAD THE DATA TO INSERT
            var document = Document;
            document.Document_Type_Id = SelectedDocumentType.Document_Type_Id;
            var documentLignes = DocumentRepository.GetLignes(document.Document_Id);

            // REMOVE ALL PKs
            document.Document_Id = 0;
            document.Document_Num = string.Empty;

            // SAVE THE DOCUMENT FIRST
            document = DocumentRepository.Enregistrer(document);

            // RETREIVE DOCUMENT ID TO SAVE THE LINE    
            foreach (var item in documentLignes)
            {
                // RESET THE LIGNE ID TO INSERT 
                item.Document_Ligne_Id = 0;
                item.Document_Id = document.Document_Id;
                DocumentRepository.EnregistrerLigne(item);
            }
            MessageBox.Show("Done!");
        }
    }
}
