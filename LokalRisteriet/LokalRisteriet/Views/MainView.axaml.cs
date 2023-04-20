using Avalonia.Controls;
using Avalonia.Interactivity;
using LokalRisteriet.ViewModels;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia.Collections;
using LokalRisteriet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Controls.Primitives;
using Avalonia.Xaml.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Calendar = Avalonia.Controls.Calendar;

namespace LokalRisteriet.Views
{
    public partial class MainView : UserControl
    {

        
        public event EventHandler BookingViewEvent;
        public event EventHandler EditBookingViewEvent;
        public event EventHandler AddBookingViewEvent;
        public event EventHandler MarkDaysEvent;
        private BookingViewModel bookingViewModel;
        private MainViewVM bvm;
        

        public MainView()
        {
            InitializeComponent();
            bookingViewModel = new BookingViewModel();
            
            DataContext = bookingViewModel;

            _calcal.SelectedDatesChanged += (sender, args) =>
            {
                SelectedDates();
            };



        }



        private void Booking_Button2(object sender, RoutedEventArgs e)
        {

            BookingViewEvent?.Invoke(this, EventArgs.Empty);

        }

        private void AddBookingButton(object sender, RoutedEventArgs e)
        {

            AddBookingViewEvent?.Invoke(this, EventArgs.Empty);

        }

        private void EditBookingButton(object sender, RoutedEventArgs e)
        {

            EditBookingViewEvent?.Invoke(this, EventArgs.Empty);

        }
        private void MarkDays(object sender, RoutedEventArgs e)
        {
            _calcal.SelectedDates.Clear();
            ColorCal();
        }

        private void ColorCal()
        {
            bvm = new MainViewVM();
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();
            foreach (Booking b in bookingViewModel.Bookings)
            {
                if (b.BookingStart.Month == DateTime.Today.Month)
                {
                    bvm.MarkBooking(b);

                   
                   _calcal.SelectedDates.Add(b.BookingStart);
                }

            }
            DataContext = null;
            DataContext = bvm;
        }
        private void SelectedDates()
        {
            bvm = new MainViewVM();
            foreach (DateTime day in _calcal.SelectedDates)
            {
                bvm.AddManyBookings(bookingViewModel.GetBookingByDay(day));
            }
            DataContext = null;
            DataContext = bvm;
           

        }
    
    }
}
