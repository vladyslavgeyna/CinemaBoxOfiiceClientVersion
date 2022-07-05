using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using usersApp.Classes.CinemaClasses;
using usersApp.Classes.DataBaseClasses;
using System.Threading;
using usersApp.Pages;

namespace usersApp.Windows
{
    public partial class CinemaHall : Window
    {
        private AppContext db;
        private Movie movie;
        public CinemaHall()
        {
            InitializeComponent();
        }
        private async void CinemaPlaceFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxRow.SelectedIndex == -1)
            {
                comboBoxRow.ToolTip = "Ви не обрали дату";
                comboBoxRow.Background = Brushes.DarkRed;
                return;
            }
            else if (comboBoxColumn.SelectedIndex == -1)
            {
                comboBoxColumn.ToolTip = "Ви не обрали час";
                comboBoxColumn.Background = Brushes.DarkRed;
                return;
            }
            else
            {
                if (AuthWindow.authUser != null)
                {
                    db = AppContext.GetInstance();
                    List<Movie> movies = db.Movies.Where(b => b.User_Id == AuthWindow.authUser.id).ToList();
                    if(movies.Count >= 1)
                    {
                        new MesBox("В обране можна додати лише один фільм", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                        this.Close();
                        return;
                    }
                    else
                    {
                        if(MovieDetails.cinemaMovie != null)
                        {
                            MovieDetails.cinemaMovie.CinemaPlace = new CinemaPlace(comboBoxRow.SelectedIndex + 1, comboBoxColumn.SelectedIndex + 1);
                            movie = new Movie(MovieDetails.cinemaMovie.Name, MovieDetails.cinemaMovie.TimeOfSession, MovieDetails.cinemaMovie.DateOfSession.Day, MovieDetails.cinemaMovie.DateOfSession.Month, MovieDetails.cinemaMovie.DateOfSession.Year, MovieDetails.cinemaMovie.CinemaPlace.Row, MovieDetails.cinemaMovie.CinemaPlace.Column, MovieDetails.cinemaMovie.Price, AuthWindow.authUser.id, MovieDetails.cinemaMovie.Plan);
                            db.Movies.Add(movie);
                            db.SaveChanges();
                            UserPageWindow.navigationPages[2] = new FavouritePage();
                            SuccessPopUpWindow successPopUpWindow = new SuccessPopUpWindow();
                            successPopUpWindow.Show();
                            await Task.Factory.StartNew(() => Pause(3));
                            successPopUpWindow.Close();
                            this.Close();
                        }
                    }
                }
            }
        }
        private void Pause(int T)
        {
            Thread.Sleep(T * 1000);
        }
        private void comboBoxRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxRow.ToolTip = null;
            comboBoxRow.Background = Brushes.Transparent;
            comboBoxColumn.IsEnabled = true;
            List<string> list = new List<string>();
            if (comboBoxRow.SelectedIndex == 0)
            {
                for (int i = 0; i < 10; i++)
                    list.Add($"{i + 1} місце");
                comboBoxColumn.ItemsSource = list;
            }
            else if (comboBoxRow.SelectedIndex == 1 || comboBoxRow.SelectedIndex == 2)
            {
                for(int i = 0; i < 12; i++)
                    list.Add($"{i + 1} місце");
                comboBoxColumn.ItemsSource = list;
            }
            else if (comboBoxRow.SelectedIndex == 3 || comboBoxRow.SelectedIndex == 4)
            {
                for (int i = 0; i < 14; i++)
                    list.Add($"{i + 1} місце");
                comboBoxColumn.ItemsSource = list;
            }
            else if (comboBoxRow.SelectedIndex == 5)
            {
                for (int i = 0; i < 16; i++)
                    list.Add($"{i + 1} місце");
                comboBoxColumn.ItemsSource = list;
            }
        }
        private void comboBoxColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxColumn.ToolTip = null;
            comboBoxColumn.Background = Brushes.Transparent;
        }
    }
}