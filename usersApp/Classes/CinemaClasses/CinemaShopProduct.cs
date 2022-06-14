using System;

namespace usersApp.Classes.CinemaClasses
{
    public class CinemaShopProduct : CinemaProduct
    {
        public int Amount { get; set; }
        public CinemaShopProduct(string name, double price, int amount) : base(name, price)
        {
            Amount = amount;
        }
        public CinemaShopProduct() : base() { }
    }
}
