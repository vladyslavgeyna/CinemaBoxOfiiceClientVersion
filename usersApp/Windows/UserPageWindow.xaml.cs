using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using usersApp.Pages;

namespace usersApp.Windows
{
    public partial class UserPageWindow : Window
    {
        public static List<Page> navigationPages;
        public UserPageWindow()
        {
            InitializeComponent();
            navigationPages = new List<Page>() { new HomePage(), new UserPage(), new FavouritePage(), new MoviesPage(), new BuyingPage(), new SupportPage() };
        }
        private void Ham1_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigationPages[0];
        }
        private void Ham2_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigationPages[1];
        }
        public void Ham3_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigationPages[2];
        }
        private void Ham4_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigationPages[3];
        }
        private void Ham5_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigationPages[4];
        }
        private void Ham6_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigationPages[5];
        }
    }
}
