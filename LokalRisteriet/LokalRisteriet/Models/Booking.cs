using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;

namespace LokalRisteriet.Models
{
    public class Booking
    {
        private int _bookingid;
        private string _bookingtype;
        private string _bookingnote;
        private List<Room> _rooms;
        private List<Employee> _employees;
        private List<Task> _tasks;
        private List<AddOn> _addOns;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private TimeSpan _duration;
        private int _customerID;
        private double _price;
        private double _amountOfGuests;
        private bool _reserved;

       

        public int BookingID
        {
            get { return _bookingid; }
            set { _bookingid = value; }
        }


        public string BookingType
        {
            get { return _bookingtype; }
            set { _bookingtype = value; }
        }

        public string BookingNote
        {
            get { return _bookingnote; }
            set { _bookingnote = value; }
        }

        public List<Room> BookingRooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }


        public List<Employee> BookingEmployees
        {
            get { return _employees; }
            set { _employees = value; }
        }


        public List<Task> BookingTasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        public List<AddOn> BookingAddOn
        {
            get { return _addOns; }
            set { _addOns = value; }
        }


        public DateTime BookingStart
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }


        public DateTime BookingEnd
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }


        public TimeSpan BookingDuration
        {
            get { return _duration; }
            set { _duration = value; }
        }


        public int BookingCustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }


        public double BookingPrice
        {
            get { return _price; }
            set { _price = value; }
        }


        public double BookingAmountOfGuests
        {
            get { return _amountOfGuests; }
            set { _amountOfGuests = value; }
        }
        public bool BookingReserved
        {
            get { return _reserved; }
            set { _reserved = value; }
        }


        public Booking(int bookingid, string bookingtype, string bookingnote , List<Room> rooms, List<Employee> employees, List<AddOn> addOns, DateTime startDateTime, DateTime endDateTime, TimeSpan duration, int customerID, double price, double amountOfGuests, bool reserved)
        {
            _bookingid = bookingid;
            _bookingtype = bookingtype;
            _bookingnote = bookingnote;
            _rooms = rooms;
            _employees = employees;
            _addOns = addOns;
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            _duration = duration;
            _customerID = customerID;
            _price = price;
            _amountOfGuests = amountOfGuests;
            _reserved = reserved;
        }
    }
}
