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
        public EditBookingView editBookingView = new EditBookingView();

        public MainWindow()
        {
            InitializeComponent();
            mainView.BookingViewEvent += BookingBtnMainView;
            mainView.AddBookingViewEvent += AddBookingView;
            mainView.EditBookingViewEvent += MainView_EditBookingViewEvent;


            UserMiddleControl.Content = mainView;
            MainViewBtn.IsVisible = false;

         
        }

        private void MainView_EditBookingViewEvent(object? sender, EventArgs e)
        {

            UserMiddleControl.Content = editBookingView;
            if (UserMiddleControl.Content == editBookingView)
            {
                MainViewBtn.IsVisible = true;

            }

        }

        private void BookingBtnMainView(object sender, EventArgs e)
        {
            UserMiddleControl.Content = biv;
            if (UserMiddleControl.Content == biv)
            {
                MainViewBtn.IsVisible = true;

            }

        }

        private void AddBookingView(object sender, EventArgs e)
        {
            UserMiddleControl.Content = addBookingView;
            if (UserMiddleControl.Content == addBookingView)
            {
                MainViewBtn.IsVisible = true;

            }

        }

        private void Booking_Button(object sender, RoutedEventArgs e)
        {
            
            UserMiddleControl.Content = biv;
            if (UserMiddleControl.Content == biv)
            {
                MainViewBtn.IsVisible = true;

            }
          


        }

  

        private void MainView_Button(object sender, RoutedEventArgs e)
        {

          
                UserMiddleControl.Content = mainView;
                if (UserMiddleControl.Content == mainView)
                {
                MainViewBtn.IsVisible = false;

            }




        }
    }
}