using Dapper;
using GesLune.Models;
using Microsoft.Data.SqlClient;

namespace GesLune.Repositories
{
    public class ActeurRepository
    {
        public static List<Model_Acteur> GetAll()
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            connection.Open();
            return connection.Query<Model_Acteur>("SELECT * FROM Tble_Acteurs").ToList();
        }

        public static List<Model_Acteur> GetAll(int top)
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            connection.Open();
            return connection.Query<Model_Acteur>($"SELECT TOP({top}) * FROM Tble_Acteurs").ToList();
        }

        public static List<Model_Acteur_Type> GetTypes()
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            connection.Open();
            return connection.Query<Model_Acteur_Type>($"SELECT * FROM Tble_Acteur_Types").ToList();
        }

        public static List<Model_Acteur> GetByTypeId(int Acteur_Type_Id) 
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            connection.Open();
            return connection.Query<Model_Acteur>($"SELECT * FROM Tble_Acteurs WHERE Acteur_Type_Id = {Acteur_Type_Id}").ToList();
        }

        public static int Delete(int Acteur_Id)
        {
            using SqlConnection connection = new(RepositoryBase.ConnectionString);
            connection.Open();
            return connection.Execute
                ($"DELETE FROM Tble_Acteurs WHERE Acteur_Id = {Acteur_Id}");
        }

    }
}
