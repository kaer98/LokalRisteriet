
namespace LokalRisteriet.Models
{
    internal class Task
    {
        public string Name { get; private set; }
        public bool IsDone { get; set; }
        public string EmployeeName { get; set; }

        public Task(string name, bool isDone, string employeeName)
        { 
            Name = name;
            IsDone = isDone;
            EmployeeName = employeeName;
        }
    }
}
