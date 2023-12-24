using HR.Core.Interfaces;
using System.Text.RegularExpressions;

namespace HR.Core.Entities;

public class Employee:IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Department? Department { get; set; }
    public Company? Company { get; set; }
    public int Salary {  get; set; }
    public bool IsDeleted { get; set; }
    private static int id;

    public Employee(string name, string surname,string companyName, string departmentName, int salary)
    {
        Id = id++;
        Name = name;
        Surname = surname;
        Department.Name = departmentName;
        Company.Name= companyName;
        Salary = salary;
        IsDeleted = false;
    }
}
