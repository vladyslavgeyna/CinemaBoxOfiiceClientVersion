using System;

namespace usersApp.Classes.DataBaseClasses
{
    public class Movie
    {
        public int id { get; set; }
        private string name, timeOfSession, plan;
        private int dayOfSession, monthOfSession, yearOfSession, user_id, rowPlace, columnPlace;
        private double price;
        public int DayOfSession
        {
            get { return dayOfSession; }
            set { dayOfSession = value; }
        }
        public int MonthOfSession
        {
            get { return monthOfSession; }
            set { monthOfSession = value; }
        }
        public int YearOfSession
        {
            get { return yearOfSession; }
            set { yearOfSession = value; }
        }
        public int RowPlace
        {
            get { return rowPlace; }
            set { rowPlace = value; }
        }
        public int ColumnPlace
        {
            get { return columnPlace; }
            set { columnPlace = value; }
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
        public string Plan
        {
            get { return plan; }
            set { plan = value; }
        }
        public string TimeOfSession
        {
            get { return timeOfSession; }
            set { timeOfSession = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public Movie() { }
        public Movie(string name, string timeOfSession, int dayOfSession, int monthOfSession, int yearOfSession, int rowPlace, int columnPlace, double price, int user_id, string plan)
        {
            this.name = name;
            this.timeOfSession = timeOfSession;
            this.dayOfSession = dayOfSession;
            this.monthOfSession = monthOfSession;
            this.yearOfSession = yearOfSession;
            this.rowPlace = rowPlace;
            this.columnPlace = columnPlace;
            this.price = price;
            this.user_id = user_id;
            this.plan = plan;
        }
    }
}
