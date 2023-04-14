using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalRisteriet.Models
{
    public class Booking
    {
        private string _bookingid;
        private string _bookingtype;
        private string _bookingnote;
        private string _room;
        private string _employee;
        private string _task;
        private string _addon;
        private DateTime _startdatetime;
        private DateTime _enddatetime;
        private int _duration;
        private string _customerid;
        private double _price;
        private double _amountofpeople;

        public string bookingid
        {
            get { return _bookingid; }
            set { _bookingid = value; }
        }


        public string bookingtype
        {
            get { return _bookingtype; }
            set { _bookingtype = value; }
        }

        public string bookingnote
        {
            get { return _bookingnote; }
            set { _bookingnote = value; }
        }

        public string room
        {
            get { return _room; }
            set { _room = value; }
        }


        public string employee
        {
            get { return _employee; }
            set { _employee = value; }
        }


        public string task
        {
            get { return _task; }
            set { _task = value; }
        }

        public string addon
        {
            get { return _addon; }
            set { _addon = value; }
        }


        public DateTime startdatetime
        {
            get { return _startdatetime; }
            set { _startdatetime = value; }
        }


        public DateTime enddatetime
        {
            get { return _enddatetime; }
            set { _enddatetime = value; }
        }


        public int duration
        {
            get { return _duration; }
            set { _duration = value; }
        }


        public string customerid
        {
            get { return _customerid; }
            set { _customerid = value; }
        }


        public double price
        {
            get { return _price; }
            set { _price = value; }
        }


        public double amountofpeople
        {
            get { return _amountofpeople; }
            set { _amountofpeople = value; }
        }

        public Booking(string bookingid, string bookingtype, string bookingnote , string room, string employee, string task, string addon, DateTime startdatetime, DateTime enddatetime, int duration, string customerid, double price, double amountofpeople)
        {
            _bookingid = bookingid;
            _bookingtype = bookingtype;
            _bookingnote = bookingnote;
            _room = room;
            _employee = employee;
            _task = task;
            _addon = addon;
            _startdatetime = startdatetime;
            _enddatetime = enddatetime;
            _duration = duration;
            _customerid = customerid;
            _price = price;
            _amountofpeople = amountofpeople;
        }
    }
}
