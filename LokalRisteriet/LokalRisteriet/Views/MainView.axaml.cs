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

namespace LokalRisteriet.Views
{
    public partial class MainView : UserControl
    {

        
        public event EventHandler BookingViewEvent;
        public event EventHandler EditBookingViewEvent;
        public event EventHandler AddBookingViewEvent;

        public BookingViewModel bookingViewModel = new BookingViewModel();
        

        public MainView()
        {
            InitializeComponent();
           
            
            DataContext = bookingViewModel;

//            ColorCal();
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

        private void ColorCal()
        {


        }


    }
}
