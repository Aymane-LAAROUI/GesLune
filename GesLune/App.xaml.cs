using GesLune.Windows;
using System.Windows;

namespace GesLune
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new DocumentsWindow();
            mainWindow.Show(); 
        }
    }
}
