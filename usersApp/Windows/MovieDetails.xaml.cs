using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using usersApp.Classes.CinemaClasses;

namespace usersApp.Windows
{
    public partial class MovieDetails : Window
    {
        public static CinemaMovie cinemaMovie = null;
        public MovieDetails()
        {
            InitializeComponent();
            datePickerDate.DisplayDateStart = DateTime.Now;
            datePickerDate.DisplayDateEnd = DateTime.Now.AddDays(6);
        }
        private string linkTrailer;
        public string LinkTrailer
        {
            get { return linkTrailer; }
            set { linkTrailer = value; }
        }
        private void watchTrailer_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(linkTrailer);
        }
        private void buttonСhoosePlace_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerDate.Text == "")
            {
                datePickerDate.ToolTip = "Ви не обрали дату";
                datePickerDate.Background = Brushes.DarkRed;
                return;
            }
            else if (comboBoxTime.SelectedIndex == -1)
            {
                comboBoxTime.ToolTip = "Ви не обрали час";
                comboBoxTime.Background = Brushes.DarkRed;
                return;
            }
            else
            {
                if (AuthWindow.authUser != null)
                {
                    if(AuthWindow.authUser.Age >= int.Parse(textBlockMinAge.Text))
                    {
                        string plan = null;
                        if(radioButtonPension.IsChecked == true)
                        {
                            plan = "Пенсіонер";
                        }
                        else if(radioButtonSchoolChild.IsChecked == true)
                        {
                            plan = "Школяр";
                        }
                        else if(radioButtonStudent.IsChecked == true)
                        {
                            plan = "Студент";
                        }
                        cinemaMovie = new CinemaMovie(textBlockNameOfMovie.Text, MakePriceOfSession(), datePickerDate.SelectedDate.Value, comboBoxTime.Text, plan);
                        this.Close();
                        CinemaHall cinemaHall = new CinemaHall();
                        cinemaHall.ShowDialog();
                    }
                    else
                    {
                        new MesBox($"{"На жаль, Ви не досягли",34}\n{"достатнього віку для цього фільму",36}", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                        return;
                    }
                }
                
            }
        }

        private void comboBoxTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxTime.ToolTip = null;
            comboBoxTime.Background = Brushes.Transparent;
        }

        private void datePickerDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (datePickerDate.Text != "")
            {
                datePickerDate.ToolTip = null;
                datePickerDate.Background = Brushes.Transparent;
            }
        }

        private void buttonReloadPrice_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerDate.Text == "" || comboBoxTime.SelectedIndex == -1)
            {
                new MesBox("Ви не обрали дату, чи час", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            else
            {
                textBlockPriceOfSession.Visibility = Visibility.Visible;
                textBlockUAH.Visibility = Visibility.Visible;
                MakePriceOfSession();
            }
        }
        private double MakePriceOfSession()
        {
            double priceOfSession;
            bool isAnyChecked = false;
            if (radioButtonPension.IsChecked == true || radioButtonSchoolChild.IsChecked == true || radioButtonStudent.IsChecked == true)
                isAnyChecked = true;
            DateTime dateTime = datePickerDate.SelectedDate.Value;
            if (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                if (comboBoxTime.SelectedIndex >= 3)
                {
                    if (isAnyChecked)
                        priceOfSession = 100;
                    else
                        priceOfSession = 110;
                }
                else
                {
                    if (isAnyChecked)
                        priceOfSession = 80;
                    else
                        priceOfSession = 90;
                }
            }
            else
            {
                if (comboBoxTime.SelectedIndex >= 3)
                {
                    if (isAnyChecked)
                        priceOfSession = 80;
                    else
                        priceOfSession = 90;
                }
                else
                {
                    if (isAnyChecked)
                        priceOfSession = 60;
                    else
                        priceOfSession = 70;
                }
            }
            textBlockPriceOfSession.Text = priceOfSession.ToString();
            return priceOfSession;
        }
    }
}
