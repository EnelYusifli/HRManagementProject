using HR.Core.Interfaces;
using System.Text.RegularExpressions;

namespace HR.Core.Entities;

public class Employee:IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Department { get; set; }
    public string Company { get; set; }
    public int Salary {  get; set; }
    public bool IsDeleted { get; set; }
    private static int id;

    public Employee(string name, string surname,string company, string department, int salary)
    {
        Id = id++;
        Name = name;
        Surname = surname;
        Department = department;
        Company= company;
        Salary = salary;
        IsDeleted = false;
    }
}
