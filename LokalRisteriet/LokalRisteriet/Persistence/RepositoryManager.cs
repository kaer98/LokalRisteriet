using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalRisteriet.Persistence
{
    public static class RepositoryManager
    {
        public static AddOnRepo AddOnRepo { get; set; } = new AddOnRepo();
        public static BookingRepo BookingRepo { get; set; } = new BookingRepo();
        public static CustomerRepo CustomerRepo { get; set; } = new CustomerRepo();
        public static EmployeeRepo EmployeeRepo { get; set; } = new EmployeeRepo();
        public static RoomRepo RoomRepo { get; set; } = new RoomRepo();
        public static TaskRepo TaskRepo { get; set; } = new TaskRepo();

    }
}
