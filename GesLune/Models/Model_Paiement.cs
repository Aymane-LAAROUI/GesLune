namespace GesLune.Models
{
    public class Model_Paiement
    {
        public int Paiement_Id { get; set; }
        public double Paiement_Montant { get; set; }
        public int? Paiement_Acteur_Id { get; set; }
        public string? Paiement_Acteur_Nom {  get; set; }
        public DateTime Paiement_Date {  get; set; } = DateTime.Now;
        public string? Paiement_Description {  get; set; }

        public Model_Paiement() { }
        public Model_Paiement(int paiement_Id, double paiement_Montant, int? paiement_Acteur_Id, string? paiement_Acteur_Nom, DateTime paiement_Date, string? paiement_Description)
        {
            Paiement_Id = paiement_Id;
            Paiement_Montant = paiement_Montant;
            Paiement_Acteur_Id = paiement_Acteur_Id;
            Paiement_Acteur_Nom = paiement_Acteur_Nom;
            Paiement_Date = paiement_Date;
            Paiement_Description = paiement_Description;
        }
    }
}
