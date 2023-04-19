using Avalonia.Controls;
using Avalonia.Interactivity;
using LokalRisteriet.Models;
using LokalRisteriet.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace LokalRisteriet
{
    public partial class AddBookingView : UserControl
    {
        private BookingViewModel bookingViewModel;
        private RoomViewModel roomViewModel;
        private CustomerViewModel customerViewModel;

        public AddBookingView()
        {
            InitializeComponent();
             bookingViewModel = new BookingViewModel();
             roomViewModel = new RoomViewModel();
             customerViewModel = new CustomerViewModel();
            dd18.Items = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            ddu18.Items = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };


        }

        private void btnCreateBooking(object sender, RoutedEventArgs e)
        {
            string bName = txtName.Text;
            string bPhone = txtPhoneNo.Text;
            string bEmail = txtEmail.Text;
            DateTime bTimeStart = dPDate.SelectedDate.Value.DateTime + tPStart.SelectedTime.Value;
            DateTime bTimeEnd = dPDate.SelectedDate.Value.DateTime + tPSlut.SelectedTime.Value;
            int bNoOfPeople = int.Parse(txtGuest.Text);
            string bNote = txtNote.Text;
            bool bRoom1 = cbRoom1.IsChecked.Value;
            bool bRoom2 = cbRoom2.IsChecked.Value;
            string bType = txtTyoe.Text;
            int employeesAdult = dd18.SelectedIndex;
            int employeesChild = ddu18.SelectedIndex;
            Customer customer = customerViewModel.GetCustomerByEmail(bEmail);
            if (customer == null)
            {
                Customer c = new Customer(bName, bPhone, bEmail);
                c.CustomerId = customerViewModel.GetNextID();
                customerViewModel.AddCustomer(c);

                customer = c;
            }
            

            Booking booking = new Booking(bType, bNote,rooms(), bTimeStart,bTimeEnd,bNoOfPeople,false);
            booking.BookingCustomerID = customer.CustomerId;
            bookingViewModel.AddBooking(booking);
        }


        public List<Room> rooms()
        {
            List<Room> list = new List<Room>();
            Room room = null;
            Room room2 = null;
            if (cbRoom1.IsChecked.Value)
            {
                room = roomViewModel.GetRooms()[0];
            }
            if(cbRoom2.IsChecked.Value)
            {
                room2 = roomViewModel.GetRooms()[1];
            }
            list.Add(room);
            list.Add(room2);
            return list;
        }

    }
}
