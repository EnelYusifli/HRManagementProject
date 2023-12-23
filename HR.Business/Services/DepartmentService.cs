using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Runtime.CompilerServices;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{
    
    public void Create(string? departmentName, string departmentDescription, string company, int employeeLimit)
    {
        if (String.IsNullOrEmpty(departmentName))
         throw new ArgumentNullException();
        if (String.IsNullOrEmpty(company))
         throw new ArgumentNullException();
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        if (dbDepartment is not null)
         throw new AlreadyExistException($"{dbDepartment.Name.ToUpper()} is already exist");
        if (employeeLimit < 4)
         throw new LessThanMinimumException($"The {departmentName.ToUpper()} department should have at least 4 employees ");
        Department department = new(departmentName, employeeLimit, company);
        HRDbContext.Departments.Add(department);
        Console.WriteLine($"The new department- {department.Name.ToUpper()} has been successfully created \n");

    }
    //burda employeeId de elave edirem cunki eyni adli bir nece nefer ola biler.
    public void AddEmployeeToDepartment(string departmentName,string companyName, string employeeName,int employeeId=-1)
    {
        if (String.IsNullOrEmpty(departmentName))
            throw new ArgumentNullException();
        if(employeeId < 0) 
            throw new ArgumentOutOfRangeException();
        int counter = 0;
        foreach (var employee in HRDbContext.Employees)
        {
            if (employee.Id == employeeId && employee.Company.ToLower()==companyName.ToLower() && employee.Department.ToLower() != departmentName.ToLower())
            {
                counter++;
                HRDbContext.Employees.Add(employee);
                Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully created \n");
                break;
            }
            if (employee.Id == employeeId && employee.Company.ToLower() == companyName.ToLower() && employee.Department.ToLower() == departmentName.ToLower())
            {
                counter++;
                throw new AlreadyExistException($"Employee {employeeName.ToUpper()} is already in {departmentName.ToUpper()} Department");
                
            }
        }
        if(counter==0)
            throw new NotFoundException($"Employee {employeeName.ToUpper()} cannot be found");


    }

    public void GetDepartmentEmployees(string departmentName)
    {
        throw new NotImplementedException();
    }

    public void UpdateDepartment(string oldDepartmentName, string newDepartmentName, int newEmployeeLimit)
    {
        throw new NotImplementedException();
    }

    public void ShowAll(string departmentName)
    {
        foreach (var employee in HRDbContext.Employees)
        {
            if (employee.Department.ToLower() == departmentName.ToLower())
                Console.WriteLine($"{employee.Id} {employee.Name}");
        }
    }
}
