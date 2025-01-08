using GesLune.Repositories;
using LiveCharts;
using System.Data;

namespace GesLune.ViewModels.Statistiques
{
    public class ChiffreAffaireViewModel : ViewModelBase
    {
        private DataTable DataTable { get; set; } = new();
        public ChartValues<double> Values { get; set; } = [];

        public double Total
        {
            get => Values.Sum();
        }
        public ChiffreAffaireViewModel()
        {
            DataInit();
        }

        private void DataInit()
        {
            DataTable = DocumentRepository.GetChiffreAffaireMensuel();
            foreach (DataRow row in DataTable.Rows)
            {
                double total = Convert.ToDouble(row["Chiffre_Affaire_Total"]);
                Values.Add(total);
            }
        }
    }
}
