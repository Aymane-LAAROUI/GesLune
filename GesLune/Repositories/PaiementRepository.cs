using GesLune.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace GesLune.Repositories
{
    public class PaiementRepository
    {
        public static List<Model_Paiement> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Paiement>("SELECT * FROM Tble_Paiements").ToList();
        }

        public static List<Model_Paiement_Type> GetTypes()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Paiement_Type>("SELECT * FROM Tble_Paiement_Types").ToList();
        }

        public static int Delete(int id) 
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Paiements WHERE Paiement_Id = " + id);
        }

        public static Model_Paiement Enregistrer(Model_Paiement model)
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
            return connection.QueryFirst<Model_Paiement>(
                    "sp_save_paiement", // Nom de la procédure stockée
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
        }

    }
}
