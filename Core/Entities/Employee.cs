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
    public string Position {  get; set; }
    public bool IsDeleted { get; set; }
    private static int id;

    public Employee(string name, string surname,Company company, Department department, int salary, string position)
    {
        Id = id++;
        Name = name;
        Surname = surname;
        Department = department;
        Company = company;
        Salary = salary;
        Position = position;
        IsDeleted = false;
    }
}
