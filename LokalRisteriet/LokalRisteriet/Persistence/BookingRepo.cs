using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;



namespace LokalRisteriet.Persistence
{
    public class BookingRepo
    {
        private List<Booking> _bookings;
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";

        public BookingRepo()
        {
            _bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Booking INNER JOIN Room ON Booking.BookingRoom1=Room.RoomName AND Booking.BookingRoom2=Room.RoomName INNER JOIN Employee on Booking.BookingEmployee1=EmployeeID and Booking.BookingEmployee2=EmployeeID AND Booking.BookingEmployee3=EmployeeID And Booking.BookingEmployee4=EmployeeID INNER JOIN AddOn ON Booking.BookingID=AddOn.AddOnBookingID;", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int bookingID = int.Parse((string)dr["BookingID"].ToString());
                        string bookingtype = dr["BookingType"].ToString();
                        List<Room> rooms = new List<Room>();
                        Room room1 = new Room(dr["BookingRoom1"].ToString(), int.Parse((string)dr["RoomCapacity"].ToString()));
                        Room room2 = new Room(dr["BookingRoom2"].ToString(), int.Parse((string)dr["RoomCapacity"].ToString()));
                        rooms.Add(room1);
                        rooms.Add(room2);
                        List<Employee> employees = new List<Employee>();
                        Employee employee1 = new Employee(int.Parse(dr["EmployeeID"].ToString()),dr["EmployeeName"].ToString(), bool.Parse(dr["EmployeeAdult"].ToString()));
                        Employee employee2 = new Employee(int.Parse(dr["EmployeeID"].ToString()),dr["EmployeeName"].ToString(), bool.Parse(dr["EmployeeAdult"].ToString()));
                        Employee employee3 = new Employee(int.Parse(dr["EmployeeID"].ToString()),dr["EmployeeName"].ToString(), bool.Parse(dr["EmployeeAdult"].ToString()));
                        Employee employee4 = new Employee(int.Parse(dr["EmployeeID"].ToString()),dr["EmployeeName"].ToString(), bool.Parse(dr["EmployeeAdult"].ToString()));
                        employees.Add(employee1);
                        employees.Add(employee2);
                        employees.Add(employee3);
                        employees.Add(employee4);
                        List<AddOn> addOns = new List<AddOn>();

                        DateTime bookingStart = DateTime.Parse(dr["BookingStart"].ToString());
                        DateTime bookingEnd = DateTime.Parse(dr["BookingEnd"].ToString());
                        TimeSpan bookingDuration = TimeSpan.Parse(dr["BookingDuration"].ToString());
                        int bookingCustomerID = int.Parse(dr["BookingCustomerID"].ToString());
                        int bookingAmountOfGuests = int.Parse(dr["BookingAmountOfGuests"].ToString());
                        double bookingPrice = double.Parse(dr["BookingPrice"].ToString());
                        bool bookingReserved = bool.Parse(dr["BookingReserved"].ToString());
                        string bookingNote = dr["BookingNote"].ToString();
                        Booking booking = new Booking(bookingID, bookingtype, bookingNote, rooms, employees, addOns, bookingStart, bookingEnd, bookingDuration, bookingCustomerID, bookingAmountOfGuests, bookingPrice, bookingReserved);

                        _bookings.Add(booking);
                    }
                }
            }

        }

        public List<Booking> GetAllBookings()
        {
            
            return _bookings;
        }


    }

}
