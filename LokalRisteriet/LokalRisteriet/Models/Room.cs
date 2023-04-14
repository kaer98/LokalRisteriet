
namespace LokalRisteriet.Models
{
    internal class Room
    {
        public string Name { get; private set; }
        public int Capacity { get; private set; }

        public Room(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }
    }
}
