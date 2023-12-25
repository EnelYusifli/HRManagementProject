using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Runtime.CompilerServices;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{
     public ICompanyService companyService { get; }
    public DepartmentService()
    {
        companyService = new CompanyService();
    }

    public void Create(string? departmentName, string? departmentDescription, string? company, int employeeLimit)
    {
        if (String.IsNullOrEmpty(departmentName))
         throw new ArgumentNullException();
        if (String.IsNullOrEmpty(company))
         throw new ArgumentNullException();
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == company.ToLower());
        if (dbCompany is null)
            throw new NotFoundException($"{company.ToUpper()} Company cannot be found");
        if (dbDepartment is not null && dbDepartment.Company==company)
         throw new AlreadyExistException($"{dbDepartment.Name.ToUpper()} Department is already exist");
        if (employeeLimit < 4)
         throw new LessThanMinimumException($"The {departmentName.ToUpper()} department should have at least 4 employees ");
        Department department = new(departmentName, employeeLimit, company);
        HRDbContext.Departments.Add(department);
        Console.WriteLine($"The new department- {department.Name.ToUpper()} has been successfully created \n");

    }
    
    public void AddEmployeeToDepartment(int departmentId=-1,int employeeId=-1)
    {
        if (departmentId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");
        if(employeeId < 0) 
            throw new ArgumentOutOfRangeException();
        int counter = 0;
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department cannot be found");
        if (dbDepartment.currentEmployeeCount == dbDepartment.EmployeeLimit)
            throw new AlreadyFullException($"{dbDepartment.Name.ToUpper()} Department is already full");
        foreach (var employee in HRDbContext.Employees)
        {
            if (employee.Id == employeeId && employee.DepartmentId != dbDepartment.Id)
            {
                counter++;
                employee.Department = dbDepartment;
                dbDepartment.currentEmployeeCount++;
                Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully added \n");
                break;
            }
            else if (employee.Id == employeeId && employee.DepartmentId ==dbDepartment.Id)
            {
                counter++;
                throw new AlreadyExistException($"Employee {employee.Name} is already in {dbDepartment.Name.ToUpper()} Department");
                
            }
        }
        if(counter==0)
            throw new NotFoundException($"Employee cannot be found");
    }

    public void GetDepartmentEmployees(string? departmentName, string? companyName)
    {

        if (String.IsNullOrEmpty(departmentName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
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
                Console.WriteLine($"Employees:\n Id: {employee.Id}\n Full Name: {employee.Name.ToUpper()} {employee.Surname.ToUpper()}\n Position: {employee.Position.ToUpper()}");
        }

    }

    public void UpdateDepartment(string? oldDepartmentName, string? newDepartmentName,string? companyName, int newEmployeeLimit)
    {
        if (String.IsNullOrEmpty(oldDepartmentName))
            throw new ArgumentNullException(); 
        if (String.IsNullOrEmpty(newDepartmentName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
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
        dbOldDepartment.EmployeeLimit=newEmployeeLimit;
        Console.WriteLine($"{newDepartmentName} has been successfully updated");


    }
    public Department? FindDepartmentByName(string? departmentName)
    {
        if (String.IsNullOrEmpty(departmentName))
            throw new ArgumentNullException();
        return HRDbContext.Departments.Find(c => c.Name.ToLower() == departmentName.ToLower());
    }

    public Department? FindDepartmentById(int departmentId)
    {
        if (departmentId<0)
            throw new LessThanMinimumException("Id cannot be negative");
        return HRDbContext.Departments.Find(c => c.Id == departmentId);
    }
}
