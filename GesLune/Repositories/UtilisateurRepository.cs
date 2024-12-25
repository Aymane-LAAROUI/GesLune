using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace GesLune.Repositories
{
    public class UtilisateurRepository
    {

        public static Model_Utilisateur? Authenticate(string username, string password)
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            Object parameters = new
            {
                Utilisateur_Login = username,
                Utilisateur_Password = password
            };

            return connection.QueryFirstOrDefault<Model_Utilisateur>
                ("sp_authenticate_utilisateur", parameters, commandType: CommandType.StoredProcedure);
        }

    }
}
