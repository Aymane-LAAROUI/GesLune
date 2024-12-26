using GesLune.Models;
using Microsoft.Data.SqlClient;
using Dapper;

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
    }
}
