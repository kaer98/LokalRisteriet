using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace LokalRisteriet.Persistence
{
    public class AddOnRepo
    {
        // Database connection string
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        //List of AddOns
        private List<AddOn> _addOns;
        //Next available ID for a AddOn
        public int nextID { get; set; }
    
        public AddOnRepo()
        {
            // Initialize the list of AddOns
            _addOns = new List<AddOn>();
            
           nextID = GetNextID();
            // Get all the AddOns from the database and add them to the list
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

        // Add a AddOn to the list and the database
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
            nextID = GetNextID();
        }
        // Get all the AddOns
        public List<AddOn> GetAllAddOns() => _addOns;

        //Delete a AddOn from the list and the database by ID
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

        private int GetNextID()
        {
            // Get the next available ID for a AddOn from the database
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
            return nextID;
        }

        // Delete a AddOn from the list and the database
        public void deleteAddOn(AddOn addOn)
        {
           
           _addOns.RemoveAll(a=>a.AddOnID==addOn.AddOnID);
           using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM AddOn WHERE AddOnId = @id", connection);
                cmd.Parameters.AddWithValue("@id", addOn.AddOnID);
                cmd.ExecuteNonQuery();
            }
            nextID = GetNextID();
        }
        // Update a AddOn in the list and the database
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
        //find a AddOn by ID
       // public AddOn GetAddOnByID(int id) => _addOns.Find(a => a.AddOnID == id);
       public AddOn GetAddOnByID(int id)
       {
           AddOn aa = null;
           foreach (AddOn a in _addOns)
           {
               if (a.AddOnID == id)
               {
                   aa= a;
               }
           }
           return aa;
       }
        public List<AddOn> GetAddOnsByBookingID(int id) {
            List<AddOn> addOns = new List<AddOn>();
            foreach(AddOn addOn in _addOns)
            {
                if (addOn.AddOnBookingID == id)
                {
                    addOns.Add(addOn);
                }
            }
            return addOns;
        }
    }
}
