using System.Windows.Controls;
using usersApp.Windows;

namespace usersApp.Pages
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            if (AuthWindow.authUser != null)
                textBlockNameOfUser.Text = AuthWindow.authUser.Name;
        }
    }
}
