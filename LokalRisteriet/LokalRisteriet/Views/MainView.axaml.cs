using Avalonia.Controls;
using Avalonia.Interactivity;
using LokalRisteriet.ViewModels;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia.Collections;
using LokalRisteriet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LokalRisteriet.Views
{
    public partial class MainView : UserControl
    {

        MainViewVM MainViewVm = new MainViewVM();
        
        public event EventHandler BookingViewEvent;
        public event EventHandler AddBookingViewEvent;

        public MainView()
        {
            InitializeComponent();
            DataContext = MainViewVm;
            BookingViewModel bookingViewModel = new BookingViewModel();
            
            DataContext = bookingViewModel;

            

        }

        private void Booking_Button2(object sender, RoutedEventArgs e)
        {

            BookingViewEvent?.Invoke(this, EventArgs.Empty);

        }

        private void AddBookingButton(object sender, RoutedEventArgs e)
        {

            AddBookingViewEvent?.Invoke(this, EventArgs.Empty);

        }


    }
}
