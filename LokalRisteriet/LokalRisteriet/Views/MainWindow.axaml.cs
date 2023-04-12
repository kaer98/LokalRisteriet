using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace LokalRisteriet.Views
{
    public partial class MainWindow : Window
    {

        public BookingInfoView biv = new BookingInfoView();
        public MainView        mainView = new MainView();
        public AddBookingView addBookingView = new AddBookingView();

        public MainWindow()
        {
            InitializeComponent();
            mainView.BookingViewEvent += BookingBtnMainView;
            mainView.AddBookingViewEvent += AddBookingView;


            UserMiddleControl.Content = mainView;
            MainViewBtn.IsEnabled = false;

         
        }

        private void BookingBtnMainView(object sender, EventArgs e)
        {
            UserMiddleControl.Content = biv;
            if (UserMiddleControl.Content == biv)
            {
                MainViewBtn.IsEnabled = true;

            }

        }

        private void AddBookingView(object sender, EventArgs e)
        {
            UserMiddleControl.Content = addBookingView;
            if (UserMiddleControl.Content == addBookingView)
            {
                MainViewBtn.IsEnabled = true;

            }

        }

        private void Booking_Button(object sender, RoutedEventArgs e)
        {
            
            UserMiddleControl.Content = biv;
            if (UserMiddleControl.Content == biv)
            {
                MainViewBtn.IsEnabled = true;

            }
          


        }

        private void MainView_Button(object sender, RoutedEventArgs e)
        {

          
                UserMiddleControl.Content = mainView;
                if (UserMiddleControl.Content == mainView)
                {
                    MainViewBtn.IsEnabled = false;

            }




        }
    }
}