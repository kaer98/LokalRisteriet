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
        // Fields
        private int _bookingid;
        private string _bookingtype;
        private string _bookingnote;
        private List<Room> _rooms;
        private int _employee;
        private List<Task> _tasks;
        private List<AddOn> _addOns;
        private DateTime _startDateTime, _endDateTime, _RegistrationDate;
        private TimeSpan _duration;
        private int _customerID;
        private Customer _customer;
        private double _price;
        private double _amountOfGuests;
        private bool _reserved;
        private double _deposit;

       
        // Properties
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



        public List<Task> BookingTasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        public List<AddOn> BookingAddOns
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
            get { CalculateDuration(); return _duration; }
            set { _duration = value; }
        }

        public DateTime RegistrationDate
        {
            get { return _RegistrationDate; }
            set { _RegistrationDate = value; }
        }

        public int BookingCustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }


        public double BookingPrice
        {
            get { return CalculatePrice(); }
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

        public int Employee { get => _employee; set => _employee = value; }
        public Customer Customer { get => _customer; set => _customer = value; }
        

        public void BookingAddTask(Task task)
        {
            task.TaskBookingID = BookingID;
            _tasks.Add(task);
        }


        public double Deposit
        {
            get { return _deposit; }
            set { _deposit = value; }
        }
        
        // Methods
        public Booking(string bookingtype, string bookingnote , List<Room> rooms, DateTime startDateTime, DateTime endDateTime, double amountOfGuests, bool reserved)
        {
            // Set booking properties
            _RegistrationDate = DateTime.Now;
            _bookingtype = bookingtype;
            _bookingnote = bookingnote;
            _rooms = rooms;
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;

            CalculateDuration();

            // Set other booking properties
            _amountOfGuests = amountOfGuests;
            _reserved = reserved;
            _tasks = new List<Task>();
            _addOns = new List<AddOn>();

            // Calculate booking price
            CalculatePrice();

        }

        public double CalculatePrice()
        {
            TimeSpan over5 = TimeSpan.FromHours(5);
            double timePrice = 0;

            // Add up the prices of all add-ons
            foreach (AddOn addOn in _addOns)
            {
                timePrice += addOn.TotalPrice;
            }

            // Calculate the price based on the booking duration
            if (BookingDuration.Hours >=6)
            {
                timePrice = 5000+((BookingDuration.Hours-6) * 1000);
                timePrice += Employee * 400*BookingDuration.Hours;
            }
            else 
            { 
                timePrice = BookingDuration.Hours * 1000;
                timePrice += Employee * 400 * BookingDuration.Hours;
            }

            // Store the calculated price and return it
            return _price = timePrice;
        }

        public void CalculateDuration()
        {

            if (_startDateTime > _endDateTime)
            {
                _duration = _endDateTime + TimeSpan.FromHours(24) - _startDateTime;
            }
            else
            {
                _duration = _endDateTime - _startDateTime;
            }

        }
        
    }
}
