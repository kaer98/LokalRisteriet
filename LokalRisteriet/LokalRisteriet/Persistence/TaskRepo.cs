﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using Microsoft.Data.SqlClient;
using Task = LokalRisteriet.Models.Task;

namespace LokalRisteriet.Persistence
{
    public class TaskRepo
    {

        private string _connectionString = "Server=10.56.8.36; database=P3_DB_2023_04; user id=P3_PROJECT_USER_04; password=OPENDB_04; TrustServerCertificate=True;";
        private List<Task> _tasks;

        public TaskRepo()
        {
            _tasks = new List<Task>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Task INNER JOIN Employee ON Employee.EmployeeID=Task.TaskEmployee", connection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = int.Parse(dr["TaskId"].ToString());
                        string name = dr["TaskName"].ToString();
                        int employeeID = int.Parse(dr["TaskEmployee"].ToString());
                        string employeeName = dr["EmployeeName"].ToString();
                        bool employeeAdult = bool.Parse(dr["EmployeeAdult"].ToString());
                        Task task = new Task(name);
                        if (employeeID != 0)
                        {
                            task.TaskIsDone = true;
                        }
                        
                        Employee employee = new Employee(employeeName,employeeAdult);
                        employee.EmployeeID = employeeID;
                        task.TaskEmployee = employee;
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Task(TaskName) VALUES(@TaskName)", connection);
                cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                cmd.ExecuteNonQuery();
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
                if (task.TaskEmployee == null)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Task SET TaskName = @TaskName, TaskEmployee = NULL WHERE TaskId = @TaskId", connection);
                    cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                    cmd.Parameters.AddWithValue("@TaskId", task.TaskID);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Task SET TaskName = @TaskName, TaskEmployee = @TaskEmployee WHERE TaskId = @TaskId", connection);
                    cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                    cmd.Parameters.AddWithValue("@TaskEmployee", task.TaskEmployee.EmployeeID);
                    cmd.Parameters.AddWithValue("@TaskId", task.TaskID);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<Task> GetAllTasks()
        {
            return _tasks;
        }


    }
}
