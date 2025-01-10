namespace GesLune.Models
{
    public class Model_Paiement
    {
        public int Paiement_Id { get; set; }
        public decimal Paiement_Montant { get; set; }
        public int? Paiement_Acteur_Id { get; set; }
        public string? Paiement_Acteur_Nom {  get; set; }
        public DateTime Paiement_Date {  get; set; } = DateTime.Now;
        public string? Paiement_Description {  get; set; }
        public int? Paiement_Document_Id { get; set; }
        public int Paiement_Type_Id { get; set; }
        public Model_Paiement() { }
    }
}
