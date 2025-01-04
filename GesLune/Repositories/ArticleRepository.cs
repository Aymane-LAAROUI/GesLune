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
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            return connection.Query<Model_Article>("SELECT * FROM Tble_Articles").ToList();
        }

        public static DataTable GetEtatStock()
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            connection.Open();
            SqlCommand command = new("SELECT * FROM view_etat_stock",connection);
            SqlDataAdapter adapter = new(command);
            DataTable dt = new();
            adapter.Fill(dt);
            return dt;
        }
        
        public static DataTable GetFicheStock(int Article_Id)
        {
            using var connection = new SqlConnection(RepositoryBase.ConnectionString);
            connection.Open();

            // Create the SqlCommand and set the CommandType to StoredProcedure
            using var command = new SqlCommand("sp_fiche_article", connection)
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
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
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

        public static int Delete(int id) 
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Articles WHERE Article_Id = " + id);
        }

    }
}
