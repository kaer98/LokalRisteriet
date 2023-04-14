
namespace LokalRisteriet.Models
{
    public class Room
    {
        public string RoomName { get; set; }
        public int Capacity { get; set; }

        public int RoomID { get; set; }

        public Room(string name, int capacity)
        {
            RoomName = name;
            Capacity = capacity;
        }
    }
}
