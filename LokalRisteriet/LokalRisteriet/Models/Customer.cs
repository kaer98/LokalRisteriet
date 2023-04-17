using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalRisteriet.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerEmail { get; set; }

        public Customer(string customerName, string customerPhoneNo, string customerEmail)
        {
            CustomerName = customerName;
            CustomerPhoneNo = customerPhoneNo;
            CustomerEmail = customerEmail;
        }
    }
}
