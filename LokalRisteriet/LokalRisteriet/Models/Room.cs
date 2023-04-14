
namespace LokalRisteriet.Models
{
    public class Room
    {
        public string RoomName { get; private set; }
        public int Capacity { get; private set; }

        public Room(string name, int capacity)
        {
            RoomName = name;
            Capacity = capacity;
        }
    }
}
