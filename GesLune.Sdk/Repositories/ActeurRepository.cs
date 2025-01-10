using Dapper;
using GesLune.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;

namespace GesLune.Repositories
{
    public class ActeurRepository
    {
        public static List<Model_Acteur> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Acteur>("SELECT * FROM Tble_Acteurs").ToList();
        }

        public static List<Model_Acteur> GetAll(int top)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Acteur>($"SELECT TOP({top}) * FROM Tble_Acteurs").ToList();
        }

        public static List<Model_Acteur_Type> GetTypes()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Acteur_Type>($"SELECT * FROM Tble_Acteur_Types").ToList();
        }

        public static List<Model_Acteur> GetByTypeId(int Acteur_Type_Id) 
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Acteur>($"SELECT * FROM Tble_Acteurs WHERE Acteur_Type_Id = {Acteur_Type_Id}").ToList();
        }

        public static DataTable GetReleve(int Acteur_Id,DateTime? DateDu = null,DateTime? DateAu = null)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            DataTable dataTable = new();

            // Set default values for DateDu and DateAu if they are null
            DateTime defaultDateDu = new(1900, 1, 1); // Default start date
            DateTime defaultDateAu = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59); // Default end date (end of current day)

            DateDu ??= defaultDateDu; // Use null-coalescing operator to assign default value
            DateAu ??= defaultDateAu;


            // If DateAu is provided, set it to the end of the day
            if (DateAu.HasValue)
            {
                DateAu = DateAu.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            MessageBox.Show($"{DateDu.Value} > {DateAu.Value}");

            Object Parameters = new { DateDu,DateAu };
            try
            {
                // Open the connection
                connection.Open();

                // Create the command and specify the stored procedure
                using SqlCommand command = new("sp_releve_acteur", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add the parameter for the stored procedure
                command.Parameters.AddWithValue("@ActeurId", Acteur_Id);
                command.Parameters.AddWithValue("@DateDu", DateDu);
                command.Parameters.AddWithValue("@DateAu", DateAu);

                // Execute the command and fill the DataTable
                using SqlDataAdapter adapter = new(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Log the error and throw the exception
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

            // Return the populated DataTable
            return dataTable;
        }


        public static Model_Acteur Enregistrer(Model_Acteur model)
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
            return connection.QueryFirst<Model_Acteur>(
                    "sp_save_acteur", // Nom de la procédure stockée
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
        }

        public static int Delete(int Acteur_Id)
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Execute
                ($"DELETE FROM Tble_Acteurs WHERE Acteur_Id = {Acteur_Id}");
        }
    }
}
