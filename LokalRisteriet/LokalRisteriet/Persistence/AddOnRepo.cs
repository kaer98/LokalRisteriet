﻿using System;
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

        public void addAddOn(AddOn addOn)
        {
            _addOns.Add(addOn);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO AddOn (AddOnName, AddOnPrice, AddOnBookingId) VALUES (@name, @price, @bookingID)", connection);
                cmd.Parameters.AddWithValue("@name", addOn.Name);
                cmd.Parameters.AddWithValue("@price", addOn.Price);
                cmd.Parameters.AddWithValue("@bookingID", addOn.AddOnBookingID);
                cmd.ExecuteNonQuery();
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
                cmd.Parameters.AddWithValue("@name", addOn.Name);
                cmd.Parameters.AddWithValue("@price", addOn.Price);
                cmd.Parameters.AddWithValue("@bookingID", addOn.AddOnBookingID);
                cmd.Parameters.AddWithValue("@id", addOn.AddOnID);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
