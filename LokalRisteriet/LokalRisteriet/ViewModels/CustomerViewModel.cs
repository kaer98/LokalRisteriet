using LokalRisteriet.Models;
using LokalRisteriet.Persistence;
using System.Collections.Generic;

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
        public List<Customer> GetAllCustomers() => _customerRepo.GetAllCustomers();

        public int AddCustomer(Customer customer) => _customerRepo.AddCustomer(customer);

        public void UpdateCustomer(Customer customer)=> _customerRepo.UpdateCustomer(customer);

        public void DeleteCustomer(Customer customer) => _customerRepo.DeleteCustomer(customer);

        public Customer GetCustomerByEmail(string mail) => _customerRepo.GetAllCustomers().Find(customer => customer.CustomerEmail == mail);

        public int GetNextID() => nextid;

    }
}
