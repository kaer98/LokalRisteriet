using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using LokalRisteriet.ViewModels;

namespace LokalRisteriet.Views
{
    public partial class BookingInfoView : UserControl
    {
        private BookingInfoViewVM bookingInfoViewVM = new BookingInfoViewVM();
        public BookingInfoView()
        {
            InitializeComponent();
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
    }
}
