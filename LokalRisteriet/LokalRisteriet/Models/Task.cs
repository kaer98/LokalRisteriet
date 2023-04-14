using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalRisteriet.Models
{
    internal class Task
    {
        public string Name { get; private set; }
        public bool IsDone { get; private set; }


        public Task(string name, bool isDone)
        { 
            Name = name;
            IsDone = isDone;
        }
    }
}
