using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private BookingRepo _bookingRepo;
        private CustomerRepo _customerRepo;
        private ObservableCollection<Booking> _books;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<Booking> _markedBookings;

        public ObservableCollection<Booking> Bookings
        {
            get { return _books = new ObservableCollection<Booking>(_bookingRepo.GetAllBookings());}
            set { _books = value; }
        }

        public ObservableCollection<Customer> Customers
        {
            get { return _customers = new ObservableCollection<Customer>(_customerRepo.GetAllCustomers()); }
            set { _customers = value; }
        }

        public void MarkBooking(Booking booking)
        {
            _markedBookings.Add(booking);
        }

        public ObservableCollection<Booking> MarkedBookings
        {
            get { return _markedBookings; }
            set { _markedBookings = value; }
        }
        public BookingViewModel()
        {
            _bookingRepo = new BookingRepo();
            _customerRepo = new CustomerRepo();
            _books = Bookings;
            _customers = new ObservableCollection<Customer>(_customerRepo.GetAllCustomers());
            AddCustomers();
            _markedBookings = new ObservableCollection<Booking>();
        }

        public Booking AddBooking(Booking booking)
        {
           return _bookingRepo.AddBooking(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            _bookingRepo.DeleteBooking(booking);
        }

        public List<Booking> GetBookings()
        {
            return _bookingRepo.GetAllBookings();
        }

        public void UpdateBooking(Booking booking)
        {
            _bookingRepo.UpdateBooking(booking);
        }

        public void AddCustomers()
        {
            foreach (Booking book in _books)
            {
                if (book.BookingCustomerID != 0)
                {
                    foreach (Customer cust in _customers)
                    {
                        if (book.BookingCustomerID == cust.CustomerId)
                        {
                            book.Customer = cust;
                        }
                    }
                }
            }
            
        }

        public ObservableCollection<Booking> GetBookingByDay(DateTime Day) => _bookingRepo.GetBookingByDay(Day);

        public Booking selectedBooking;
        public Booking SelectedBooking
        {
            get
            {
                return selectedBooking;
            }

            set
            {
                selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }

        #region OnChanged events
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion

    }
}
