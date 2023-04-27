using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;

namespace LokalRisteriet.Persistence
{
    public class AddOnRepo
    {
        //private string _connectionString = ConfigurationManager.ConnectionStrings["Production"].ConnectionString;
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        private List<AddOn> _addOns;
        private int nextID = 0;
    
        public AddOnRepo()
        {
            _addOns = new List<AddOn>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('AddOn') + IDENT_INCR('AddOn') AS NextID", connection);
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
                        double price = double.Parse(dr["AddOnPrice"].ToString());
                        int amount = 0;
                       
                        int s = 0;
                        AddOn addOn = new AddOn(name, price);
                        if (int.TryParse(dr["AddOnBookingID"].ToString() ?? "", out s))
                        {
                            int bookingID = s;
                            addOn.AddOnBookingID = bookingID;
                        } 
                        if (int.TryParse(dr["AddOnAmount"].ToString() ?? "", out amount))
                        {
                            addOn.Amount = amount;
                        }
                        addOn.AddOnID = id;
                        addOn.Amount = amount;
                        _addOns.Add(addOn);
                    }
                }
            }
        }

        public void addAddOn(AddOn addOn)
        {
            _addOns.Add(addOn);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (addOn.AddOnBookingID != 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO AddOn (AddOnName, AddOnPrice, AddOnBookingId, AddOnAmount) VALUES (@name, @price, @bookingID, @AddOnAmount)", connection);
                    cmd.Parameters.AddWithValue("@name", addOn.AddOnName);
                    cmd.Parameters.AddWithValue("@price", addOn.Price);
                    cmd.Parameters.AddWithValue("@bookingID", addOn.AddOnBookingID);
                    cmd.Parameters.AddWithValue("@AddOnAmount", addOn.Amount);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO AddOn (AddOnName, AddOnPrice, AddOnAmount) VALUES (@name, @price, @AddOnAmount)", connection);
                    cmd.Parameters.AddWithValue("@name", addOn.AddOnName);
                    cmd.Parameters.AddWithValue("@price", addOn.Price);
                    cmd.Parameters.AddWithValue("@AddOnAmount", addOn.Amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AddOn> GetAllAddOns() => _addOns;


        public void DeleteAddOnByID(int id)
        {
            AddOn addOn = _addOns.Find(a => a.AddOnID == id);
            _addOns.Remove(addOn);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM AddOn WHERE AddOnId = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteAddOn(AddOn addOn)
        {
            _addOns.Remove(addOn);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM AddOn WHERE AddOnId = @id", connection);
                cmd.Parameters.AddWithValue("@id", addOn.AddOnID);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateAddOn(AddOn addOn)
        {
            int i =_addOns.FindIndex(a => a.AddOnID == addOn.AddOnID);
            _addOns[i] = addOn;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE AddOn SET AddOnName = @name, AddOnPrice = @price, AddOnBookingId = @bookingID, AddOnAmount= @AddOnAmount WHERE AddOnId = @id", connection);
                cmd.Parameters.AddWithValue("@name", addOn.AddOnName);
                cmd.Parameters.AddWithValue("@price", addOn.Price);
                cmd.Parameters.AddWithValue("@bookingID", addOn.AddOnBookingID);
                cmd.Parameters.AddWithValue("@id", addOn.AddOnID);
                cmd.Parameters.AddWithValue("@AddOnAmount", addOn.Amount);
                cmd.ExecuteNonQuery();
            }
        }

        public AddOn GetAddOnByID(int id) => _addOns.Find(a => a.AddOnID == id);
    }
}
