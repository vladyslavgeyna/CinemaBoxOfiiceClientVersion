using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace usersApp.Windows
{
    public partial class AuthWindow : Window
    {
        private AppContext db;
        public AuthWindow()
        {
            InitializeComponent();
            db = AppContext.GetInstance();
        }
        public static User authUser = null;
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.textBoxInput.Text.Trim();
            string password = textBoxPassword.Password.Trim();
            Regex regex = new Regex("^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$", RegexOptions.Compiled);
            Match match = regex.Match(login);
            if (!match.Success)
            {
                textBoxLogin.textBoxInput.ToolTip = "Некоректно введений логін";
                textBoxLogin.textBoxInput.Background = Brushes.DarkRed;
            }
            else if (password.Length < 5)
            {
                textBoxPassword.ToolTip = "Мінімальна довжина пароля 5 символів";
                textBoxPassword.Background = Brushes.DarkRed;
            }
            else
            {
                password = MainWindow.GetHash(password);
                authUser = db.Users.Where(b => b.Login == login && b.Password == password).FirstOrDefault();
                if (authUser != null)
                {
                    new MesBox("Авторизація пройшла успішно", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    this.Close();
                }
                else
                {
                    new MesBox("Користувача не знайдено", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
        }
        private void Button_Red_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
