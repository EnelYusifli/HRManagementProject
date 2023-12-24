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
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        if(employeeId < 0) 
            throw new ArgumentOutOfRangeException();
        int counter = 0;
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        if (dbDepartment is null)
            throw new NotFoundException($"{departmentName.ToUpper()} cannot be found");
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null)
            throw new NotFoundException($"{companyName.ToUpper()} cannot be found");
        foreach (var employee in HRDbContext.Employees)
        {
            if (employee.Id == employeeId && employeeName.ToLower()==employee.Name.ToLower() && employee.Company==dbCompany && employee.Department != dbDepartment)
            {
                counter++;
                employee.Department = dbDepartment;
                HRDbContext.Employees.Add(employee);
                Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully added \n");
                break;
            }
            else if (employee.Id == employeeId  && employeeName == employee.Name && employee.Company == dbCompany && employee.Department ==dbDepartment)
            {
                counter++;
                throw new AlreadyExistException($"Employee {employeeName.ToUpper()} is already in {departmentName.ToUpper()} Department");
                
            }
        }
        if(counter==0)
            throw new NotFoundException($"Employee {employeeName.ToUpper()} cannot be found");


    }

    public void GetDepartmentEmployees(string departmentName, string companyName)
    {
        Department? dbDepartment =
           HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        if (dbDepartment is null)
            throw new NotFoundException($"{departmentName.ToUpper()} cannot be found");
        Company? dbCompany =
           HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null)
            throw new NotFoundException($"{companyName.ToUpper()} cannot be found");
        foreach (var employee in HRDbContext.Employees)
        {
            if (employee.Department == dbDepartment)
                Console.WriteLine($"Id: {employee.Id}\n Name: {employee.Name}");
        }

    }

    public void UpdateDepartment(string oldDepartmentName, string newDepartmentName,string companyName, int newEmployeeLimit)
    {
        Department? dbOldDepartment =
          HRDbContext.Departments.Find(d => d.Name.ToLower() == oldDepartmentName.ToLower());
        if (dbOldDepartment is null)
            throw new NotFoundException($"{oldDepartmentName.ToUpper()} cannot be found");
        Department? dbNewDepartment =
          HRDbContext.Departments.Find(d => d.Name.ToLower() == newDepartmentName.ToLower());
        if (dbNewDepartment is not null)
            throw new AlreadyExistException($"{newDepartmentName.ToUpper()} department is already exist");
        Company? dbCompany =
           HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null)
            throw new NotFoundException($"{companyName.ToUpper()} cannot be found");
        dbOldDepartment.Name = newDepartmentName;


    }

   
}
