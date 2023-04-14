using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalRisteriet.Models
{
    internal class AddOn
    {
        public string Name { get; private set; }
        public double Price { get; private set; }

        public AddOn(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
