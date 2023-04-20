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

            try
            {
                editBookingView.txtName.Text = mainView.bookingViewModel.selectedBooking.Customer.CustomerName;
            }
            catch (Exception)
            {
                editBookingView.txtName.Text = "Ikke Angivet";
            }

            try
            {
                editBookingView.txtEmail.Text = mainView.bookingViewModel.selectedBooking.Customer.CustomerEmail;
            }
            catch (Exception)
            {
                editBookingView.txtEmail.Text = "Ikke Angivet";
            }

            try
            {
                editBookingView.txtPhoneNo.Text = mainView.bookingViewModel.selectedBooking.Customer.CustomerPhoneNo;
            }
            catch (Exception)
            {
                editBookingView.txtPhoneNo.Text = "Ikke Angivet";
            }

            editBookingView.tPStart.SelectedTime = mainView.bookingViewModel.selectedBooking.BookingStart.TimeOfDay;
                editBookingView.tPSlut.SelectedTime = mainView.bookingViewModel.selectedBooking.BookingEnd.TimeOfDay;
                editBookingView.dd18.SelectedIndex = mainView.bookingViewModel.selectedBooking.EmployeesAdult;
                editBookingView.ddu18.SelectedIndex = mainView.bookingViewModel.selectedBooking.EmployeesChild;
                editBookingView.txtType.Text = mainView.bookingViewModel.selectedBooking.BookingType;
                editBookingView.txtNote.Text = mainView.bookingViewModel.selectedBooking.BookingNote;
                editBookingView.txtGuest.Text =
                    mainView.bookingViewModel.selectedBooking.BookingAmountOfGuests.ToString();
      

            // double guestAmount = double.Parse(editBookingView.txtGuest.Text);
           // guestAmount =  mainView.bookingViewModel.selectedBooking.BookingAmountOfGuests;
           // editBookingView.txtGuest.Text = guestAmount.ToString();
           

           UserMiddleControl.Content = editBookingView;
            if (UserMiddleControl.Content == editBookingView)
            {
                MainViewBtn.IsVisible = true;

            }
            
            

        }

        
        //Booking Info View
        private void BookingBtnMainView(object sender, EventArgs e)
        {
            try
            {
                biv.choosenBookingName.Content = mainView.bookingViewModel.selectedBooking.Customer.CustomerName;
            }
            catch (Exception ex)
            {
                biv.choosenBookingName.Content = "(Personen har ingen navn!)";
            }

            biv.lblBookingType.Content = mainView.bookingViewModel.selectedBooking.BookingType;
            
            
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