
namespace LokalRisteriet.Models
{
    public class Employee
    {
        public string Name { get; private set; }
        public double Price
        {
            // Return 400 if employee is over 18 else 200
            get => IsAdult ? 400.0 : 200.0;
        }
        public bool IsAdult { get; private set; }

        private int _employeeID;

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }


        public Employee(int id, string name, bool isAdult)
        {
            _employeeID = id;
            Name = name;
            IsAdult = isAdult;
        }
    }
}
