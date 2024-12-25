namespace GesLune.Models
{
    public class Model_Document
    {
        public int Document_Id { get; set; }
        public string Document_Num { get; set; } = string.Empty;
        public string Document_Nom_Ste { get; set; } = string.Empty;
        public string Document_Ice { get; set; } = string.Empty;
        public DateTime Document_Date { get; set; } = DateTime.Now.Date;
        public string Document_Nom_Client { get; set; } = string.Empty;
        public string? Document_Adresse_Client { get; set; }
        public int Document_Type_Id { get; set; }
        public int Acteur_Id { get; set; }

        public Model_Document() { }
    }
}
