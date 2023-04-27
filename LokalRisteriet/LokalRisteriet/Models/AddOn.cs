
using System;

namespace LokalRisteriet.Models
{
    public class AddOn
    {
        public string AddOnName { get; set; }
        public double Price { get; private set; }
        public int _amount = 0;
        private int _addOnID;
        private int _addOnBookingID;
        public double TotalPrice => _amount * Price;

        public int AddOnBookingID
        {
            get { return _addOnBookingID; }
            set { _addOnBookingID = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int AddOnID
        {
            get { return _addOnID; }
            set { _addOnID = value; }
        }


        public AddOn(string name, double price)
        {
            AddOnName = name;
            Price = price;
        }

        public AddOn() { }
    }
}
