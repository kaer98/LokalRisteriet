
namespace LokalRisteriet.Models
{
    public class Task
    {
        public string TaskName { get; set; }
        public int TaskID { get; set; }
        public bool TaskIsDone { get; set; }
        public Employee TaskEmployee { get; set; }

        public int TaskBookingID { get; set; }

        public Task(string name)
        {
            TaskName = name;
            TaskIsDone = false;
        }
        public Task() { }
    }
}
