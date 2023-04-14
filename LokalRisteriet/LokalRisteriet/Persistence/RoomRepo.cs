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
                        Room room = new Room(name, capacity);
                        _rooms.Add(room);
                    }
                }
            }
        }

    }
}
