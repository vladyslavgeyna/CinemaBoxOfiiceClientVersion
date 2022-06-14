using System;

namespace usersApp
{
    public class User
    {
        public int id { get; set; }

        private string login, password, name;
        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public User() { }

        public User(string login, string password, string name, int age)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.age = age;
        }
    }
}
