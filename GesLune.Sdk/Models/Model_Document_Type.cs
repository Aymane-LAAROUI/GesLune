namespace GesLune.Models
{
    public class Model_Document_Type
    {
        public int Document_Type_Id { get; set; }
        public string Document_Type_Nom { get; set; } = string.Empty;
        public string Document_Type_Nom_Abrege { get; set; } = string.Empty;
        public int Document_Type_Acteur_Type_Id { get; set; }

        public Model_Document_Type() { }

    }
}
