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
using MessageBoxSlim.Avalonia;
using MessageBoxSlim.Avalonia.DTO;
using MessageBoxSlim.Avalonia.Enums;
using MessageBoxSlim.Avalonia.Views;
using Microsoft.IdentityModel.Tokens;

namespace LokalRisteriet.Views
{
    public partial class MainView : UserControl
    {


        public event EventHandler BookingViewEvent;
        public event EventHandler EditBookingViewEvent;
        public event EventHandler AddBookingViewEvent;
        //public MainViewVM bvm;
        public BookingViewModel bookingViewModel = new BookingViewModel();


        public MainView()
        {
            InitializeComponent();
            editBookingBtn.IsEnabled = false;
            btn.IsEnabled = false;
   

            _calcal.SelectedDatesChanged += (sender, args) =>
            {
                SelectedDates();
            };
            //DataContext = bookingViewModel;

            //            ColorCal();
        }


        // Booking error handler
        private void Booking_Button2(object sender, RoutedEventArgs e)
        {
            if (bookingViewModel.SelectedBooking != null)
            {
                BookingViewEvent?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                lblError.Content = "Der er ikke valgt en booking";
            }
        }

        // AddBookingButton method
        private void AddBookingButton(object sender, RoutedEventArgs e)
        {

            AddBookingViewEvent?.Invoke(this, EventArgs.Empty);
            lblError.Content = "";

        }

        // Edit Booking Button Function
        private void EditBookingButton(object sender, RoutedEventArgs e)
        {
            if ( bookingViewModel.selectedBooking!= null)
            {
                EditBookingViewEvent?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                lblError.Content = "Der er ikke valgt en booking";
            }

           
        }

        // MarkDays Method
        private void MarkDays(object sender, RoutedEventArgs e)
        {
            ColorCal();
            lblError.Content = "";
            
            
        }

        private void ColorCal()
        {
            // Clear existing selected dates
            _calcal.SelectedDates.Clear();

            // Initialize bookingViewModel
            bookingViewModel = new BookingViewModel();

            // Iterate over bookings and mark those within the current month/year
            foreach (Booking b in bookingViewModel.Bookings1)
            {
                if (b.BookingStart.Month == _calcal.DisplayDate.Month && b.BookingStart.Year == _calcal.DisplayDate.Year)
                {
                    bookingViewModel.MarkBookings(b);
                    _calcal.SelectedDates.Add(b.BookingStart);
                    
                }
            }

            // Update data context to reflect changes
            DataContext = null;
            DataContext = bookingViewModel;
        }

        // retrieves the selected dates from a calendar control and uses them to add bookings to the view model, which is then updated in the UI.
        private void SelectedDates()
        {
            bookingViewModel = new BookingViewModel();
            foreach (DateTime day in _calcal.SelectedDates)
            {
                ObservableCollection<Booking> bookings = bookingViewModel.GetBookingByDay(day);
                if (!bookings.IsNullOrEmpty())
                {
                    bookingViewModel.SelectedBooking = bookings[0];
                }
                bookingViewModel.AddManyBookings(bookings);
            }
            DataContext = null;
            DataContext = bookingViewModel;



        }

        // This method handles the selection changed event of the booking list and enables or disables the edit and delete buttons based on whether an item is selected or not.
        private void BookingList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            bool editBtnOnOff = false;
            
            if (bookingList.SelectedItem == null)
            {
                editBtnOnOff = false;
            }
            else
            {
                editBtnOnOff = true;
            }
            
            if (editBtnOnOff == true)
            {
                editBookingBtn.IsEnabled = true;
                btn.IsEnabled = true;
            }
            else if (editBtnOnOff == false)
            {
                editBookingBtn.IsEnabled = false;
                btn.IsEnabled = false;
            }


        }

    }
}