using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;

namespace LokalRisteriet.Persistence
{
    public class AddOnRepo
    {
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        private List<AddOn> _addOns;

        public AddOnRepo()
        {
            _addOns = new List<AddOn>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM AddOn", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["AddOnId"].ToString());
                        string name = dr["AddOnName"].ToString();
                        int price = int.Parse(dr["AddOnPrice"].ToString());
                        int bookingID = int.Parse(dr["AddOnBookingId"].ToString());
                        AddOn addOn = new AddOn(id, name, price, bookingID);
                        _addOns.Add(addOn);
                    }
                }
            }
        }

    }
}
