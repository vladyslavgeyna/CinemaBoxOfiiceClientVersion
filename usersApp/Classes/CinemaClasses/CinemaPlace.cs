using System;

namespace usersApp.Classes.CinemaClasses
{
    public class CinemaPlace
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CinemaPlace (CinemaPlace cinemaPlace)
        {
            Row = cinemaPlace.Row;
            Column = cinemaPlace.Column;
        }
        public CinemaPlace() { }
        public CinemaPlace(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
