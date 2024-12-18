namespace GesLune.Models
{
    public class Model_Document
    {
        public int Document_Id { get; set; }
        public string Document_Nom_Ste { get; set; }
        public string Document_Ice { get; set; }
        public DateTime Document_Date { get; set; }
        public string Document_Nom_Client { get; set; }
        public string? Document_Adresse_Client { get; set; }
        public string Document_Num { get; set; }

        public Model_Document() { }

    }
}
