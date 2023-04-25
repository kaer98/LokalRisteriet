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
        public int Id { get; set; }
        public MainWindow mainWindow = MainWindow.GetInstance();
        public int CustomerID { get; set; }
        private BookingViewModel bookingViewModel;
        private CustomerViewModel customerViewModel;
        private RoomViewModel roomViewModel;

        public bool onoffIsDeleted = false;
        public EditBookingView()
        {
            InitializeComponent();
            bookingViewModel = new BookingViewModel();
            customerViewModel = new CustomerViewModel();
            roomViewModel = new RoomViewModel();
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

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            bookingViewModel.DeleteBookingByID(Id);

        }
        //Opdatering af booking.

        private void BtnCreateBooking_OnClick(object sender, RoutedEventArgs e)
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
            string bType = txtType.Text;
            int employeesAdult = dd18.SelectedIndex;
            int employeesChild = ddu18.SelectedIndex;
            if (txtDepositum.Text == null)
            {
                txtDepositum.Text = "0";
            }
            double bDepositum = double.Parse(txtDepositum.Text);
            Customer customer = customerViewModel.GetCustomerById(CustomerID);
           customer.CustomerEmail = bEmail;
           customer.CustomerName = bName;
            customer.CustomerPhoneNo = bPhone;
            customerViewModel.UpdateCustomer(customer);
            Booking booking = new Booking(bType, bNote, rooms(), bTimeStart, bTimeEnd, bNoOfPeople, false);
            booking.BookingCustomerID = customer.CustomerId;
            booking.EmployeesAdult = employeesAdult;
            booking.EmployeesChild = employeesChild;
            booking.Deposit = bDepositum;
            booking.BookingID = Id;
            bookingViewModel.UpdateBooking(booking);
        }
        public double CalculatePrice()
        {
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
            timePrice += employeesChild * 200*bookingDuration.Hours + employeesAdult * 400*bookingDuration.Hours;
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

        private void ResizeGrip_OnPointerMoved(object? sender, PointerEventArgs e)
        {

            Point currentPosition = e.GetPosition(txtNote.Parent);
            double horizontalChange = currentPosition.X - _startPoint.X;
            double verticalChange = currentPosition.Y - _startPoint.Y;

            double newWidth = _startWidth + horizontalChange;
            double newHeight = _startHeight + verticalChange;

            if (newWidth > txtNote.MinWidth)
            {
                txtNote.Width = newWidth;
            }

            if (newHeight > txtNote.MinHeight)
            {
                txtNote.Height = newHeight;
            }
        }

        private void ResizeGrip_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            e.Pointer.Capture(null);
        }


        
    }
}
