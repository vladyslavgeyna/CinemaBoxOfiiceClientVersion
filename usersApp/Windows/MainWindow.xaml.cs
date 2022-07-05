using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace usersApp.Windows
{
    public partial class MainWindow : Window
    {
        private AppContext db;
        private User RegUser;
        public MainWindow()
        {
            InitializeComponent();
            db = AppContext.GetInstance();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.textBoxInput.Text.Trim();
            string password = textBoxPassword.Password.Trim();
            string passwordConfirm = textBoxPasswordConfirm.Password.Trim();
            string name = textBoxName.textBoxInput.Text.Trim();
            bool checkAge = int.TryParse(textBoxAge.textBoxInput.Text.Trim(), out int age);
            Regex regex = new Regex("^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$", RegexOptions.Compiled);
            Match match = regex.Match(login);
            if (name.Length <= 1)
            {
                textBoxName.textBoxInput.ToolTip = "Некоректно введене ім'я";
                textBoxName.textBoxInput.Background = Brushes.DarkRed;
                return;
            }
            else if (!match.Success)
            {
                textBoxLogin.textBoxInput.ToolTip = "Некоректно введений логін";
                textBoxLogin.textBoxInput.Background = Brushes.DarkRed;
                return;
            }
            else if (password.Length < 5)
            {
                textBoxPassword.ToolTip = "Мінімальна довжина пароля 5 символів";
                textBoxPassword.Background = Brushes.DarkRed;
                return;
            }
            else if (passwordConfirm != password)
            {
                textBoxPasswordConfirm.ToolTip = "Ви помилилися при другому введені пароля";
                textBoxPasswordConfirm.Background = Brushes.DarkRed;
                return;
            }
            else if (checkAge == false)
            {
                textBoxAge.textBoxInput.ToolTip = "Некоректно введений вік";
                textBoxAge.textBoxInput.Background = Brushes.DarkRed;
                return;
            }
            else if (age < 6 || age > 100)
            {
                textBoxAge.textBoxInput.ToolTip = "Вік занадто малий або великий";
                textBoxAge.textBoxInput.Background = Brushes.DarkRed;
                return;
            }
            else if (IsUserExist())
            {
                textBoxLogin.textBoxInput.ToolTip = "Цей логін вже занятий іншим користувачем. Введіть інший";
                textBoxLogin.textBoxInput.Background = Brushes.DarkRed;

                new MesBox($"Цей логін вже занятий іншим користувачем\n{"Введіть інший",41}", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
            else
            {
                password = GetHash(password);
                RegUser = new User(login, password, name, age);
                db.Users.Add(RegUser);
                db.SaveChanges();
                new MesBox("Реєстрація пройшла успішно", MessageType.Success, MessageButtons.Ok).ShowDialog();
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                this.Close();
            }
        }
        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
        private bool IsUserExist()
        {
            User user = null;
            user = db.Users.Where(b => b.Login == textBoxLogin.textBoxInput.Text.Trim()).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetHash(string text)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(hash);
        }
        private void textBoxAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
