using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;

namespace LokalRisteriet.Persistence
{
    public class EmployeeRepo
    {
        private string _connectionString =
            "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        private List<Employee> _employees;

        public EmployeeRepo()
        {
            _employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["EmployeeId"].ToString());
                        string name = dr["EmployeeName"].ToString();
                        bool adult = bool.Parse(dr["EmployeeAdult"].ToString());
                        Employee employee = new Employee(id, name, adult);
                        _employees.Add(employee);
                    }
                }
            }
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Employee(EmployeeName, EmployeeAdult) VALUES(@name, @adult)", connection);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@adult", employee.IsAdult);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            int i = _employees.FindIndex(e => e.EmployeeID == employee.EmployeeID);
            _employees[i] = employee;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Employee SET EmployeeName = @name, EmployeeAdult = @adult WHERE EmployeeID = @id", connection);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@adult", employee.IsAdult);
                cmd.Parameters.AddWithValue("@id", employee.EmployeeID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            _employees.Remove(employee);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeID = @id", connection);
                cmd.Parameters.AddWithValue("@id", employee.EmployeeID);
                cmd.ExecuteNonQuery();
            }
        }

        public Employee GetEmployeeByName(string employeeName)
        {
            return _employees.Find(e => e.Name == employeeName);

        }

    }
}
