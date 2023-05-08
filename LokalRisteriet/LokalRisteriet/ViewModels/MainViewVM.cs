using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class MainViewVM : INotifyPropertyChanged
    {
        //test
        public ObservableCollection<Booking> _bookings;


        public ICommand BookingCommand { get; }

        // Constructor of MainViewVM class.
        public MainViewVM()
        {
          _bookings = new ObservableCollection<Booking>();
    
            //maybe delete, we dont know
            BookingCommand = new RelayCommand(() =>
            {

            });
        }
        
        



        // Property for Selected Booking.
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

        // MarkBooking Method for adding bookings to observable list
        public void MarkBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        // Bookings (ObservableCollection<Booking>)
        public ObservableCollection<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value; }
        }

        // Add multiple bookings to the list of bookings
        public void AddManyBookings(ObservableCollection<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                MarkBooking(booking);
            }
        }







    }
}
