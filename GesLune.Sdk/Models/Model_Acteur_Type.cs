namespace GesLune.Models
{
    public class Model_Acteur_Type
    {
        public int Acteur_Type_Id { get; set; }
        public string Acteur_Type_Nom { get; set; } = string.Empty;
        public string Acteur_Type_Nom_Abrege { get; set; } = string.Empty;

        public Model_Acteur_Type(int acteur_Type_Id, string acteur_Type_Nom, string acteur_Type_Nom_Abrege)
        {
            Acteur_Type_Id = acteur_Type_Id;
            Acteur_Type_Nom = acteur_Type_Nom;
            Acteur_Type_Nom_Abrege = acteur_Type_Nom_Abrege;
        }
    }
}
