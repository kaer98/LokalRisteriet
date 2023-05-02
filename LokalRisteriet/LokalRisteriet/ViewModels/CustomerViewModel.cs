using LokalRisteriet.Models;
using LokalRisteriet.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalRisteriet.ViewModels
{
    public class CustomerViewModel
    {
        private CustomerRepo _customerRepo;
        private int nextid;

        // Customer View Model Constructor.
        public CustomerViewModel()
        {
            _customerRepo = new CustomerRepo();
            nextid = _customerRepo.GetNextID();
        }

        // Method to get all customers
        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAllCustomers();
        }

        // Method to add a new customer and return their ID
        public int AddCustomer(Customer customer)
        {
            return _customerRepo.AddCustomer(customer);
        }

        // Method to update an existing customer
        public void UpdateCustomer(Customer customer)
        {
            _customerRepo.UpdateCustomer(customer);
        }

        // Method to delete a customer
        public void DeleteCustomer(Customer customer)
        {
            _customerRepo.DeleteCustomer(customer);
        }

        // Method to get a customer by email
        public Customer GetCustomerByEmail(string mail)
        {
            bool found = false;
            mail = mail.ToLower();
            Customer customer = null;

            foreach (Customer c in _customerRepo.GetAllCustomers())
            {
                if (c.CustomerEmail.ToLower() == mail)
                {
                    found = true;
                    customer = c;
                }
            }

            return customer;
        }

        // Method to get the next available customer ID
        public int GetNextID()
        {
            return nextid;
        }

        // Method to get a customer by ID
        public Customer GetCustomerById(int id)
        {
            return _customerRepo.GetCustomerById(id);
        }
    }
}
