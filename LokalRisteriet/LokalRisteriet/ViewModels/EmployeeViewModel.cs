using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;

namespace LokalRisteriet.ViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeRepo _employeeRepo;

        public EmployeeViewModel() => _employeeRepo = new EmployeeRepo();

        public void AddEmployee(Employee employee) => _employeeRepo.AddEmployee(employee);

        public void DeleteEmployee(Employee employee) => _employeeRepo.DeleteEmployee(employee);

        public List<Employee> GetEmployees() => _employeeRepo.GetAllEmployees();

        public void UpdateEmployee(Employee employee) => _employeeRepo.UpdateEmployee(employee);
    }
}
