using System.Windows;
using System.Windows.Controls;

namespace GesLune
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Type> _windows = new()
        {
            { "0",typeof(PosWindow)},
            { "1",typeof(ListeVenteWindow)},
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag && _windows.TryGetValue(tag, out var type))
            {
                if (Activator.CreateInstance(type) is Window window)
                {
                    window.Show();
                }
            }
        }
    }
}
