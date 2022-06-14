using System;

namespace usersApp.Classes.CinemaClasses
{
    public class CinemaOrder
    {
        public DateTime DateTimeOfOrder { get; set; }
        public string PaymentMethod { get; set; }
        public CinemaOrder(DateTime dateTimeOfOrder, string paymentMethod)
        {
            DateTimeOfOrder = dateTimeOfOrder;
            PaymentMethod = paymentMethod;
        }
        public CinemaOrder() { }
    }
}
