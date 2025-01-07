namespace GesLune.Models
{
    public class Model_Article
    {
        public int Article_Id { get; set; }
        public string Article_Nom { get; set; } = string.Empty;
        public string? Article_Description { get; set; }
        public double Article_Prix { get; set; }
        public bool Article_Stockable { get; set; }
        public int Article_Categorie_Id { get; set; }

        public Model_Article() { }
    }
}
