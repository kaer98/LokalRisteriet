using Avalonia.Controls;
using Avalonia.Interactivity;
using LokalRisteriet.ViewModels;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LokalRisteriet.Views
{
    public partial class MainView : UserControl
    {

        MainViewVM MainViewVm = new MainViewVM();
        
        public event EventHandler BookingViewEvent;

        public MainView()
        {
            InitializeComponent();
            DataContext = MainViewVm;

            

        }

        private void Booking_Button2(object sender, RoutedEventArgs e)
        {

            BookingViewEvent?.Invoke(this, EventArgs.Empty);

        }


    }
}
