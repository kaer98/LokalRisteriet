using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace LokalRisteriet.Persistence
{
    public class RoomRepo
    {
        // Database connection string
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";

        // List of rooms
        private List<Room> _rooms;

        // Next available ID for a room
        private int nextID = 0;


        public RoomRepo()
        {
            // Initialize the list of rooms
            _rooms = new List<Room>();

            // Get the next available ID for a room from the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Room') + IDENT_INCR('Room') AS NextID", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
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

            // Get all the rooms from the database and add them to the list
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Room", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string name = dr["RoomName"].ToString();
                        int capacity = int.Parse(dr["RoomCapacity"].ToString());
                        int roomID = int.Parse(dr["RoomID"].ToString());
                        Room room = new Room(name, capacity);
                        room.RoomID = roomID;
                        _rooms.Add(room);
                    }
                }
            }
        }

        // Add a room to the list and the database
        public void AddRoom(Room room)
        {
            _rooms.Add(room);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Room (RoomName, RoomCapacity) VALUES (@name, @capacity)", connection);
                cmd.Parameters.AddWithValue("@name", room.RoomName);
                cmd.Parameters.AddWithValue("@capacity", room.Capacity);
                cmd.ExecuteNonQuery();
            }
        }

        // Get all the rooms in the list
        public List<Room> GetAllRooms() => _rooms;

        // Remove a room from the list and the database
        public void DeleteRoom(Room room)
        {
            _rooms.Remove(room);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Room WHERE RoomID = @RoomID", connection);
                cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                cmd.ExecuteNonQuery();
            }
        }

        // Update a room in the list and the database
        public void UpdateRoom(Room room)
        {
            int i = _rooms.FindIndex(r => r.RoomID == room.RoomID);
            _rooms[i] = room;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Room SET RoomName = @name, RoomCapacity = @capacity WHERE RoomID = @roomID", connection);
                cmd.Parameters.AddWithValue("@name", room.RoomName);
                cmd.Parameters.AddWithValue("@capacity", room.Capacity);
                cmd.Parameters.AddWithValue("@roomID", room.RoomID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
