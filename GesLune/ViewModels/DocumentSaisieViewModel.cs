using Dapper;
using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace GesLune.ViewModels
{
    public class DocumentSaisieViewModel : ViewModelBase
    {
        private Model_Document _document;
        public Model_Document Document
        {
            get => _document;
            set
            {
                if (_document != value)
                {
                    if (value != null)
                    {
                        foreach (var property in value.GetType().GetProperties())
                        {
                            var propertyName = property.Name;
                            var propertyValue = property.GetValue(value); // Utiliser 'value' au lieu de '_document'
                            MessageBox.Show($"{propertyName} : {propertyValue}");
                        }
                        _document = value;
                        OnPropertyChanged(nameof(Document));
                    }
                }
                else
                {
                    MessageBox.Show("hwa hwa");
                }
            }
        }


        private ObservableCollection<Model_Document_Ligne> _lignes;
        public ObservableCollection<Model_Document_Ligne> Lignes
        {
            get => _lignes;
            set
            {
                if (_lignes != value)
                {
                    _lignes = value;
                    OnPropertyChanged(nameof(Lignes));
                }
            }
        }

        private List<Model_Document_Type> _document_types;

        public List<Model_Document_Type> Document_Types
        {
            get => _document_types;
            set
            {
                if (value != _document_types)
                {
                    _document_types = value;
                    OnPropertyChanged(nameof(Document_Types));
                }
            }
        }

        public Model_Document_Type? Selected_Type
        {
            get => _document_types.Find(e => e.Document_Type_Id == _document.Document_Type_Id);
            set
            {
                if (value  != null)
                Document.Document_Type_Id = value.Document_Type_Id;
            }
        }

        public DocumentSaisieViewModel(Model_Document? document = null)
        {
            _document = document ?? new Model_Document();
            _lignes = [];
            LoadDocumentTypes();
            LoadLignes();
        }

        private void LoadDocumentTypes()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            Document_Types = connection.Query<Model_Document_Type>("SELECT * FROM Tble_Document_Types").ToList();
        }

        public void LoadLignes()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Tble_Document_Lignes WHERE Document_Id = " + _document.Document_Id;
            Lignes = new(connection.Query<Model_Document_Ligne>(query));
        }

        public void Enregistrer()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            // Préparer les paramètres pour la procédure stockée
            var parameters = new DynamicParameters();
            foreach (var property in _document.GetType().GetProperties())
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(_document); //?? DBNull.Value
                parameters.Add("@" + propertyName, propertyValue);
                //MessageBox.Show($"{propertyName} : {propertyValue}");
            }

            try
            {
                // Appeler la procédure stockée
                var document_ = connection.QueryFirst<Model_Document>(
                    "sp_save_document", // Nom de la procédure stockée
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                if (document_ != null)
                {
                    MessageBox.Show($"nmra: {document_.Document_Num}");
                    Document = document_;
                }
                else MessageBox.Show("ra ja 5awi");

                // Afficher un message de succès
                MessageBox.Show("Opération réussie", "Succès");
            }
            catch (Exception ex)
            {
                // Gérer les erreurs et afficher un message
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur");
            }
        }

        public void EnregistrerLigne(Model_Document_Ligne ligne)
        {
            Enregistrer();
            ligne.Document_Id = Document.Document_Id;
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            // Check if the document exists
            string verifQuery = "SELECT COUNT(Document_Ligne_Id) FROM Tble_Document_Lignes WHERE Document_Ligne_Id = @Document_Ligne_Id";
            using var verifCommand = new SqlCommand(verifQuery, connection);
            verifCommand.Parameters.AddWithValue("@Document_Ligne_Id", ligne.Document_Ligne_Id);
            int exists = (int)verifCommand.ExecuteScalar();

            // Construct the query dynamically based on existence
            string query;
            if (exists == 0)
            {
                query = "INSERT INTO Tble_Document_Lignes (" +
                        string.Join(", ", ligne.GetType().GetProperties().Select(p => p.Name).Where(e => !e.Equals("Document_Ligne_Id"))) +
                        ") VALUES (" +
                        string.Join(", ", ligne.GetType().GetProperties().Select(p => "@" + p.Name).Where(e => !e.Equals("@Document_Ligne_Id"))) + ")";
            }
            else
            {
                query = "UPDATE Tble_Document_Lignes SET " +
                        string.Join(", ", ligne.GetType().GetProperties()
                            .Where(p => p.Name != nameof(Model_Document_Ligne.Document_Ligne_Id))
                            .Select(p => p.Name + " = @" + p.Name)) +
                        " WHERE Document_Ligne_Id = @Document_Ligne_Id";
            }

            // Create a dictionary of parameters
            var parameters = ligne.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(ligne) ?? String.Empty);

            // Create and execute the query
            using var command = new SqlCommand(query, connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                MessageBox.Show($"{param.Value}");
            }
            //MessageBox.Show(query);
            int res = command.ExecuteNonQuery();
            //MessageBox.Show($"{res}", "Succès");
            LoadLignes();
        }

        public void Delete(int id)
        {
            MessageBox.Show($"Supprimer {id}");
            using var connection = new SqlConnection(
                ConnectionString
            );
            connection.Open();
            string query = $"DELETE FROM Tble_Document_Lignes WHERE Document_Ligne_Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            LoadLignes();
        }
    }
}
