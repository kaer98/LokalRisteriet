
namespace LokalRisteriet.Models
{
    public class Task
    {
        public string Name { get; private set; }
        public bool IsDone { get; set; }
        public Employee TaskEmployee { get; set; }

        public Task(string name)
        {
            Name = name;
            IsDone = false;
        }
    }
}
