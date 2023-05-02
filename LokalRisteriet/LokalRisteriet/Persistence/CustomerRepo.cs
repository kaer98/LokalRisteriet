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
        // List of Customers
        private List<Customer> _customers;
        // Next available ID for a Customer
        private int nextID = 0;
        // Database connection string
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        public CustomerRepo()
        { 
            // Initialize the list of Customers
         _customers = new List<Customer>();
            // Get the next available ID for a Customer from the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Customer') + IDENT_INCR('Customer') AS NextID", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["NextID"].ToString());
                        if (id > nextID)
                        {
                            nextID = id;
                        }
                    }
                }
            }
            // Get all the Customers from the database and add them to the list
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
                        Customer customer = new Customer(name, customerPhoneNo, customerEmail);
                        customer.CustomerId = customerID;
                        _customers.Add(customer);
                    }
                 }
            }
        }
        // get all Customers
        public List<Customer> GetAllCustomers() => _customers;
        // Add a new Customer
        public int AddCustomer(Customer customer)
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
            return nextID++;
        }
        // Update a Customer
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
        // Delete a Customer
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
        // Get the next available ID for a Customer
        public int GetNextID()
        {
            return nextID;
        }
        // Get a Customer by ID
        public Customer GetCustomerById(int id)
        {
            return _customers.Find(x => x.CustomerId == id);
        }
    }
}
