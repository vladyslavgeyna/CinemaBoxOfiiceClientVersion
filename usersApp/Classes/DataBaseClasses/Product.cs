using System;

namespace usersApp.Classes.DataBaseClasses
{
    public class Product
    {
        public int id { get; set; }
        private string name;
        private int amount, user_id;
        private double price;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public int User_Id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public Product() { }
        public Product(string name, double price, int user_id, int amount)
        {
            this.name = name;
            this.price = price;
            this.user_id = user_id;
            this.amount = amount;
        }
    }
}
