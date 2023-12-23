using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{

    public void Create(string? departmentName, string departmentDescription, int employeeLimit,string company)
    {
        if (String.IsNullOrEmpty(departmentName))
        throw new ArgumentNullException();
        if (String.IsNullOrEmpty(company))
        throw new ArgumentNullException();
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        if (dbDepartment is not null)
        throw new AlreadyExistException($"{dbDepartment} is already exist");
        if (employeeLimit < 4)
        throw new LessThanMinimumException($"The {departmentName} department should have at least 4 employees ");
        Department department = new(departmentName, employeeLimit, company);
        HRDbContext.Departments.Add(department);
        Console.WriteLine($"{department.Name} has been successfully created \n");

    }
    public void AddEmployeeToDepartment(string departmentName, string employeeName)
    {
        throw new NotImplementedException();
    }

    public void GetDepartmentEmployees(string departmentName)
    {
        throw new NotImplementedException();
    }

    public void UpdateDepartment(string oldDepartmentName, string newDepartmentName, int newEmployeeLimit)
    {
        throw new NotImplementedException();
    }
}
