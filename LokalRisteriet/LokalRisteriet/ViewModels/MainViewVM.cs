using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class MainViewVM : INotifyPropertyChanged
    {
        //test
       public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();


        public ICommand BookingCommand { get; }

        public MainViewVM()
        {
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room("bobRoom",34));

            Booking booking1 = new Booking("Fødselsdag", "skal bruge lokalet i mindst 5 timer", rooms, DateTime.Now, DateTime.Now, 30, false);

            
    
       


            BookingCommand = new RelayCommand(() =>
            {

            });
        }


        public Booking selectedBooking;
        public Booking SelectedBooking
        {
            get
            {
                return selectedBooking;
            }

            set
            {
                selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }

        #region OnChanged events
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
      
        #endregion








    }
}
