using System.Data.Entity;
using usersApp.Classes.DataBaseClasses;

namespace usersApp
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Movie> Movies { get; set; }
        private AppContext() : base("DefaultConnection") { }
        private static AppContext instance;
        private static readonly object _lock = new object();
        public static AppContext GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppContext();
                    }
                }
            }
            return instance;
        }
    }
}
