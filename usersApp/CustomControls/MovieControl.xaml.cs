using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using usersApp.Windows;

namespace usersApp.CustomControls
{
    public partial class MovieControl : UserControl
    {
        public MovieControl()
        {
            InitializeComponent();
        }
        public ImageSource MovieImageSource
        {
            get { return (ImageSource)GetValue(MovieImageSourceProperty); }
            set { SetValue(MovieImageSourceProperty, value); }
        }
        public static readonly DependencyProperty MovieImageSourceProperty =
            DependencyProperty.Register("MovieImageSourceProperty", typeof(ImageSource), typeof(MovieControl), new PropertyMetadata(null, OnMovieImageSourcePropertyChanged));

        private static void OnMovieImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MovieControl source = d as MovieControl;
            source.ImageMovie.Source = (ImageSource)e.NewValue;
        }
        public string MovieName
        {
            get { return (string)GetValue(MovieNameProperty); }
            set { SetValue(MovieNameProperty, value); }
        }
        public static readonly DependencyProperty MovieNameProperty =
            DependencyProperty.Register("MovieNameProperty", typeof(string), typeof(MovieControl), new PropertyMetadata(null, OnMovieNamePropertyChanged));
        private static void OnMovieNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MovieControl source = d as MovieControl;
            source.TextBlockMovieName.Text = (string)e.NewValue;
        }
        private string linkTrailer;
        public string LinkTrailer
        {
            get { return linkTrailer; }
            set { linkTrailer = value; }
        }
        private int minAge;
        public int MinAge
        {
            get { return minAge; }
            set { minAge = value; }
        }
        private int yearOfIssue;
        public int YearOfIssue
        {
            get { return yearOfIssue; }
            set { yearOfIssue = value; }
        }
        private string movieDuration;
        public string MovieDuration
        {
            get { return movieDuration; }
            set { movieDuration = value; }
        }
        private string countryOfProduction;
        public string CountryOfProduction
        {
            get { return countryOfProduction; }
            set { countryOfProduction = value; }
        }
        private string movieGenre;
        public string MovieGenre
        {
            get { return movieGenre; }
            set { movieGenre = value; }
        }
        private string movieStudio;
        public string MovieStudio
        {
            get { return movieStudio; }
            set { movieStudio = value; }
        }
        private void ButtonMovieDetails_Click(object sender, RoutedEventArgs e)
        {
            MovieDetails movieDetails = new MovieDetails();
            movieDetails.ImageMovie.Source = MovieImageSource;
            movieDetails.textBlockNameOfMovie.Text = MovieName;
            movieDetails.textBlockMinAge.Text = minAge.ToString();
            movieDetails.textBlockYearOfIssue.Text = yearOfIssue.ToString();
            movieDetails.textBlockMovieDuration.Text = movieDuration;
            movieDetails.textBlockCountryOfProduction.Text = countryOfProduction;
            movieDetails.textBlockMovieGenre.Text = movieGenre;
            movieDetails.textBlockMovieStudio.Text = movieStudio;
            movieDetails.LinkTrailer = linkTrailer;
            movieDetails.ShowDialog();
        }
    }
}
