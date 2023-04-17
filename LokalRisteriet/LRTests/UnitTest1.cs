using LokalRisteriet.Models;
using LokalRisteriet.ViewModels;
using Task = LokalRisteriet.Models.Task;

namespace LRTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddTask()
        {
            //Arrange
            Task task = new Task("Test");
            TaskViewModel taskViewModel = new TaskViewModel();

            //Act

            taskViewModel.AddTask(task);

            //Assert
            Assert.AreEqual(task, taskViewModel.GetTasks().Last());

        }
        [TestMethod]
        public void TestAddTaskToBooking()
        {
            //Arrange
            Task task = new Task("Test");
            List<Room> rooms = new List<Room>() { new Room("test", 10) };
            List<Employee> employees = new List<Employee>() { new Employee("test", true) };
            DateTime start = new DateTime(2023, 04, 09, 13, 30, 0);
            DateTime end = new DateTime(2023, 04, 09, 19, 0, 0);
            Booking booking = new Booking("konfirmation", "sørger selv for drikkelse", rooms, start, end, 1, false);
            BookingViewModel bookingViewModel = new BookingViewModel();
            TaskViewModel taskViewModel = new TaskViewModel();

            //Act
            
            booking = bookingViewModel.AddBooking(booking);
            booking.BookingAddTask(task);
            taskViewModel.AddTaskFromBooking(booking);

            //Assert
            Assert.AreEqual(task , taskViewModel.GetTasks().Last());

        }

        [TestMethod]
        public void TestDeleteTask()
        {
            //Arrange
            Task task = new Task("Test");
            TaskViewModel taskViewModel = new TaskViewModel();
            int count = taskViewModel.GetTasks().Count;
            //Act
            taskViewModel.AddTask(task);
            taskViewModel.DeleteTask(task);
            //Assert
            Assert.AreEqual(count, taskViewModel.GetTasks().Count);
        }

        [TestMethod]
        public void TestUpdateTask()
        {
            //Arrange
            Task task = new Task("Test");
            TaskViewModel taskViewModel = new TaskViewModel();
            taskViewModel.AddTask(task);
            //Act
            task.TaskName = "Test2";
            taskViewModel.UpdateTask(task);
            //Assert
            Assert.AreEqual(task, taskViewModel.GetTasks().Last());
        }

        [TestMethod]
        public void TestAddRoom()
        {
            //Arrange 
            Room room = new Room("Test", 10);
            RoomViewModel roomViewModel = new RoomViewModel();

            //Act
            roomViewModel.AddRoom(room);

            //Assert
            Assert.AreEqual(room, roomViewModel.GetRooms().Last());
            roomViewModel.DeleteRoom(room);

        }

        [TestMethod]
        public void TestRemoveRoom()
        {
            //Arrange
            Room room = new Room("Test", 10);
            RoomViewModel roomViewModel = new RoomViewModel();
            int count = roomViewModel.GetRooms().Count;

            //Act
            roomViewModel.AddRoom(room);
            roomViewModel.DeleteRoom(room);

            //Assert
            Assert.AreEqual(count, roomViewModel.GetRooms().Count);
            

        }

        [TestMethod]
        public void TestUpdateRoom()
        {
            //Arrange
            Room room = new Room("Test", 10);
            RoomViewModel roomViewModel = new RoomViewModel();
            roomViewModel.AddRoom(room);
            //Act
            room.RoomName = "Test2";
            roomViewModel.UpdateRoom(room);
            //Assert
            Assert.AreEqual(room, roomViewModel.GetRooms().Last());
        }

        [TestMethod]
        public void TestAddEmployee()
        {
            //Arrange
            Employee employee = new Employee("Test", true);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            //Act
            employeeViewModel.AddEmployee(employee);

            //Assert
            Assert.AreEqual(employee, employeeViewModel.GetEmployees().Last());

        }

        [TestMethod]
        public void TestRemoveEmployee()
        {
            //Arrange
            Employee employee = new Employee("Test", true);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            int count = employeeViewModel.GetEmployees().Count;
            //Act
            employeeViewModel.AddEmployee(employee);
            employeeViewModel.DeleteEmployee(employee);
            //Assert
            Assert.AreEqual(count, employeeViewModel.GetEmployees().Count);
        }

        [TestMethod]
        public void TestUpdateEmployee()
        {
            //Arrange
            Employee employee = new Employee("Test", true);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.AddEmployee(employee);
            //Act
            employee.EmployeeName = "Test2";
            employeeViewModel.UpdateEmployee(employee);
            //Assert
            Assert.AreEqual(employee, employeeViewModel.GetEmployees().Last());
        }

        [TestMethod]
        public void TestAddAddOn()
        {
            //Arrange
            AddOn addOn = new AddOn("Test", 10);
            AddOnViewModel addOnViewModel = new AddOnViewModel();
            
            //Act
            addOnViewModel.AddAddOn(addOn);

            //Assert
            Assert.AreEqual(addOn, addOnViewModel.GetAddOns().Last());
        }

        [TestMethod]
        public void TestRemoveAddOn()
        {
            //Arrange
            AddOn addOn = new AddOn("Test", 10);
            AddOnViewModel addOnViewModel = new AddOnViewModel();
            int count = addOnViewModel.GetAddOns().Count;
            //Act
            addOnViewModel.AddAddOn(addOn);
            addOnViewModel.DeleteAddOn(addOn);
            //Assert
            Assert.AreEqual(count, addOnViewModel.GetAddOns().Count);
        }

        [TestMethod]
        public void TestUpdateAddOn()
        {
            //Arrange
            AddOn addOn = new AddOn("Test", 10);
            AddOnViewModel addOnViewModel = new AddOnViewModel();
            addOnViewModel.AddAddOn(addOn);
            //Act
            addOn.AddOnName = "Test2";
            addOnViewModel.UpdateAddOn(addOn);
            //Assert
            Assert.AreEqual(addOn, addOnViewModel.GetAddOns().Last());
        }

        [TestMethod]

        public void TestAddBooking()
        {

            //Arrange
            List<Room> rooms = new List<Room>(){new Room("test",10)};
            List<Employee> employees = new List<Employee>() { new Employee("test", true) };
            DateTime start = new DateTime(2023, 04, 09, 13,30,0);
            DateTime end = new DateTime(2023, 04, 09, 19, 0, 0);
            Booking booking = new Booking("konfirmation","sørger selv for drikkelse", rooms,start,end,1,false);
            BookingViewModel bookingViewModel = new BookingViewModel();

            //Act
            bookingViewModel.AddBooking(booking);
            
            //Assert
            Assert.AreEqual(booking, bookingViewModel.GetBookings().Last());

        }

        [TestMethod]
        public void TestRemoveBooking()
        {
            //Arrange
            List<Room> rooms = new List<Room>() { new Room("test", 10) };
            List<Employee> employees = new List<Employee>() { new Employee("test", true) };
            DateTime start = new DateTime(2023, 04, 09, 13, 30, 0);
            DateTime end = new DateTime(2023, 04, 09, 19, 0, 0);
            Booking booking = new Booking("konfirmation", "sørger selv for drikkelse", rooms, start, end, 1, false);
            BookingViewModel bookingViewModel = new BookingViewModel();
            int count = bookingViewModel.GetBookings().Count;
            //Act
            bookingViewModel.AddBooking(booking);
            bookingViewModel.DeleteBooking(booking);
            //Assert
            Assert.AreEqual(count, bookingViewModel.GetBookings().Count);
        }

        [TestMethod]
        public void TestUpdateBooking()
        {
            //Arrange
            List<Room> rooms = new List<Room>() { new Room("test", 10) };
            List<Employee> employees = new List<Employee>() { new Employee("test", true) };
            DateTime start = new DateTime(2023, 04, 09, 13, 30, 0);
            DateTime end = new DateTime(2023, 04, 09, 19, 0, 0);
            Booking booking = new Booking("konfirmation", "sørger selv for drikkelse", rooms, start, end, 1, false);
            BookingViewModel bookingViewModel = new BookingViewModel();
            bookingViewModel.AddBooking(booking);
            //Act
            booking.BookingNote = "Ønsker ingenting";
            bookingViewModel.UpdateBooking(booking);
            //Assert
            Assert.AreEqual(booking, bookingViewModel.GetBookings().Last());
        }
        
    }
}