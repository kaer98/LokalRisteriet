using Avalonia.Controls;
using Avalonia.Interactivity;
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

    
    }
}
