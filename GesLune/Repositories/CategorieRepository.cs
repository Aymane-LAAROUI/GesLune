using GesLune.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace GesLune.Repositories
{
    public class CategorieRepository
    {
        public static List<Model_Categorie> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Categorie>("SELECT * FROM Tble_Categories").ToList();
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
