namespace GesLune.Models
{
    public class Model_Document_Ligne
    {
        public int Document_Ligne_Id { get; set; }
        public double Document_Ligne_Quantity { get; set; }
        public int? Document_Article_Id { get; set; }
        public string Document_Ligne_Article_Nom {  get; set; } = string.Empty;
        public decimal Document_Ligne_Prix_Unitaire { get; set; }
        public decimal Document_Ligne_Total { get; set; }
        public int Document_Id { get; set; }

        public Model_Document_Ligne() {}
    }
}
