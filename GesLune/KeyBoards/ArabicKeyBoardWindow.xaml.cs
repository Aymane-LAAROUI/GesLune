using System.Windows;
using System.Windows.Controls;

namespace GesLune.KeyBoards
{
    public partial class ArabicKeyBoardWindow : Window
    {
        public string Query { get; set; }
        public ArabicKeyBoardWindow(string query = "")
        {
            InitializeComponent();
            Query = query;
            InputTextBox.Text = Query;
            DataContext = this;
            InputTextBox.Focus();
        }

        private void InitButton_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
        }
        
        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string? key = button.Content.ToString();
                if (key == "مسافة")
                    InputTextBox.Text += " ";
                else
                    InputTextBox.Text += key;
            }
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length > 0)
            {
                InputTextBox.Text = InputTextBox.Text[..^1];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
