﻿using LokalRisteriet.Models;
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
        public CustomerViewModel()
        { 
            _customerRepo = new CustomerRepo();         
            nextid = _customerRepo.GetNextID();
        }
        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAllCustomers();
        }
        public int AddCustomer(Customer customer)
        {
           return _customerRepo.AddCustomer(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            _customerRepo.UpdateCustomer(customer);
        }
        public void DeleteCustomer(Customer customer)
        {
            _customerRepo.DeleteCustomer(customer);
        }

        public Customer GetCustomerByEmail(string mail)
        {
            bool found = false;
            Customer customer = null;
            foreach (Customer c in _customerRepo.GetAllCustomers())
            {
                if (c.CustomerEmail == mail)
                {
                    found = true;
                    customer = c;
                }
            }


            return customer;
        }
        public int GetNextID()
        {
            return nextid;
        }

    }
}
