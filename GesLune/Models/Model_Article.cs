namespace GesLune.Models
{
    public class Model_Article
    {
        public int Article_Id { get; set; }
        public string Article_Nom { get; set; } = string.Empty;
        public string? Article_Description { get; set; }
        public double Article_Prix { get; set; }

        public Model_Article(int article_Id, string article_Nom, string? article_Description, double article_Prix)
        {
            Article_Id = article_Id;
            Article_Nom = article_Nom;
            Article_Description = article_Description;
            Article_Prix = article_Prix;
        }

        public Model_Article() { }
    }
}
