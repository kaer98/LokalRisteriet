﻿using System;
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

        public MainViewVM()
        {
          _bookings = new ObservableCollection<Booking>();
    
       


            BookingCommand = new RelayCommand(() =>
            {

            });
        }

        public void MarkBookings(Booking b)
        {
            Bookings.Add(b);
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
        public void MarkBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        public ObservableCollection<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value; }
        }

        public void AddManyBookings(ObservableCollection<Booking> bookings)
        {
            foreach (Booking booking in bookings)
            {
                MarkBooking(booking);
            }
        }







    }
}
