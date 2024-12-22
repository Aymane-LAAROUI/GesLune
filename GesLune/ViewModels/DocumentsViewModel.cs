using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using Dapper;
using System.Linq;

namespace GesLune.ViewModels
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
        public DocumentsViewModel() 
        {
            //_documents = new DataTable();
            LoadFiltres();
            LoadData();
        }

        public void LoadFiltres() 
        {
            try
            {
                using var connection = new SqlConnection(
                    ConnectionString
                );
                string query = "SELECT * FROM Tble_Document_Types";

                IEnumerable<Model_Document_Type> filtres = [
                        new Model_Document_Type()
                        {
                            Document_Type_Id = 0,
                            Document_Type_Nom = "Tous",
                            Document_Type_Nom_Abrege = ""
                        }
                    ];

                Filtres = filtres.Concat(connection.Query<Model_Document_Type>(query));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void ApplyFilter()
        //{
        //    if (SelectedFilter != null)
        //    {
        //        // Example: Access other properties for filtering or logic
        //        var id = SelectedFilter.Document_Type_Id;
        //        var abbr = SelectedFilter.Document_Type_Nom_Abrege;

        //        // Implement your filtering logic here, e.g., update another collection or UI elements

        //        System.Diagnostics.Debug.WriteLine($"Selected ID: {id}, Abbreviation: {abbr}");
        //    }
        //}

        public void LoadData()
        {
            try
            {
                using var connection = new SqlConnection(
                    ConnectionString
                );
                string query;
                if (_selectedFilter == null || _selectedFilter.Document_Type_Id == 0)
                {
                    query = "SELECT * FROM Tble_Documents";

                }
                else
                {
                    query = "SELECT * FROM Tble_Documents WHERE Document_Type_Id = " + _selectedFilter.Document_Type_Id;
                }
                Documents =  connection.Query<Model_Document>( query );
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
