using System.Collections.ObjectModel;
using LokalRisteriet.Models;

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