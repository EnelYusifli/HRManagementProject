using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Department:IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public int EmployeeLimit { get; set; }
    public Company? Company { get; set; }
    public string CompanyName { get; set; }
    private static int id;
    public int currentEmployeeCount;

    public Department(string name, int employeeLimit, string company)
    {
        Id = id++;
        Name = name;
        EmployeeLimit = employeeLimit;
        CompanyName = company;
        currentEmployeeCount = 0;
    }
}
