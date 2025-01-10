using GesLune.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace GesLune.Repositories
{
    public class ArticleRepository
    {
        public static List<Model_Article> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Article>("SELECT * FROM Tble_Articles").ToList();
        }
        public static List<string> GetCodes(int article_id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<string>("SELECT Article_Code FROM Tble_Article_Codes WHERE Article_Id =" + article_id).ToList();
        }

        public static DataTable GetEtatStock()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            connection.Open();
            SqlCommand command = new("SELECT * FROM view_etat_stock",connection);
            SqlDataAdapter adapter = new(command);
            DataTable dt = new();
            adapter.Fill(dt);
            return dt;
        }
        
        public static DataTable GetFicheStock(int Article_Id)
        {
            using var connection = new SqlConnection(MainRepository.ConnectionString);
            connection.Open();

            // Create the SqlCommand and set the CommandType to StoredProcedure
            using var command = new SqlCommand("sp_fiche_stock", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // You can add parameters to the command if your stored procedure requires them
            command.Parameters.AddWithValue("@Article_Id", Article_Id);

            // Use SqlDataAdapter to fill the DataTable
            var adapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }

        public static Model_Article Enregistrer(Model_Article model)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            // Préparer les paramètres pour la procédure stockée
            var parameters = new DynamicParameters();
            foreach (var property in model.GetType().GetProperties())
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(model); //?? DBNull.Value
                parameters.Add("@" + propertyName, propertyValue);
            }
            return connection.QueryFirst<Model_Article>(
                    "sp_save_article", // Nom de la procédure stockée
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
        }

        public static int AddCode(int Article_Id,string Article_Code)
        {
            using var connection = new SqlConnection( MainRepository.ConnectionString);
            connection.Open();
            var command = new SqlCommand("INSERT INTO Tble_Article_Codes " +
                "(Article_Id,Article_Code) " +
                "VALUES (@Article_Id,@Article_Code)", connection);
            command.Parameters.AddWithValue("@Article_Id", Article_Id);
            command.Parameters.AddWithValue("@Article_Code", Article_Code);
            return command.ExecuteNonQuery();
        }

        public static int Delete(int id) 
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Articles WHERE Article_Id = " + id);
        }
        public static int DeleteCode(string code) 
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Article_Codes WHERE Article_Code = " + code);
        }

    }
}
