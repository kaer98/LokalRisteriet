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
