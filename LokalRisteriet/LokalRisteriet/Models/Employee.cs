
namespace LokalRisteriet.Models
{
    public class Employee
    {
        public string EmployeeName { get; set; }
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


        public Employee(string name, bool isAdult)
        {
            EmployeeName = name;
            IsAdult = isAdult;
        }
        public Employee() { }
    }
}
