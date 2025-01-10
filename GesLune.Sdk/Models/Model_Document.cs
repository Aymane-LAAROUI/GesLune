namespace GesLune.Models
{
    public class Model_Document
    {
        public int Document_Id { get; set; }
        public string Document_Num { get; set; } = string.Empty;
        public string Document_Nom_Ste { get; set; } = string.Empty;
        public string Document_Ice { get; set; } = string.Empty;
        public DateTime Document_Date { get; set; } = DateTime.Now.Date;
        public int Document_Acteur_Id { get; set; }
        public string Document_Acteur_Nom { get; set; } = string.Empty;
        public string? Document_Acteur_Adresse { get; set; }
        public int Document_Type_Id { get; set; }
        public int Document_Reference_Id { get; set; }

        public Model_Document() { }
    }
}
