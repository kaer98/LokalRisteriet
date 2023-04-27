﻿using System;
using System.Collections.Generic;
using System.Configuration;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using Task = LokalRisteriet.Models.Task;

namespace LokalRisteriet.Persistence
{
    public class TaskRepo
    {

        //private string _connectionString = ConfigurationManager.ConnectionStrings["Production"].ConnectionString;
        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        private List<Task> _tasks;
        private int nextID = 0;

        public TaskRepo()
        {
            _tasks = new List<Task>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Task') + IDENT_INCR('Task') AS NextID", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Task", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["TaskId"].ToString());
                        string name = dr["TaskName"].ToString();
                        int bookingID = 0;
                        string employee = null;
                        if (dr["TaskEmployee"] != DBNull.Value)
                        {
                            employee = dr["TaskEmployee"].ToString();
                        }
                        if (dr["TaskBookingID"] != DBNull.Value)
                        {
                            bookingID = int.Parse(dr["TaskBookingID"].ToString());
                        }
                      
                        Task task = new Task(name);
                        if (employee != null)
                        {
                            task.TaskIsDone = true;
                        }
                        task.Initials= employee;
                        task.TaskID = id;
                        task.TaskBookingID = bookingID;
                        _tasks.Add(task);
                    }
                }
            }
        }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if ( task.TaskBookingID == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Task(TaskName) VALUES(@TaskName)", connection);
                    cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                    cmd.ExecuteNonQuery();
                }
                else if (task.TaskBookingID != 0) 
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Task(TaskName, TaskBookingID) Values(@TaskName, @TaskBookingID)", connection);
                    cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                    cmd.Parameters.AddWithValue("@TaskBookingID", task.TaskBookingID);
                    cmd.ExecuteNonQuery();
                }
                
            }
        }

        public void DeleteTask(Task task)
        {
            _tasks.Remove(task);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Task WHERE TaskId = @TaskId", connection);
                cmd.Parameters.AddWithValue("@TaskId", task.TaskID);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTask(Task task)
        {
            int i =_tasks.FindIndex(t => t.TaskID == task.TaskID);
            _tasks[i] = task;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
               
                    SqlCommand cmd = new SqlCommand("UPDATE Task SET TaskName = @TaskName, TaskEmployee = NULL WHERE TaskId = @TaskId", connection);
                    cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                    cmd.Parameters.AddWithValue("@TaskId", task.TaskID);
                    cmd.ExecuteNonQuery();
            }

        }

        public void LoadTasksForBooking()
        {
   
        }

        public List<Task> GetAllTasks() => _tasks;

        public void AddTasksFromBooking(Booking booking)
        {
            foreach (Task t in booking.BookingTasks)
            {
                t.TaskBookingID = booking.BookingID;
                AddTask(t);
            }
        }

    }
}
