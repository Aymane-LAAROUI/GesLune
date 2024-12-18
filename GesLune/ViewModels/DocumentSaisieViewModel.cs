using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GesLune.ViewModels
{
    public class DocumentSaisieViewModel(DataRow dataRow) : ViewModelBase
    {
        private DataRow _data = dataRow;
        public DataRow Data
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
        public object Num
        {
            get => _data["Document_Num"];
            set
            {
                _data["Document_Num"] = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public object Date
        {
            get => _data["Document_Date"];
            set
            {
                _data["Document_Date"] = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public object Client
        {
            get => _data["Document_Nom_Client"];
            set
            {
                _data["Document_Nom_Client"] = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public object Adresse
        {
            get => _data["Document_Adresse_Client"];
            set
            {
                _data["Document_Adresse_Client"] = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        public void Enregistrer()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string verif_query = $"SELECT count(Document_Id) FROM Tble_Documents WHERE Document_Id={_data["Document_Id"]}";
            using var command = new SqlCommand(verif_query,connection);
            int existe = (int) command.ExecuteScalar();
            // MessageBox.Show($"{existe}");
            string query = (existe == 0)
                ? $"INSERT INTO Tble_Documents ({string.Join(", ", dataRow.Table.Columns)}) VALUES ({string.Join(", ", dataRow.Table.Columns.Cast<DataColumn>().Select(col => "@" + col.ColumnName))})"
                : $"UPDATE Tble_Documents SET {string.Join(", ", dataRow.Table.Columns.Cast<DataColumn>()
                .Where(e => !e.ColumnName.Equals("Document_Id"))
                .Select(col => col.ColumnName + " = @" + col.ColumnName))} WHERE Document_Id = @Document_Id";
            var parameters = dataRow.Table.Columns
            .Cast<DataColumn>()
            .ToDictionary(col => col.ColumnName, col => dataRow[col.ColumnName]);
            
            int res = connection.Execute(query, parameters );
            //MessageBox.Show($"{res}");
        }
    }
}
