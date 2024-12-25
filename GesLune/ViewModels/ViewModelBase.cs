using System.ComponentModel;

namespace GesLune.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //public static readonly string ConnectionString = "Data Source=localhost;Initial Catalog=GesLune;User ID=sa;Password=admin@123456;TrustServerCertificate=True";

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
