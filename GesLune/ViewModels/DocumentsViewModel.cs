using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace GesLune.ViewModels
{
    public class DocumentsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadData()
        {
            using var connection = new SqlConnection(
                "Data Source=localhost;Initial Catalog=GesLune;User ID=sa;Password=admin@123456;TrustServerCertificate=True"
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
                "Data Source=localhost;Initial Catalog=GesLune;User ID=sa;Password=admin@123456;TrustServerCertificate=True"
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
