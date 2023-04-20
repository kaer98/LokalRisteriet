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
        private int _employeesAdult, _employeesChild;
        private List<Task> _tasks;
        private List<AddOn> _addOns;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private TimeSpan _duration;
        private int _customerID;
        private Customer _customer;
        private double _price;
        private double _amountOfGuests;
        private bool _reserved;
        private double _deposit;




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

        public int EmployeesAdult { get => _employeesAdult; set => _employeesAdult = value; }
        public int EmployeesChild { get => _employeesChild; set => _employeesChild = value; }
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


        public Booking(string bookingtype, string bookingnote , List<Room> rooms, DateTime startDateTime, DateTime endDateTime, double amountOfGuests, bool reserved)
        {
            _bookingtype = bookingtype;
            _bookingnote = bookingnote;
            _rooms = rooms;
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            _duration = endDateTime - startDateTime;
            _amountOfGuests = amountOfGuests;
            _reserved = reserved;
            _tasks = new List<Task>();
            _addOns = new List<AddOn>();



        }

        public void CalculatePrice()
        {
            TimeSpan over5 = TimeSpan.FromHours(5);
            double timePrice = 0;
            foreach (AddOn addOn in _addOns)
            {
                timePrice += addOn.Price;
            }
            if (BookingDuration.Hours >=6)
            {
                
                timePrice = 5000+((BookingDuration.Hours-6) * 1000);

            }
            else 
            { 
                timePrice = BookingDuration.Hours * 1000;
                timePrice += EmployeesAdult * 400;
                timePrice += EmployeesChild * 200;
            }
            _price = timePrice;
        }
        
    }
}
