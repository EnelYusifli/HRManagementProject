using HR.Core.Interfaces;
using System.Text.RegularExpressions;

namespace HR.Core.Entities;

public class Employee:IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int DepartmentId { get; set; }
    public int Salary {  get; set; }
    public bool IsDeleted { get; set; }
    private static int id;

    public Employee(string name, string surname, int departmentId, int salary)
    {
        Id = id++;
        Name = name;
        Surname = surname;
        DepartmentId = departmentId;
        Salary = salary;
        IsDeleted = false;
    }
}
