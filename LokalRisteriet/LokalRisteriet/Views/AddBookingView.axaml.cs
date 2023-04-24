using Avalonia.Controls;
using Avalonia.Interactivity;
using LokalRisteriet.Models;
using LokalRisteriet.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Avalonia;
using Avalonia.Input;

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
            tPStart.SelectedTimeChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };
            tPSlut.SelectedTimeChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };
            ddu18.SelectionChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };
            dd18.SelectionChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };





        }

        public double CalculatePrice()
        {
            TimeSpan bookingDuration = new TimeSpan();
            double timePrice = 0;
            if (tPSlut.SelectedTime != null && tPStart.SelectedTime != null)
            {
                if (tPStart.SelectedTime.Value > tPSlut.SelectedTime)
                {
                    bookingDuration = (tPSlut.SelectedTime.Value+TimeSpan.FromHours(24)) - tPStart.SelectedTime.Value;
                }
                else
                {
                    bookingDuration = tPSlut.SelectedTime.Value - tPStart.SelectedTime.Value;
                }
                
            }
            else
            {
                return 0;
            }

            int employeesAdult = 0;
            if (dd18.SelectedIndex != -1)
            {
                employeesAdult = dd18.SelectedIndex;
            }
            int employeesChild = 0;
            if (ddu18.SelectedIndex != -1)
            {
                employeesChild = ddu18.SelectedIndex;
            }
                

            if (bookingDuration.Hours >= 6)
            {

                timePrice = 5000 + ((bookingDuration.Hours - 6) * 1000);
            }
            else
            {
                timePrice = bookingDuration.Hours * 1000;
            }

            timePrice += employeesChild * 200 + employeesAdult * 400;

            return timePrice;
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
            if (txtDepositum.Text == null)
            {
                txtDepositum.Text = "0";
            }
            double bDepositum = double.Parse(txtDepositum.Text);
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
            booking.EmployeesAdult = employeesAdult;
            booking.EmployeesChild = employeesChild;
            booking.Deposit = bDepositum;
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


    

        private void TxtNote_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            
        }

        private void TxtNote_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox1 = (TextBox)sender;
                int caretIndex = textBox1.CaretIndex;
                textBox1.Text = textBox1.Text.Insert(caretIndex, "\r\n");
                textBox1.CaretIndex = caretIndex + 2; // Move caret to end of new line
                e.Handled = true;
            }        }

        private void btnReserve(object? sender, RoutedEventArgs e)
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
            if (txtDepositum.Text == null)
            {
                txtDepositum.Text = "0";
            }
            double bDepositum = double.Parse(txtDepositum.Text);
            Customer customer = customerViewModel.GetCustomerByEmail(bEmail);
            if (customer == null)
            {
                Customer c = new Customer(bName, bPhone, bEmail);
                c.CustomerId = customerViewModel.GetNextID();
                customerViewModel.AddCustomer(c);

                customer = c;
            }


            Booking booking = new Booking(bType, bNote, rooms(), bTimeStart, bTimeEnd, bNoOfPeople, true);
            booking.BookingCustomerID = customer.CustomerId;
            booking.EmployeesAdult = employeesAdult;
            booking.EmployeesChild = employeesChild;
            booking.Deposit = bDepositum;
            bookingViewModel.AddBooking(booking);
        }

}
    }
}
