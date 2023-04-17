using System.Collections.Generic;
using System.Configuration;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;

namespace LokalRisteriet.Persistence
{
    public class AddOnRepo
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["Production"].ConnectionString;
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
                        double price = double.Parse(dr["AddOnPrice"].ToString());
                        int s = 0;
                        AddOn addOn = new AddOn(name, price);
                        if (int.TryParse(dr["AddOnBookingID"].ToString() ?? "", out s))
                        {
                            int bookingID = s;
                            addOn.AddOnBookingID = bookingID;
                        }


                       
                        
                            addOn.AddOnID = id;
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
                if (addOn.AddOnID != 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO AddOn (AddOnName, AddOnPrice, AddOnBookingId) VALUES (@name, @price, @bookingID)", connection);
                    cmd.Parameters.AddWithValue("@name", addOn.AddOnName);
                    cmd.Parameters.AddWithValue("@price", addOn.Price);
                    cmd.Parameters.AddWithValue("@bookingID", addOn.AddOnBookingID);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO AddOn (AddOnName, AddOnPrice) VALUES (@name, @price)", connection);
                    cmd.Parameters.AddWithValue("@name", addOn.AddOnName);
                    cmd.Parameters.AddWithValue("@price", addOn.Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AddOn> GetAllAddOns()
        {
            return _addOns;
        }

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
                SqlCommand cmd = new SqlCommand("UPDATE AddOn SET AddOnName = @name, AddOnPrice = @price, AddOnBookingId = @bookingID WHERE AddOnId = @id", connection);
                cmd.Parameters.AddWithValue("@name", addOn.AddOnName);
                cmd.Parameters.AddWithValue("@price", addOn.Price);
                cmd.Parameters.AddWithValue("@bookingID", addOn.AddOnBookingID);
                cmd.Parameters.AddWithValue("@id", addOn.AddOnID);
                cmd.ExecuteNonQuery();
            }
        }

        public AddOn GetAddOnByID(int id)
        {
            return _addOns.Find(a => a.AddOnID == id);
        }

    }
}
