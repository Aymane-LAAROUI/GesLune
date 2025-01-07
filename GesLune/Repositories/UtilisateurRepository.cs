using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace GesLune.Repositories
{
    public class UtilisateurRepository
    {

        public static List<Model_Utilisateur> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Utilisateur>("SELECT * FROM Tble_Utilisateurs").ToList();
        }

        public static Model_Utilisateur? Authenticate(string username, string password)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            Object parameters = new
            {
                Utilisateur_Login = username,
                Utilisateur_Password = password
            };

            return connection.QueryFirstOrDefault<Model_Utilisateur>
                ("sp_authenticate_utilisateur", parameters, commandType: CommandType.StoredProcedure);
        }

        public static Model_Utilisateur Enregistrer(Model_Utilisateur model)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            Object parameters = new
            {
                model.Utilisateur_Id,
                model.Utilisateur_Login,
                model.Utilisateur_Password,
            };
            return connection.QueryFirst<Model_Utilisateur>("sp_save_utilisateur",parameters,commandType: CommandType.StoredProcedure);
        }

        public static int Delete(int Utilisateur_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute("DELETE FROM Tble_Utilisateurs WHERE Utilisateur_Id = " + Utilisateur_Id);
        }

    }
}
