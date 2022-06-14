using System;

namespace usersApp.Classes.CinemaClasses
{
    public class CinemaMovie : CinemaProduct
    {
        public DateTime DateOfSession { get; set; }
        public string TimeOfSession { get; set; }
        public string Plan { get; set; }
        public CinemaPlace CinemaPlace { get; set; }
        public CinemaMovie(string name, double price, DateTime dateOfSession, string timeOfSession, CinemaPlace cinemaPlace, string plan) : base(name, price)
        {
            DateOfSession = dateOfSession;
            TimeOfSession = timeOfSession;
            CinemaPlace = new CinemaPlace(cinemaPlace);
            Plan = plan;
        }
        public CinemaMovie(string name, double price, DateTime dateOfSession, string timeOfSession, string plan) : base(name, price)
        {
            DateOfSession = dateOfSession;
            TimeOfSession = timeOfSession;
            Plan = plan;
        }
        public CinemaMovie() : base()
        {
            CinemaPlace = new CinemaPlace();
        }
    }
}
