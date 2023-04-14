using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class RoomViewModel
    {
        private RoomRepo _roomRepo;

        public RoomViewModel()
        {
            _roomRepo = new RoomRepo();
        }

        public void AddRoom(Room room)
        {
            _roomRepo.AddRoom(room);
        }

        public void DeleteRoom(Room room)
        {
            _roomRepo.DeleteRoom(room);
        }

        public List<Room> GetRooms()
        {
            return _roomRepo.GetAllRooms();
        }

        public void UpdateRoom(Room room)
        {
            _roomRepo.UpdateRoom(room);
        }
    }
}
