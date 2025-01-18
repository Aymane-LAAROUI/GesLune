using System.Windows;
using System.Windows.Controls;

namespace GesLune.KeyBoards
{
    public partial class NumericKeyBoardWindow : Window
    {
        public double Query;
        public NumericKeyBoardWindow(double query = 1)
        {
            InitializeComponent();
            DataContext = this;
            Query = query;
            InputTextBox.Text = $"{Query}";
            InputTextBox.Focus();
            
        }

        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                InputTextBox.Text += button.Content.ToString();
            }
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(InputTextBox.Text))
            {
                InputTextBox.Text = InputTextBox.Text[..^1];
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(InputTextBox.Text, out double result)) return;
            Query = result;
            this.DialogResult = true;
            this.Close();
        }
    }
}
