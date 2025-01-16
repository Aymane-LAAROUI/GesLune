using GesLune.Sdk.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace GesLune.Sdk.Repositories
{
    public class CategorieRepository
    {
        public static List<Model_Categorie> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Categorie>("SELECT * FROM Tble_Categories").ToList();
        }

        public static List<Model_Categorie> GetAll(int pageNumber, int pageSize)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);

            // Calculate the offset for pagination
            int offset = (pageNumber - 1) * pageSize;

            // Query with OFFSET and FETCH for pagination
            string query = @"
                           SELECT * 
                           FROM Tble_Categories
                           ORDER BY Categorie_Id
                           OFFSET @Offset ROWS
                           FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new { Offset = offset, PageSize = pageSize };

            return connection.Query<Model_Categorie>(query, parameters).ToList();
        }

        public static Model_Categorie GetById(int id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.QueryFirst<Model_Categorie>("SELECT * FROM Tble_Categories WHERE Categorie_Id = " + id);
        }

        public static int Delete(int id) 
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Categories WHERE Categorie_Id = " + id);
        }

        public static Model_Categorie Enregistrer(Model_Categorie model)
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
            return connection.QueryFirst<Model_Categorie>(
                    "sp_save_Categorie", // Nom de la procédure stockée
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
        }

    }
}
