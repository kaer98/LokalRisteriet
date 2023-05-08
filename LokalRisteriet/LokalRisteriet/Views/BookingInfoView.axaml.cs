using Avalonia.Controls;
using Avalonia.Interactivity;
using LokalRisteriet.Models;
using LokalRisteriet.ViewModels;
using System;
using System.Data;
using System.Linq;

namespace LokalRisteriet.Views
{
    public partial class BookingInfoView : UserControl
    {
        private BookingInfoViewVM bookingInfoViewVM;
        private AddOnViewModel addOnViewModel;
        private TaskViewModel taskViewModel;
        public Booking CurrBooking { set; get; }
        public int Id { set; get; }
        public BookingInfoView()
        {
            InitializeComponent();
            UpdateDataContext();
            addOnViewModel = new AddOnViewModel();
            taskViewModel = new TaskViewModel();
            
          //  UpdateEstimatedPrice();

        }


        public void SetListBoxTasks()
        {
            bookingInfoViewVM = new BookingInfoViewVM();
            bookingInfoViewVM.AddListBoxes(Id);
            DataContext = bookingInfoViewVM;

        }

        //method for adding a new task to a booking
        private void AddTasksButton_OnClick(object? sender, RoutedEventArgs e)
        {
            string taskName = string.Empty;
            if (txtTask.Text.Length > 0)
            {
                taskName = txtTask.Text;

                Task task = new Task(taskName);
                task.TaskBookingID = Id;
                taskViewModel.AddTask(task);
                txtTask.Text = string.Empty;
                UpdateDataContext();
                lblErrortask.Content = "";
            }
            else
            {
                lblErrortask.Content = "intet skrevet i opgave felt!";
            }


        }

        // Function for Delete Tasks Button Click Event.
        private void DeleteTasksButton_OnClick(object? sender, RoutedEventArgs e)
        {
            if (bookingInfoViewVM.selectedTask != null)
            {
                taskViewModel.DeleteTask(bookingInfoViewVM.SelectedTask);
                UpdateDataContext();
            }
            else
            {
                lblErrortask.Content = "ingen opgave valgt!";
            }
        }

        // AddProduct method to create and add a new add-on product to a booking
        private void AddProduct(object? sender, RoutedEventArgs e)
        {
            
            string productName = "";
            int productAmount = 0;
            double productPrice = 0;
            if (txtProductName.Text != null)
            {
                productName = txtProductName.Text;
            }
            if (txtProductAmount.Text != null && int.TryParse(txtProductAmount.Text,out productAmount))
            {
                productAmount = int.Parse(txtProductAmount.Text);
            }
            if (txtProductPrice.Text != null && double.TryParse(txtProductPrice.Text,out productPrice))
            {
                productPrice = double.Parse(txtProductPrice.Text);
            }
            else
            {
                lblErroraddOn.Content = "mangler information om produkt!";
                return;
            }
            AddOn addOn = new AddOn(productName, productPrice);
            addOn.Amount = productAmount;
            addOn.AddOnBookingID = Id;
            addOnViewModel.AddAddOn(addOn);
            UpdateDataContext();

            txtProductAmount.Text = String.Empty; txtProductPrice.Text = String.Empty; txtProductName.Text = String.Empty;
        }

        // Update Product Function
        private void UpdateProduct(object? sender, RoutedEventArgs e)
        {
            AddOn addOn = bookingInfoViewVM.SelectedAddOn;
            if (addOn != null)
            {
                addOnViewModel.UpdateAddOn(addOn);
                lblErroraddOn.Content = "";
                UpdateDataContext();
            }
            else
            {
                lblErroraddOn.Content = "intet valgt!";
            }

        }

        // Delete Product Function
        private void DeleteProduct(object? sender, RoutedEventArgs e)
        {
            if (bookingInfoViewVM.SelectedAddOn != null)
            {
                addOnViewModel.DeleteAddOn(bookingInfoViewVM.SelectedAddOn);
                UpdateDataContext();
                lblErroraddOn.Content = "";
            }
            else
            {
                lblErroraddOn.Content = "intet produkt valgt!";
            }
        }

        // Update Selected Task Done status
        private void btnDoneTask(object? sender, RoutedEventArgs e)
        {
            Task task = bookingInfoViewVM.SelectedTask;
            if (task != null)
            {
                taskViewModel.UpdateTask(task);
                lblErrortask.Content = "";
                UpdateDataContext();

            }
            else
            {
                lblErrortask.Content = "Intet Markeret!";
            }

        }
        private void UpdateDataContext()
        {
            bookingInfoViewVM = new BookingInfoViewVM();
            bookingInfoViewVM.AddListBoxes(Id);
            if (CurrBooking != null)
            {
                CurrBooking.BookingAddOns = addOnViewModel.GetAddOnByBookingID(Id);
                lblEstPrice.Content = $"Estimerede pris: {CurrBooking.CalculatePrice()} Kr.";

            }
                DataContext = bookingInfoViewVM;
        //    UpdateEstimatedPrice();

        }
        //private void UpdateEstimatedPrice()
        //{ if (bookingInfoViewVM.GetBookingByID(Id) != null && bookingInfoViewVM.AddOns != null)
        //    {
        //        bookingInfoViewVM.GetBookingByID(Id).BookingAddOns = bookingInfoViewVM.AddOns.ToList();
        //        lblEstPrice.Content = $"Estimerede pris: {bookingInfoViewVM.GetBookingByID(Id).BookingPrice}";
        //    }
        //}
    }
}
