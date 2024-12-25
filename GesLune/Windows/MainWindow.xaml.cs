using GesLune.ViewModels;
using System.Windows;

namespace GesLune.Windows
{
    public partial class MainWindow : Window
    {

        private readonly MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }
    }
}
