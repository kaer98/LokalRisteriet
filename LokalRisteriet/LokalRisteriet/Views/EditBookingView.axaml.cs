using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using LokalRisteriet.Models;
using LokalRisteriet.ViewModels;

namespace LokalRisteriet.Views
{
    public partial class EditBookingView : UserControl
    {
        private Booking _currBooking;
        public event EventHandler BackToMain;

        public int Id { get; set; }
        public int CustomerID { get; set; }
        private BookingViewModel bookingViewModel;
        private CustomerViewModel customerViewModel;
        private RoomViewModel roomViewModel;
        public EditBookingView()
        {
            InitializeComponent();
            bookingViewModel = new BookingViewModel();
            customerViewModel = new CustomerViewModel();
            roomViewModel = new RoomViewModel();
            //make lists for comboboxes when choosing amount of employees both adults and non adults
            dd18.Items = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            // update estimated price when changing time and amount of employees
            tPStart.SelectedTimeChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };
            tPSlut.SelectedTimeChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };
            dd18.SelectionChanged += (sender, e) =>
            {
                txtPrice.Text = CalculatePrice().ToString();
            };

        }

        // Delete Booking by ID Function
        private void btnDelete(object sender, RoutedEventArgs e)
        {
            bookingViewModel.DeleteBooking(CurrBooking);
            BackToMain?.Invoke(this, EventArgs.Empty);

        }

        public Booking CurrBooking{
            get { return _currBooking; }
            set { _currBooking = value; }
        }

        // Create a new booking
        private void BtnCreateBooking_OnClick(object sender, RoutedEventArgs e)
        {
            // Initialize variables to store user input
            string bName = "";
            string bPhone = "";
            string bEmail = "";
            DateTime bTimeStart = new DateTime();
            DateTime bTimeEnd = new DateTime();
            int bNoOfPeople = 0;
            string bNote = "";
            bool bRoom1 = false;
            bool bRoom2 = false;
            string bType = "";
            int employee = 0;
            double bDepositum = 0;
            BackToMain?.Invoke(this, EventArgs.Empty);



            // Get user input from UI controls
            if (txtName.Text != null)
            {
                bName = txtName.Text;
            }

            if (txtPhoneNo.Text != null)
            {
                bPhone = txtPhoneNo.Text;
            }

            if (txtEmail.Text != null)
            {
                bEmail = txtEmail.Text;
            }

            // Check if customer already exists based on email provided
            Customer customer = customerViewModel.GetCustomerByEmail(bEmail);
            if (customer == null)
            {
                // Create new customer object if not found
                Customer c = new Customer(bName, bPhone, bEmail);
                c.CustomerId = customerViewModel.GetNextID();
                customerViewModel.AddCustomer(c);

                customer = c;
            }

            if (dPDate.SelectedDate != null && tPStart.SelectedTime != null && tPSlut.SelectedTime != null)
            {
                CurrBooking.BookingStart = dPDate.SelectedDate.Value.DateTime + tPStart.SelectedTime.Value;
                CurrBooking.BookingEnd = dPDate.SelectedDate.Value.DateTime + tPSlut.SelectedTime.Value;
            }

            if (txtGuest.Text != null&& int.TryParse(txtGuest.Text,out bNoOfPeople))
            {
                CurrBooking.BookingAmountOfGuests = int.Parse(txtGuest.Text);
            }

            if (txtNote.Text != null)
            {
                CurrBooking.BookingNote = txtNote.Text;
            }
            bRoom1 = cbRoom1.IsChecked.Value;
            bRoom2 = cbRoom2.IsChecked.Value;
            
            if (txtType.Text != null)
            {
                CurrBooking.BookingType = txtType.Text;
            }

            if (dd18.SelectedIndex != -1)
            {
                CurrBooking.Employee = dd18.SelectedIndex;
            }



            if (txtDepositum.Text != null && double.TryParse(txtDepositum.Text, out bDepositum))
            {
                CurrBooking.Deposit = double.Parse(txtDepositum.Text);
            }
            else
            {
                CurrBooking.Deposit = 0;
            }

            CurrBooking.BookingRooms = rooms();
            CurrBooking.BookingCustomerID = customer.CustomerId;
            
                CurrBooking.BookingReserved = cbReserved.IsChecked.Value;
            bookingViewModel.UpdateBooking(CurrBooking);
            bookingViewModel.selectedBooking= CurrBooking;
        }

        // Calculate Booking Price
        public double CalculatePrice()
        {
            // Time Calculation
            TimeSpan bookingDuration = new TimeSpan();
            double timePrice = 0;
            if (tPSlut.SelectedTime != null && tPStart.SelectedTime != null)
            {
                if (tPStart.SelectedTime.Value > tPSlut.SelectedTime)
                {
                    bookingDuration = (tPSlut.SelectedTime.Value + TimeSpan.FromHours(24)) - tPStart.SelectedTime.Value;
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
            // Adult Employee Count.
            int employee = 0;
            if (dd18.SelectedIndex != -1)
            {
                employee = dd18.SelectedIndex;
            }
            
            if (bookingDuration.Hours >= 6)
            {

                timePrice = 5000 + ((bookingDuration.Hours - 6) * 1000);
            }
            else
            {
                timePrice = bookingDuration.Hours * 1000;
            }
            // Price calculation based on number of employees and Duration
            timePrice += employee * 400*bookingDuration.Hours;
            return timePrice;
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
            if (cbRoom2.IsChecked.Value)
            {
                room2 = roomViewModel.GetRooms()[1];
            }
            list.Add(room);
            list.Add(room2);
            return list;


        }
        
        private void TxtNote_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                int caretIndex = textBox.CaretIndex;
                textBox.Text = textBox.Text.Insert(caretIndex, "\r\n");
                textBox.CaretIndex = caretIndex + 2; // Move caret to end of new line
                e.Handled = true;
            }
        }

        private void TxtNote_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {

        }

        //Resize Note Box
        private Point _startPoint;
        private double _startWidth;
        private double _startHeight;

        private void ResizeGrip_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _startPoint = e.GetPosition(txtNote.Parent);
            _startWidth = txtNote.Width;
            _startHeight = txtNote.Height;

        }

        


        
    }
}
