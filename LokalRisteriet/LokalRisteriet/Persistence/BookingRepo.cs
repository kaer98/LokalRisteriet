using System;
using System.Collections.Generic;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace LokalRisteriet.Persistence
{
    public class BookingRepo 
    {
        private List<Booking> _bookings;
        private int nextID = 0;

        // private string _connectionString = ConfigurationManager.ConnectionStrings["Production"].ConnectionString;
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";

        public BookingRepo()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Booking') + IDENT_INCR('Booking') AS NextID", connection);
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["NextID"].ToString());
                        if (id > nextID)
                        {
                            nextID = id;
                        }
                    }
                }
            }
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
                        for (int i = 1; i <= 2; i++)
                        {
                            Room room = new Room(dr[$"BookingRoom{i}"].ToString(), int.Parse((string)dr["RoomCapacity"].ToString()));
                            rooms.Add(room);
                        }

                        List<Employee> employees = new List<Employee>();
                        for (int i = 1; i <= 4; i++)
                        {
                            Employee employee = new Employee(dr["EmployeeName"].ToString(), bool.Parse(dr["EmployeeAdult"].ToString()));
                            employee.EmployeeID = int.Parse(dr[$"BookingEmployee{i}"].ToString());
                            employees.Add(employee);
                        }

                        List<AddOn> addOns = new List<AddOn>();

                        DateTime bookingStart = DateTime.Parse(dr["BookingStart"].ToString());
                        DateTime bookingEnd = DateTime.Parse(dr["BookingEnd"].ToString());
                        TimeSpan bookingDuration = TimeSpan.Parse(dr["BookingDuration"].ToString());
                        int bookingCustomerID = int.Parse(dr["BookingCustomerID"].ToString());
                        int bookingAmountOfGuests = int.Parse(dr["BookingAmountOfGuests"].ToString());
                        double bookingPrice = double.Parse(dr["BookingPrice"].ToString());
                        bool bookingReserved = bool.Parse(dr["BookingReserved"].ToString());
                        string bookingNote = dr["BookingNote"].ToString();
                        Booking booking = new Booking(bookingtype, bookingNote, rooms, bookingStart, bookingEnd, bookingAmountOfGuests, bookingReserved);
                        booking.BookingCustomerID = bookingCustomerID;
                        booking.BookingPrice= bookingPrice;
                        booking.BookingEmployees = employees;
                        booking.BookingAddOns = addOns;
                        booking.BookingDuration= bookingDuration;
                        booking.BookingID= bookingID;

                        _bookings.Add(booking);
                    }
                }
            }

        }

        public List<Booking> GetAllBookings() => _bookings;

        public Booking AddBooking(Booking booking)
        {
            while (booking.BookingRooms.Count < 2)
            {
                booking.BookingRooms.Add(null);
            }

            while (booking.BookingEmployees.Count < 4)
            {
                booking.BookingEmployees.Add(null);
            }
            _bookings.Add(booking);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Booking(BookingType, BookingRoom1, BookingRoom2, BookingEmployee1, BookingEmployee2, BookingEmployee3, BookingEmployee4, BookingStart, BookingEnd, BookingDuration, BookingCustomerID, BookingAmountOfGuests, BookingPrice, BookingReserved, BookingNote) VALUES(@BookingType, @BookingRoom1, @BookingRoom2, @BookingEmployee1, @BookingEmployee2, @BookingEmployee3, @BookingEmployee4, @BookingStart, @BookingEnd, @BookingDuration, @BookingCustomerID, @BookingAmountOfGuests, @BookingPrice, @BookingReserved, @BookingNote)", connection);
                cmd.Parameters.AddWithValue("@BookingType", booking.BookingType);

                for (int i = 1; i <= 2; i++)
                {
                    if (booking.BookingRooms[i - 1] is null)
                    {
                        cmd.Parameters.AddWithValue($"@BookingRoom{i}", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue($"@BookingRoom{i}", booking.BookingRooms[i - 1].RoomName);
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    if (booking.BookingEmployees[i - 1] is null)
                    {
                        cmd.Parameters.AddWithValue($"@BookingEmployee{i}", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue($"@BookingEmployee{i}", booking.BookingEmployees[i - 1].EmployeeID);
                    }
                }

                cmd.Parameters.AddWithValue("@BookingStart", booking.BookingStart);
                cmd.Parameters.AddWithValue("@BookingEnd", booking.BookingEnd);
                cmd.Parameters.AddWithValue("@BookingDuration", booking.BookingDuration);
                if (booking.BookingCustomerID == 0)
                {
                    cmd.Parameters.AddWithValue("@BookingCustomerID", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingCustomerID", booking.BookingCustomerID);
                }
                cmd.Parameters.AddWithValue("@BookingAmountOfGuests", booking.BookingAmountOfGuests);
                cmd.Parameters.AddWithValue("@BookingPrice", booking.BookingPrice);
                cmd.Parameters.AddWithValue("@BookingReserved", booking.BookingReserved);
                cmd.Parameters.AddWithValue("@BookingNote", booking.BookingNote);
                cmd.ExecuteNonQuery();
            }
            booking.BookingID = nextID;
            return booking;
        }

        public void UpdateBooking(Booking booking)
        {
            int i =_bookings.FindIndex(b => b.BookingID == booking.BookingID);
            _bookings[i] = booking;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Booking SET BookingType=@BookingType, BookingRoom1=@BookingRoom1, BookingRoom2=@BookingRoom2, BookingEmployee1=@BookingEmployee1, BookingEmployee2=@BookingEmployee2, BookingEmployee3=@BookingEmployee3, BookingEmployee4=@BookingEmployee4, BookingStart=@BookingStart, BookingEnd=@BookingEnd, BookingDuration=@BookingDuration, BookingCustomerID=@BookingCustomerID, BookingAmountOfGuests=@BookingAmountOfGuests, BookingPrice=@BookingPrice, BookingReserved=@BookingReserved, BookingNote=@BookingNote WHERE BookingID=@BookingID", connection);
                cmd.Parameters.AddWithValue("@BookingID", booking.BookingID);
                cmd.Parameters.AddWithValue("@BookingType", booking.BookingType);
                if (booking.BookingRooms[0] == null)
                {
                    cmd.Parameters.AddWithValue("@BookingRoom1", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingRoom1", booking.BookingRooms[0].RoomName);
                }

                if (booking.BookingRooms[1] == null)
                {
                    cmd.Parameters.AddWithValue("@BookingRoom2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingRoom2", booking.BookingRooms[1].RoomName);
                }

                if (booking.BookingEmployees[0] == null)
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee1", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee1", booking.BookingEmployees[0].EmployeeID);
                }

                if (booking.BookingEmployees[1] == null)
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee2", booking.BookingEmployees[1].EmployeeID);
                }


                if (booking.BookingEmployees[2] == null)
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee3", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee3", booking.BookingEmployees[2].EmployeeID);
                }

                if (booking.BookingEmployees[3] == null)
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee4", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingEmployee4", booking.BookingEmployees[3].EmployeeID);
                }

                cmd.Parameters.AddWithValue("@BookingStart", booking.BookingStart);
                cmd.Parameters.AddWithValue("@BookingEnd", booking.BookingEnd);
                cmd.Parameters.AddWithValue("@BookingDuration", booking.BookingDuration);
                if (booking.BookingCustomerID == 0)
                {
                    cmd.Parameters.AddWithValue("@BookingCustomerID", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BookingCustomerID", booking.BookingCustomerID);
                }
                cmd.Parameters.AddWithValue("@BookingAmountOfGuests", booking.BookingAmountOfGuests);
                cmd.Parameters.AddWithValue("@BookingPrice", booking.BookingPrice);
                cmd.Parameters.AddWithValue("@BookingReserved", booking.BookingReserved);
                cmd.Parameters.AddWithValue("@BookingNote", booking.BookingNote);
                cmd.ExecuteNonQuery();
            }
            
        }

        public void DeleteBookingByID(int bookingID)
        {
            Booking booking = _bookings.Find(b => b.BookingID == bookingID);
            _bookings.Remove(booking);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Booking WHERE BookingID=@BookingID", connection);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBooking(Booking booking)
        {
            _bookings.Remove(booking);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Booking WHERE BookingID=@BookingID", connection);
                cmd.Parameters.AddWithValue("@BookingID", booking.BookingID);
                cmd.ExecuteNonQuery();
            }
        }


    }

}
