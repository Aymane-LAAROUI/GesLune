using GesLune.Windows;
using GesLune.Windows.Articles;
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
            var mainWindow = new LoginWindow();
            mainWindow.Show(); 
        }
    }
}
