namespace GesLune.Models
{
    public class Model_Document_Ligne
    {
        public int Document_Ligne_Id { get; set; }
        public double Document_Ligne_Quantity { get; set; }
        public string Document_Ligne_Article_Nom {  get; set; } = string.Empty;
        public decimal Document_Ligne_Prix_Unitaire { get; set; }
        public decimal Document_Ligne_Total { get; set; }
        public int Document_Id { get; set; }

        public Model_Document_Ligne() {}

        public Model_Document_Ligne(int document_Ligne_Id, double document_Ligne_Quantity, string document_Ligne_Article_Nom, decimal document_Ligne_Prix_Unitaire, decimal document_Ligne_Total, int document_Id)
        {
            Document_Ligne_Id = document_Ligne_Id;
            Document_Ligne_Quantity = document_Ligne_Quantity;
            Document_Ligne_Article_Nom = document_Ligne_Article_Nom;
            Document_Ligne_Prix_Unitaire = document_Ligne_Prix_Unitaire;
            Document_Ligne_Total = document_Ligne_Total;
            Document_Id = document_Id;
        }
    }
}
