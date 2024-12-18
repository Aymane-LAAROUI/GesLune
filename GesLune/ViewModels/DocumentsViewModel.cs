using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;

namespace GesLune.ViewModels
{
    public class DocumentsViewModel : ViewModelBase
    {
        public DataTable _data;

        public DataTable Data
        {
            get => _data;
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

        public DocumentsViewModel() 
        {
            _data = new DataTable();
            LoadData();
        }

        public void LoadData()
        {
            using var connection = new SqlConnection(
                ConnectionString
                );
            var adapter = new SqlDataAdapter();
            try
            {
                string query = "SELECT * FROM Tble_Documents";
                var command = new SqlCommand(query, connection);
                adapter.SelectCommand = command;
                Data.Clear();
                adapter.Fill(Data);
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
            string query = $"DELETE FROM Tble_Documents WHERE Document_Id =@Id";
            using var command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            LoadData();
        }

    }
}
