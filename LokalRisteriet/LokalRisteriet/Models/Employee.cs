﻿
namespace LokalRisteriet.Models
{
    public class Employee
    {
        public string Name { get; private set; }
        public double Price
        {
            // Return 400 if employee is over 18 else 200
            get => IsOverEightteen ? 400.0 : 200.0;
        }
        public bool IsOverEightteen { get; private set; }

        public Employee(string name, bool isOverEightteen)
        {
            Name = name;
            IsOverEightteen = isOverEightteen;
        }
    }
}
