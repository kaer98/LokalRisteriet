using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class BookingViewModel
    {
        private BookingRepo _bookingRepo;
        private ObservableCollection<Booking> _books;


        public ObservableCollection<Booking> Bookings
        {
            get { return _books = new ObservableCollection<Booking>(_bookingRepo.GetAllBookings());}
            set { _books = value; }
        }
        public BookingViewModel()
        {
            _bookingRepo = new BookingRepo();
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
    }
}
