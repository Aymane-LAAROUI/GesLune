using System.ComponentModel;

namespace GesLune.Sdk.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<Exception>? ExceptionThrown;
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnExceptionThrown(Exception exception)
        {
            ExceptionThrown?.Invoke(this, exception);
        }
    }
}
