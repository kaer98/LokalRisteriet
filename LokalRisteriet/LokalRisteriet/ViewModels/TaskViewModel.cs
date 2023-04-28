using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;
using Task = LokalRisteriet.Models.Task;

namespace LokalRisteriet.ViewModels
{
    public class TaskViewModel
    {
        private TaskRepo _taskRepo;

        public TaskViewModel()
        {
            _taskRepo = new TaskRepo();
        }

        public void AddTask(Task task)
        {
            _taskRepo.AddTask(task);
        }

        public void DeleteTask(Task task)
        {
            _taskRepo.DeleteTask(task);
        }

        public void DeleteTaskByID(int task)
        {
            _taskRepo.DeleteTaskById(task);
        }

        public List<Task> GetTasks()
        {
            return _taskRepo.GetAllTasks();
        }

        public void UpdateTask(Task task)
        {
            _taskRepo.UpdateTask(task);
        }

        public void AddTaskFromBooking(Booking booking)
        {
            _taskRepo.AddTasksFromBooking(booking);
        }
    }
}
