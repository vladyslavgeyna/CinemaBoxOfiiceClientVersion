using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using usersApp.Classes.DataBaseClasses;
using usersApp.Windows;

namespace usersApp.Pages
{
    public partial class FavouritePage : Page
    {
        public static AppContext db;
        public static List<Product> products;
        public static List<Movie> movies;
        public static double TotalPrice = 0;
        private double totalMoviesPrice = 0, totalProductsPrice = 0;  
        public FavouritePage()
        {
            InitializeComponent();
            if (AuthWindow.authUser != null)
            {
                db = AppContext.GetInstance();
                products = db.Products.Where(b => b.User_Id == AuthWindow.authUser.id).ToList();
                movies = db.Movies.Where(b => b.User_Id == AuthWindow.authUser.id).ToList();
                if (products.Count != 0)
                {
                    textBlockTitleProducts.Visibility = Visibility.Visible;
                    buttonRemoveFavorite.IsEnabled = true;
                    textBlockTitle.Text = "Ваше обране:";
                    listViewListOfProducts.ItemsSource = products;
                    foreach (Product product in products)
                        totalProductsPrice += product.Price * product.Amount;
                }
                if (movies.Count != 0)
                {
                    textBlockTitleMovies.Visibility = Visibility.Visible;
                    buttonRemoveFavorite.IsEnabled = true;
                    textBlockTitle.Text = "Ваше обране:";
                    listViewListOfMovies.ItemsSource = movies;
                    foreach (Movie movie in movies)
                        totalMoviesPrice += movie.Price;
                }
                TotalPrice = totalMoviesPrice + totalProductsPrice;
                if(TotalPrice != 0)
                {
                    textBlockTotalPrice.Text = TotalPrice.ToString();
                    textBlockTotalPrice.Visibility = Visibility.Visible;
                    textBlockTotalPriceUAH.Visibility = Visibility.Visible;
                    textBlockTotalPriceTitle.Visibility = Visibility.Visible;
                    buttonMakeOrder.Visibility = Visibility.Visible;
                }
            }
        }
        private void buttonRemoveFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (AuthWindow.authUser != null)
            {
                if (movies.Count != 0)
                {
                    foreach (Movie movie in movies)
                        db.Movies.Remove(movie);
                    db.SaveChanges();
                    listViewListOfMovies.ItemsSource = null;
                    textBlockTitleMovies.Visibility = Visibility.Collapsed;
                }
                if (products.Count != 0)
                {
                    foreach (Product product in products)
                        db.Products.Remove(product);
                    db.SaveChanges();
                    listViewListOfProducts.ItemsSource = null;
                    textBlockTitleProducts.Visibility = Visibility.Collapsed;
                }
                textBlockTotalPriceUAH.Visibility = Visibility.Hidden;
                textBlockTotalPriceTitle.Visibility = Visibility.Hidden;
                textBlockTotalPrice.Visibility = Visibility.Hidden;
                buttonMakeOrder.Visibility = Visibility.Hidden;
                textBlockTitle.Text = "В вашому обраному поки пусто";
                buttonRemoveFavorite.IsEnabled = false;
            }
        }
        private void buttonMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if (AuthWindow.authUser != null)
            {
                if (movies.Count != 0)
                {
                    OrderWindow orderWindow = new OrderWindow();
                    orderWindow.ShowDialog();
                    if(OrderWindow.IsOrderMade)
                    {
                        buttonRemoveFavorite_Click(sender, e);
                        OrderWindow.IsOrderMade = false;
                    }
                }
                else
                {
                    new MesBox("Ви не обрали жодного фільму", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }
            }
        }
    }
}
