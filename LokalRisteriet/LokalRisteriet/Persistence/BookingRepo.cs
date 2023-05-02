using System;
using System.Collections.Generic;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Collections.ObjectModel;

namespace LokalRisteriet.Persistence
{
    public class BookingRepo 
    {
        
        // This is a private field of type List<Booking>, used to store a collection of bookings
        private List<Booking> _bookings;
        private int nextID = 0;

        // connection string for database
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";

        
        //The booking repository
        public BookingRepo()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Booking') + IDENT_INCR('Booking') AS NextID", connection);
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    
                    // A while loop is used to iterate through the result set.
                    while (dr.Read())
                    {
                        // The NextID value is parsed from the result set and stored in the id variable.
                        int id = int.Parse(dr["NextID"].ToString());
                        
                        // If the id value is greater than the current nextID value, it is set as the new nextID value.
                        if (id > nextID)
                        {
                            nextID = id;
                        }
                    }
                }
            }
            
            // A new List of Booking objects is created.
            _bookings = new List<Booking>();
            
            // Another using statement is used to create a new SqlConnection object to retrieve all booking records from the database.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                
                // The connection is opened.
                connection.Open();
                
                // A SqlCommand object is created with a SELECT statement to retrieve all records from the Booking table.
                SqlCommand cmd = new SqlCommand("SELECT * FROM Booking", connection);
                
                // Another SqlDataReader object is created to execute the query and read the result set.
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    
                    // Another while loop is used to iterate through the result set.
                    while (dr.Read())
                    {
                        
                        // Various fields are retrieved from the current record and parsed into the appropriate data types.
                        int bookingID = int.Parse((string)dr["BookingID"].ToString());
                        string bookingtype = dr["BookingType"].ToString();

                        // A List of Room objects is created to store the rooms associated with the current booking.
                        List<Room> rooms = new List<Room>();
                        
                        // A for loop is used to iterate through the two available rooms associated with a booking.
                        for (int i = 1; i <= 2; i++)
                        {
                            // If the BookingRoom field is not null, its value is parsed and stored in the id variable.

                            int id = 0;
                            
                            // If the BookingRoom field is not null, its value is parsed and stored in the id variable.
                            if (dr[$"BookingRoom{i}"] != DBNull.Value)
                            {
                                string s = dr[$"BookingRoom{i}"].ToString();
                                id = int.Parse(dr[$"BookingRoom{i}"].ToString());

                            }
                            
                            // A new Room object is created and its RoomID is set to the parsed id value. The Room object is then added to the rooms list.
                            Room room = new Room();
                            room.RoomID = id;
                                rooms.Add(room);
                        }

                        // A List of AddOn objects is created to store the add-ons associated with the current booking.
                        List<AddOn> addOns = new List<AddOn>();

                        
                        // Various fields are retrieved from the current record and parsed into the appropriate data types.
                        DateTime bookingStart = DateTime.Parse(dr["BookingStart"].ToString());
                        DateTime bookingEnd = DateTime.Parse(dr["BookingEnd"].ToString());
                        TimeSpan bookingDuration = TimeSpan.Parse(dr["BookingDuration"].ToString());
                        int bookingCustomerID = 0;
                        if (dr["BookingCustomerID"] != DBNull.Value)
                        {
                            bookingCustomerID = int.Parse(dr["BookingCustomerID"].ToString());
                        }
                        int bookingAmountOfGuests = int.Parse(dr["BookingAmountOfGuests"].ToString());
                        double bookingPrice = double.Parse(dr["BookingPrice"].ToString());
                        bool bookingReserved = bool.Parse(dr["BookingReserved"].ToString());
                        string bookingNote = dr["BookingNote"].ToString();
                        int bookingEmployeeAdult = int.Parse(dr["BookingEmployeeAdult"].ToString());
                        int bookingEmployeeChild = int.Parse(dr["BookingEmployeeChild"].ToString());
                        Double deposit = 0;
                        if (dr["BookingDeposit"] != DBNull.Value)
                        {
                            deposit = double.Parse(dr["BookingDeposit"].ToString());
                        }
                        Booking booking = new Booking(bookingtype, bookingNote, rooms, bookingStart, bookingEnd, bookingAmountOfGuests, bookingReserved);
                        booking.BookingCustomerID = bookingCustomerID;
                        booking.BookingPrice= bookingPrice;
                        booking.BookingAddOns = addOns;
                        booking.BookingDuration= bookingDuration;
                        booking.BookingID= bookingID;
                        booking.EmployeesChild = bookingEmployeeChild;
                        booking.EmployeesAdult = bookingEmployeeAdult;
                        booking.Deposit = deposit;

                        _bookings.Add(booking);
                    }
                }
            }

        }

        //This method allows us to get all bookings.
        //In this way we can add them to a List<> or ObservableCollection (and more!)
        public List<Booking> GetAllBookings() => _bookings;

        //The Add Booking Method:
        //Allows us to Add bookings to the database. 
        public Booking AddBooking(Booking booking)
        {
            while (booking.BookingRooms.Count < 2)
            {
                booking.BookingRooms.Add(null);
            }

            
            _bookings.Add(booking);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Booking(BookingType, BookingRoom1, BookingRoom2, BookingEmployeeAdult, BookingEmployeeChild, BookingStart, BookingEnd, BookingDuration, BookingCustomerID, BookingAmountOfGuests, BookingPrice, BookingReserved, BookingNote, BookingDeposit) VALUES(@BookingType, @BookingRoom1, @BookingRoom2, @BookingEmployeeAdult, @BookingEmployeeChild, @BookingStart, @BookingEnd, @BookingDuration, @BookingCustomerID, @BookingAmountOfGuests, @BookingPrice, @BookingReserved, @BookingNote, @BookingDeposit)", connection);
                cmd.Parameters.AddWithValue("@BookingType", booking.BookingType);

                for (int i = 1; i <= 2; i++)
                {
                    if (booking.BookingRooms[i - 1] is null)
                    {
                        cmd.Parameters.AddWithValue($"@BookingRoom{i}", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue($"@BookingRoom{i}", booking.BookingRooms[i - 1].RoomID);
                    }
                }

                
                cmd.Parameters.AddWithValue($"@BookingEmployeeAdult", booking.EmployeesAdult);            
                cmd.Parameters.AddWithValue($"@BookingEmployeeChild", booking.EmployeesChild);
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
                cmd.Parameters.AddWithValue("@BookingDeposit", booking.Deposit);
                cmd.ExecuteNonQuery();
            }
            booking.BookingID = nextID++;
            return booking;
        }

        //This allows us to update a booking in the database.
        public void UpdateBooking(Booking booking)
        {
            int i =_bookings.FindIndex(b => b.BookingID == booking.BookingID);
            _bookings[i] = booking;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Booking SET BookingType=@BookingType, BookingRoom1=@BookingRoom1, BookingRoom2=@BookingRoom2, BookingEmployeeAdult=@BookingEmployeeAdult, BookingEmployeeChild=@BookingEmployeeChild, BookingStart=@BookingStart, BookingEnd=@BookingEnd, BookingDuration=@BookingDuration, BookingCustomerID=@BookingCustomerID, BookingAmountOfGuests=@BookingAmountOfGuests, BookingPrice=@BookingPrice, BookingReserved=@BookingReserved, BookingNote=@BookingNote, BookingDeposit=@BookingDeposit WHERE BookingID=@BookingID", connection);
                cmd.Parameters.AddWithValue("@BookingID", booking.BookingID);
                cmd.Parameters.AddWithValue("@BookingType", booking.BookingType);

                for (int n = 1; n <= 2; n++)
                {
                    if (booking.BookingRooms[n - 1] is null)
                    {
                        cmd.Parameters.AddWithValue($"@BookingRoom{n}", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue($"@BookingRoom{n}", booking.BookingRooms[n - 1].RoomID);
                    }
                }

                cmd.Parameters.AddWithValue("@BookingEmployeeAdult", booking.EmployeesAdult);
                cmd.Parameters.AddWithValue("@BookingEmployeeChild", booking.EmployeesChild);
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
                cmd.Parameters.AddWithValue("@BookingDeposit", booking.Deposit);

                cmd.ExecuteNonQuery();
            }
            
        }
        // This method deletes a booking from the database by id.
        public void DeleteBookingByID(int bookingID)
        {
            Booking booking = _bookings.Find(b => b.BookingID == bookingID);
            DeleteBooking(booking);
        }
        // Deletes a booking from the database.
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
        // This method gets a booking from the database by date
        public ObservableCollection<Booking> GetBookingByDay(DateTime day) => new ObservableCollection<Booking>(_bookings.FindAll(b => b.BookingStart.Date == day.Date));

    }

}
