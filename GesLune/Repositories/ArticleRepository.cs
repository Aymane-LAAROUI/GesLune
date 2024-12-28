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

        public static int Delete(int id) 
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Articles WHERE Article_Id = " + id);
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

    }
}
