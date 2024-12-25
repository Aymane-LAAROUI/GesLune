using System.Windows;

namespace GesLune.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClientsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ActeursWindow(2).Show();
        }

        private void FournisseursMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ActeursWindow(1).Show();
        }

        private void BCMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new DocumentsWindow(1).ShowDialog();
        }

        private void BLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new DocumentsWindow(2).ShowDialog();
        }

        private void FCMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new DocumentsWindow(3).ShowDialog();
        }
    }
}
