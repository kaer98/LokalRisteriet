using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using JetBrains.Annotations;
using LokalRisteriet.Models;
using LokalRisteriet.ViewModels;

namespace LokalRisteriet.Views
{
    public partial class BookingInfoView : UserControl
    {
        private BookingInfoViewVM bookingInfoViewVM;
        private AddOnViewModel addOnViewModel;
        private TaskViewModel taskViewModel;
        public int Id { set; get; }
        public BookingInfoView()
        {
            InitializeComponent();
            bookingInfoViewVM = new BookingInfoViewVM();
            bookingInfoViewVM.AddListBoxes(Id);
            DataContext = bookingInfoViewVM;
            addOnViewModel = new AddOnViewModel();
            taskViewModel = new TaskViewModel();
            
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
            if(string.IsNullOrEmpty(taskName))
            {
                taskName = txtTask.Text;
            }
            Task task = new Task(taskName);
            task.TaskBookingID = Id;
            taskViewModel.AddTask(task);
            txtTask.Text= string.Empty;

            
        }

        // Function for Delete Tasks Button Click Event.
        private void DeleteTasksButton_OnClick(object? sender, RoutedEventArgs e)
        {
            taskViewModel.DeleteTask(bookingInfoViewVM.SelectedTask);
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
            if(txtProductAmount.Text != null)
            {
                productAmount = int.Parse(txtProductAmount.Text);
            }
            if(txtProductPrice.Text != null)
            {
                productPrice = Double.Parse(txtProductPrice.Text);
            }
            AddOn addOn = new AddOn(productName,productPrice);
            addOn.Amount = productAmount;
            addOn.AddOnBookingID = Id;
            addOnViewModel.AddAddOn(addOn);

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
            }
            else
            {
                lblErroraddOn.Content = "intet valgt!";
            }
            
        }

        // Delete Product Function
        private void DeleteProduct(object? sender, RoutedEventArgs e)
        {
            addOnViewModel.DeleteAddOn(bookingInfoViewVM.SelectedAddOn);
        }

        // Update Selected Task Done status
        private void btnDoneTask(object? sender, RoutedEventArgs e)
        {
            Task task = bookingInfoViewVM.SelectedTask;
            if(task != null)
            {
                taskViewModel.UpdateTask(task);
                lblErrortask.Content = "";
            }
            else
            {
                lblErrortask.Content = "Intet Markeret!";
            }
            
        }
    }
}
