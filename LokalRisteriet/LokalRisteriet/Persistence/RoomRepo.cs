using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;

namespace LokalRisteriet.Persistence
{
    public class RoomRepo
    {
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        private List<Room> _rooms;

        public RoomRepo()
        {
            _rooms = new List<Room>();
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

        public List<Room> GetAllRooms()
        {
            return _rooms;
        }

        public void DeleteRoom(Room room)
        {
            _rooms.Remove(room);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Room WHERE RoomName = @name", connection);
                cmd.Parameters.AddWithValue("@name", room.RoomName);
                cmd.ExecuteNonQuery();
            }
        }

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
