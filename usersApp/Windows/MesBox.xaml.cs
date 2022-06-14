using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace usersApp.Windows
{
    public partial class MesBox : Window
    {
        public MesBox(string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Content = Message;
            switch (Type)
            {
                case MessageType.Success:
                    {
                        txtTitle.Text = "Успіх";
                        cardHeader.Background = Brushes.Green;
                        btnOk.Background = Brushes.Green;
                    }
                    break;
                case MessageType.Warning:
                    txtTitle.Text = "Увага";
                    cardHeader.Background = Brushes.DarkOrange;
                    btnOk.Background = Brushes.DarkOrange;
                    break;
                case MessageType.Error:
                    {
                        txtTitle.Text = "Помилка";
                        cardHeader.Background = Brushes.Red;
                        btnOk.Background = Brushes.Red;
                    }
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        private void cardHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
    public enum MessageType
    {
        Success,
        Warning,
        Error,
    }
    public enum MessageButtons
    {
        Ok,
    }

}

