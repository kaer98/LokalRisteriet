using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class MainViewVM
    {
        //test
       public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();


        public ICommand BookingCommand { get; }

        public MainViewVM()
        {
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room("bobRoom",34));

            Booking booking1 = new Booking("Fødselsdag", "skal bruge lokalet i mindst 5 timer", rooms, DateTime.Now, DateTime.Now, 30, false);

            

            Bookings.Add(booking1);
            
       


            BookingCommand = new RelayCommand(() =>
            {

            });
        }

    






    }
}
