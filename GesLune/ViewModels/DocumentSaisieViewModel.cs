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
                    _document = value;
                    OnPropertyChanged(nameof(Document));
                }
            }
        }

        private ObservableCollection<Model_Document_Ligne> _lignes;
        public ObservableCollection<Model_Document_Ligne> Lignes
        {
            get => _lignes;
            set => _lignes = value;
        }

        public DocumentSaisieViewModel(Model_Document? document)
        {
            _document = document ?? new Model_Document();
            _lignes = [];
            LoadLignes();
        }

        public void LoadLignes()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Tble_Document_Lignes WHERE Document_Id = " + _document.Document_Id;
            _lignes = new(
                connection.Query<Model_Document_Ligne>(query)
                );
        }

        public void Enregistrer()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            // Check if the document exists
            string verifQuery = "SELECT COUNT(Document_Id) FROM Tble_Documents WHERE Document_Id = @Document_Id";
            using var verifCommand = new SqlCommand(verifQuery, connection);
            verifCommand.Parameters.AddWithValue("@Document_Id", _document.Document_Id);
            int exists = (int)verifCommand.ExecuteScalar();

            // Construct the query dynamically based on existence
            string query;
            if (exists == 0)
            {
                query = "INSERT INTO Tble_Documents (" +
                        string.Join(", ", _document.GetType().GetProperties().Select(p => p.Name).Where(e => !e.Equals("Document_Id"))) +
                        ") VALUES (" +
                        string.Join(", ", _document.GetType().GetProperties().Select(p => "@" + p.Name).Where(e => !e.Equals("@Document_Id"))) + ")";
            }
            else
            {
                query = "UPDATE Tble_Documents SET " +
                        string.Join(", ", _document.GetType().GetProperties()
                            .Where(p => p.Name != nameof(Model_Document.Document_Id))
                            .Select(p => p.Name + " = @" + p.Name)) +
                        " WHERE Document_Id = @Document_Id";
            }

            // Create a dictionary of parameters
            var parameters = _document.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(_document) ?? String.Empty);

            // Create and execute the query
            using var command = new SqlCommand(query, connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
            }
            MessageBox.Show(query);
            int res = command.ExecuteNonQuery();
            MessageBox.Show($"{res}", "Succès");
        }


    }
}
