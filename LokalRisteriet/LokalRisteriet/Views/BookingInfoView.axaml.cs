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
            bookingInfoViewVM.AddTasks(Id);
            DataContext = bookingInfoViewVM;
            addOnViewModel = new AddOnViewModel();
            taskViewModel = new TaskViewModel();
        }

        public void SetListBoxTasks()
        {
            bookingInfoViewVM = new BookingInfoViewVM();
            bookingInfoViewVM.AddTasks(Id);
            DataContext = bookingInfoViewVM;
        }

        private void Popup_OnClosed(object? sender, EventArgs e)
        {
            
        }

        private void Popup_OnOpened(object? sender, EventArgs e)
        {
            
        }

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
            
            // var popupContent = new Grid()
            // {
            //     Background = Brushes.White,
            //     Width = 300,
            //     Height = 200,
            // };
            //
            // popupContent.Children.Add(new TextBlock()
            // {
            //     Text = "Popup content",
            //     HorizontalAlignment = HorizontalAlignment.Center,
            //     VerticalAlignment = VerticalAlignment.Center,
            // });
            // info_Popup.Child = popupContent;
            
        }

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
    }
}
