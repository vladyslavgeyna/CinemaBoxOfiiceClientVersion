using System.Windows.Controls;
using usersApp.Windows;

namespace usersApp.Pages
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            if(AuthWindow.authUser != null)
            { 
                textBlockUserName.Text = AuthWindow.authUser.Name;
                textBlockUserLogin.Text = AuthWindow.authUser.Login;
                textBlockUserAge.Text = AuthWindow.authUser.Age.ToString();
            }
        }
    }
}
