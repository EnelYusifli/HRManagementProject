using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Department:IEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int EmployeeLimit { get; set; }
    public Company? Company { get; set; }
    public int CompanyId { get; set; }
    private static int id;
    public int currentEmployeeCount;

    public Department(string name, int employeeLimit, int companyId, string description)
    {
        Id = id++;
        Name = name;
        EmployeeLimit = employeeLimit;
        CompanyId = companyId;
        currentEmployeeCount = 0;
        Description = description;
    }
    public override string ToString()
    {
        return $"ID: {Id} /Name: {Name.ToUpper()} / Company: {Company.Name.ToUpper()} \n ";
    }
}
