using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using Dapper;

namespace GesLune.ViewModels
{
    public class DocumentsViewModel : ViewModelBase
    {
        public ObservableCollection<Model_Document> _documents = [];

        public ObservableCollection<Model_Document> Documents
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

        public DocumentsViewModel() 
        {
            //_documents = new DataTable();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using var connection = new SqlConnection(
                    ConnectionString
                );
                string query = "SELECT * FROM Tble_Documents";
                List<Model_Document> models =  connection.Query<Model_Document>( query ).ToList();
                _documents.Clear();
                foreach (var model in models)
                {
                    _documents.Add(model);
                }
                //var command = new SqlCommand(query, connection);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(
                ConnectionString
                );
            connection.Open();
            string query = $"DELETE FROM Tble_Document_Lignes WHERE Document_Id = @Id; DELETE FROM Tble_Documents WHERE Document_Id =@Id";
            using var command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            LoadData();
        }

    }
}
