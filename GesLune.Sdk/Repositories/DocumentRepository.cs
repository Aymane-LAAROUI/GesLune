using Dapper;
using GesLune.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Windows;

namespace GesLune.Repositories
{
    public class DocumentRepository
    {
        public static List<Model_Document> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Document>("SELECT * FROM Tble_Documents").ToList();
        }

        public static List<Model_Document> GetByTypeId(int Document_Type_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Document>
                ($"SELECT * FROM Tble_Documents WHERE Document_Type_Id = {Document_Type_Id}")
                .ToList();
        }

        public static List<Model_Document_Type> GetTypes()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Document_Type>("SELECT * FROM Tble_Document_Types").ToList();
        }

        public static List<Model_Document_Ligne> GetLignes(int Document_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Document_Ligne>($"SELECT * FROM Tble_Document_Lignes WHERE Document_Id = {Document_Id}").ToList();
        }

        public static DataTable GetChiffreAffaireMensuel()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            connection.Open();
            SqlCommand command = new("SELECT * FROM view_chiffre_affaire_mensuel ORDER BY Chiffre_Affaire_Annee, Chiffre_Affaire_Mois",connection);
            SqlDataAdapter adapter = new(command);
            DataTable dt = new();
            adapter.Fill(dt);
            return dt;
        }

        public static Model_Document Enregistrer(Model_Document model)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            // Préparer les paramètres pour la procédure stockée
            var parameters = new DynamicParameters();
            foreach (var property in model.GetType().GetProperties())
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(model); //?? DBNull.Value
                parameters.Add("@" + propertyName, propertyValue);
                //MessageBox.Show($"{propertyName} : {propertyValue}");
            }
            return connection.QueryFirst<Model_Document>(
                    "sp_save_document", // Nom de la procédure stockée
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
        }
        
        public static int EnregistrerLigne(Model_Document_Ligne ligne)
        {
            // Corriger le Sous total
            ligne.Document_Ligne_Total = ligne.Document_Ligne_Prix_Unitaire * (decimal)ligne.Document_Ligne_Quantity;
            using SqlConnection connection = new (MainRepository.ConnectionString);
            connection.Open();
            // Check if the document exists
            string verifQuery = "SELECT COUNT(Document_Ligne_Id) FROM Tble_Document_Lignes WHERE Document_Ligne_Id = @Document_Ligne_Id";
            using var verifCommand = new SqlCommand(verifQuery, connection);
            verifCommand.Parameters.AddWithValue("@Document_Ligne_Id", ligne.Document_Ligne_Id);
            int exists = (int)verifCommand.ExecuteScalar();

            // Construct the query dynamically based on existence
            string query;
            if (exists == 0)
            {
                query = "INSERT INTO Tble_Document_Lignes (" +
                        string.Join(", ", ligne.GetType().GetProperties().Select(p => p.Name).Where(e => !e.Equals("Document_Ligne_Id"))) +
                        ") VALUES (" +
                        string.Join(", ", ligne.GetType().GetProperties().Select(p => "@" + p.Name).Where(e => !e.Equals("@Document_Ligne_Id"))) + ")";
            }
            else
            {
                query = "UPDATE Tble_Document_Lignes SET " +
                        string.Join(", ", ligne.GetType().GetProperties()
                            .Where(p => p.Name != nameof(Model_Document_Ligne.Document_Ligne_Id))
                            .Select(p => p.Name + " = @" + p.Name)) +
                        " WHERE Document_Ligne_Id = @Document_Ligne_Id";
            }

            // Create a dictionary of parameters
            var parameters = ligne.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(ligne) ?? String.Empty);

            // Create and execute the query
            using var command = new SqlCommand(query, connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                //MessageBox.Show($"{param.Value}");
            }
            //MessageBox.Show(query);
            return command.ExecuteNonQuery();
            //MessageBox.Show($"{res}", "Succès");
        }

        public static bool EstEncaisse(int Document_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            // VERIFIER SI LE DOCUMENT EST DEJA ENCAISSEE
            var command = new SqlCommand($"SELECT Paiement_Id FROM Tble_Paiements WHERE Paiement_Document_Id = {Document_Id}", connection);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if (reader != null && reader.HasRows) return true;
            return false;
        }

        public static int Delete(int Document_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute($"DELETE FROM Tble_Documents WHERE Document_Id = {Document_Id}");
        }

        public static int DeleteDocumentLignes(int Document_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute($"DELETE FROM Tble_Document_Lignes WHERE Document_Id = {Document_Id}");
        }

        public static int DeleteLigne(int Document_Ligne_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute($"DELETE FROM Tble_Document_Lignes WHERE Document_Ligne_Id = {Document_Ligne_Id}");
        }

    }
}
