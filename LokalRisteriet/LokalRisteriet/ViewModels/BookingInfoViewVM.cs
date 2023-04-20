using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using LokalRisteriet.Models;
using LokalRisteriet.Persistence;
using SkiaSharp;

namespace LokalRisteriet.ViewModels;

public class BookingInfoViewVM
{
    
    //test liste for BookingInfoView Employee


    public ObservableCollection<Employee> employeeList { get; set; } = new();
    public BookingInfoViewVM()
    {
     Employee amanda = new Employee("Amanda", true);
     Employee jacob = new Employee("Jacob", true);
    
     employeeList.Add((jacob));
     employeeList.Add((amanda));

     
    


    }



}