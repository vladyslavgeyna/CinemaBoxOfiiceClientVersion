using System;

namespace usersApp.Classes.CinemaClasses
{
    public abstract class CinemaProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        protected CinemaProduct(string name, double price)
        {
            Name = name;
            Price = price;
        }
        protected CinemaProduct() { }
    }
}
