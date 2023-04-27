using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;
using Microsoft.VisualBasic.CompilerServices;
using SkiaSharp;

namespace LokalRisteriet.ViewModels;

public class BookingInfoViewVM
{
    
    //test liste for BookingInfoView Employee
    

    public TaskRepo taskRepository = new();
    public AddOnRepo addOnRepository = new();
    public ObservableCollection<Task> Tasks { get; set; }
    public ObservableCollection<AddOn> AddOns { get; set; } = new();
    public int Id { get; set; }
    public BookingInfoViewVM()
    {
     
    }

    public void AddListBoxes(int id)
    {
        AddTasks(id);
        AddAddOns(id);


    }

    private void AddTasks(int id)
    {
        Tasks = new();
        foreach (Task task in taskRepository.GetAllTasks())
        {
            if (task.TaskBookingID == id)
            {
                Tasks.Add(task);
            }
        }
    }

    private void AddAddOns(int id)
    {
        AddOns = new();
        foreach (AddOn addOn in addOnRepository.GetAllAddOns())
        {
            if (addOn.AddOnBookingID == id)
            {
                AddOns.Add(addOn);
            }
        }
    }

}