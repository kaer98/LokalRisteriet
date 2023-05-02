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
using Task = LokalRisteriet.Models.Task;

namespace LokalRisteriet.ViewModels
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        // Repositories and collections
        private BookingRepo _bookingRepo;
        private TaskRepo _taskRepo;
        private CustomerRepo _customerRepo;
        private ObservableCollection<Booking> _books;
        private ObservableCollection<Booking> _books1;
        private ObservableCollection<Customer> _customers;

        // Properties
        public ObservableCollection<Booking> Bookings
        {
            get { return _books1; }
            set { _books1 = value; }
        }

        public ObservableCollection<Booking> Bookings1 { get { return _books; } }
        public ObservableCollection<Customer> Customers
        {
            get { return _customers = new ObservableCollection<Customer>(_customerRepo.GetAllCustomers()); }
            set { _customers = value; }
        }

        // Constructor
        public BookingViewModel()
        {
            _bookingRepo = new BookingRepo();
            _customerRepo = new CustomerRepo();
            _books = new ObservableCollection<Booking>(_bookingRepo.GetAllBookings());
            _customers = new ObservableCollection<Customer>(_customerRepo.GetAllCustomers());
            _books1 = new ObservableCollection<Booking>();
            AddCustomers();
        }

        // Methods for CRUD operations
        public Booking AddBooking(Booking booking)
        {
            return _bookingRepo.AddBooking(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            _bookingRepo.DeleteBooking(booking);
        }

        public void DeleteBookingByID(int id)
        {
            _bookingRepo.DeleteBookingByID(id);
        }

        public List<Booking> GetBookings()
        {
            return _bookingRepo.GetAllBookings();
        }

        public List<Task> GetTasks()
        {
            return _taskRepo.GetAllTasks();
        }

        public void UpdateBooking(Booking booking)
        {
            _bookingRepo.UpdateBooking(booking);
        }

        // AddCustomers method - Assign customers to bookings
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

        // retrieves all bookings on a given day.
        public ObservableCollection<Booking> GetBookingByDay(DateTime Day) => _bookingRepo.GetBookingByDay(Day);

        // adds a booking to the Bookings collection.
        public void MarkBookings(Booking b)
        {
            Bookings.Add(b);
        }

        // adds multiple bookings to the Bookings collection.
        public void AddManyBookings(ObservableCollection<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                MarkBookings(booking);
            }
        }

        // Selected booking property
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

        // INotifyPropertyChanged implementation
        #region OnChanged events
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion

    }
}