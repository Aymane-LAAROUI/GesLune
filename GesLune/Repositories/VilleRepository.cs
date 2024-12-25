using GesLune.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GesLune.Repositories
{
    public class VilleRepository
    {
        public static List<Model_Ville> GetAll()
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            return connection.Query<Model_Ville>("SELECT * FROM Tble_Villes").ToList();
        }
    }
}
