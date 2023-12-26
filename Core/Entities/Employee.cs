using HR.Core.Interfaces;
using System.Text.RegularExpressions;

namespace HR.Core.Entities;

public class Employee:IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Department? Department { get; set; }
    public int DepartmentId { get; set; }
    public Company? Company { get; set; }
    public int Salary {  get; set; }
    public string Position {  get; set; }
    public bool IsDeleted { get; set; }
    private static int id;

    public Employee(string name, string surname, int departmentId, int salary, string position)
    {
        Id = id++;
        Name = name;
        Surname = surname;
        DepartmentId = departmentId;
        Salary = salary;
        Position = position;
        IsDeleted = false;
    }
    //public override string ToString()
    //{
    //    // return "Person: " + Name + " " + Age;
    //    return $"ID: {Id} / Full Name: {Name} {Surname} \n ";
    //}
}
