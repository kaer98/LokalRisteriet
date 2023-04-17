using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus;

namespace LokalRisteriet.Persistence
{
    public class CustomerRepo
    {
        private List<Customer> _customers;
        //private string connectionsString = ConfigurationManager.ConnectionStrings["Production"].ConnectionString;
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        public CustomerRepo()
        { 
         _customers = new List<Customer>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", sqlConnection);
                 using(SqlDataReader dr = cmd.ExecuteReader())
                 {
                    while (dr.Read())
                    {
                        string name = dr["CustomerName"].ToString();
                        string customerPhoneNo = dr["CustomerPhoneNO"].ToString();
                        int customerID = int.Parse(dr["CustomerID"].ToString());
                        string customerEmail = dr["CustomerEmail"].ToString();
                        Customer customer = new Customer(customerID, name, customerPhoneNo, customerEmail);
                        _customers.Add(customer);
                    }
                 }
            }
        }
        public List<Customer> GetAllCustomers => _customers;

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Customer(CustomerName, CustomerPhoneNO, CustomerEmail) VALUES(@name, @phoneNO, @eMail)", sqlConnection);
                cmd.Parameters.AddWithValue("@name", customer.CustomerName);
                cmd.Parameters.AddWithValue("@phoneNO", customer.CustomerPhoneNo);
                cmd.Parameters.AddWithValue("@eMail", customer.CustomerEmail);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            int i  = _customers.FindIndex(c => c.CustomerId == customer.CustomerId);
            _customers[i] = customer;
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Customer Set CustomerName = @name,CustomerEmail = @customerEmail, CustomerPhoneNo = @customerPhoneNo WHERE CustomerID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@name", customer.CustomerName);
                cmd.Parameters.AddWithValue("@customerEmail", customer.CustomerEmail);
                cmd.Parameters.AddWithValue("@customerPhoneNo", customer.CustomerPhoneNo);
                cmd.Parameters.AddWithValue("@id", customer.CustomerId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            _customers.Remove(customer);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE CustomerID = @id", connection);
                cmd.Parameters.AddWithValue("@id", customer.CustomerId);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
