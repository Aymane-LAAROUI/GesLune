using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using Dapper;

namespace GesLune.ViewModels
{
    public class ActeursViewModel : ViewModelBase
    {
        private IEnumerable<Model_Acteur> _acteurs = [];
        public IEnumerable<Model_Acteur> Acteurs
        {
            get => _acteurs;
            set
            {
                if (_acteurs != value)
                {
                    _acteurs = value;
                    OnPropertyChanged(nameof(Acteurs));
                }
            }
        }

        private IEnumerable<Model_Acteur_Type> _filtres = [];
        public IEnumerable<Model_Acteur_Type> Filtres
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

        private Model_Acteur_Type? _selectedFilter;
        public Model_Acteur_Type? SelectedFilter
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

        public ActeursViewModel(int selectedFiltreId = 0) 
        {
            LoadFiltres(selectedFiltreId);
            LoadData();
        }

        private void LoadFiltres(int selectedFiltreId = 0)
        {
            try
            {
                using var connection = new SqlConnection(
                    ConnectionString
                );
                string query = "SELECT * FROM Tble_Acteur_Types";

                IEnumerable<Model_Acteur_Type> filtres = [
                        new Model_Acteur_Type(0,"Tous","")
                    ];
                Filtres = filtres.Concat(connection.Query<Model_Acteur_Type>(query));
                SelectedFilter = Filtres.FirstOrDefault(e=> e.Acteur_Type_Id == selectedFiltreId);
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
                using var connection = new SqlConnection(
                    ConnectionString
                );
                string query;
                if (_selectedFilter == null || _selectedFilter.Acteur_Type_Id == 0)
                {
                    query = "SELECT * FROM Tble_Acteurs";

                }
                else
                {
                    query = "SELECT * FROM Tble_Acteurs WHERE Acteur_Type_Id = " + _selectedFilter.Acteur_Type_Id;
                }
                Acteurs = connection.Query<Model_Acteur>(query);
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
            string query = "DELETE FROM Tble_Acteurs WHERE Acteur_Id =@Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            LoadData();
        }
    }
}