namespace GesLune.Models
{
    public class Model_Acteur
    {
        public int Acteur_Id { get; set; }
        public string Acteur_Nom { get; set; } = string.Empty;
        public string? Acteur_Adresse { get; set; }
        public string? Acteur_Telephone { get; set; }
        public string? Acteur_Email { get; set; }
        public string? Acteur_Remarque { get; set; }
        public int Acteur_Type_Id { get; set; }
        public int Acteur_Ville_Id { get; set; }

        public Model_Acteur(int acteur_Id, string acteur_Nom, string acteur_Adresse, string acteur_Telephone, string acteur_Email, string acteur_Remarque, int acteur_Type_Id, int acteur_Ville_Id)
        {
            Acteur_Id = acteur_Id;
            Acteur_Nom = acteur_Nom;
            Acteur_Adresse = acteur_Adresse;
            Acteur_Telephone = acteur_Telephone;
            Acteur_Email = acteur_Email;
            Acteur_Remarque = acteur_Remarque;
            Acteur_Type_Id = acteur_Type_Id;
            Acteur_Ville_Id = acteur_Ville_Id;
        }

        public Model_Acteur() { }
    }
}
