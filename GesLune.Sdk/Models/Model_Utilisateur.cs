namespace GesLune.Models
{
    public class Model_Utilisateur
    {
        public int Utilisateur_Id { get; set; }

        public string Utilisateur_Login { get; set; } = string.Empty;

        public string Utilisateur_Password { get; set; } = string.Empty;

        public Model_Utilisateur(int utilisateur_Id, string utilisateur_Login, string utilisateur_Password)
        {
            Utilisateur_Id = utilisateur_Id;
            Utilisateur_Login = utilisateur_Login;
            Utilisateur_Password = utilisateur_Password;
        }

        public Model_Utilisateur() { }
    }
}
