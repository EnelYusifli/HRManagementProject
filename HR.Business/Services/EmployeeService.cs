using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Data.Common;

namespace HR.Business.Services;

public class EmployeeService : IEmployeeService
{
    public IDepartmentService departmentService { get; }
    public ICompanyService companyService { get; }
    public EmployeeService()
    {
        departmentService = new DepartmentService();
        companyService= new CompanyService();
    }
  
   

    //department id yerine company name ve department name etdim, cunki id'ler yadimizda qalmaya biler.
    public void Create(string? employeeName, string? employeeSurname, int employeeSalary, int departmentId,string? position)
    {
        if (String.IsNullOrEmpty(employeeName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(employeeSurname))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(position))
            throw new ArgumentNullException();
            
        if(employeeSalary <= 0) 
            throw new LessThanMinimumException($"Salary cannot be 0 or negative");
        if(departmentId < 0) 
            throw new LessThanMinimumException($"Id cannot be negative");
        
        Department? dbDepartment = departmentService.FindDepartmentById(departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department cannot be found");
        if (dbDepartment.currentEmployeeCount == dbDepartment.EmployeeLimit)
            throw new AlreadyFullException($"{dbDepartment.Name.ToUpper()} Department is already full");
        Employee employee = new Employee(employeeName, employeeSurname, departmentId, employeeSalary, position);
        employee.Department=dbDepartment;
        HRDbContext.Employees.Add(employee);
        dbDepartment.currentEmployeeCount++;
        Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully created \n");

    }

    public void UpdateSalary(int employeeId, string? departmentName, string? companyName, int newSalary)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(departmentName))
            throw new ArgumentNullException();
        if (newSalary <= 0)
            throw new LessThanMinimumException($"Salary cannot be 0 or negative"); ;
        Company? dbCompany =
       HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null || companyName.ToLower() != dbCompany.Name.ToLower())
            throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
        Company? company = companyService.FindCompanyByName(companyName.ToLower());
        Department? department = departmentService.FindDepartmentByName(departmentName.ToLower());
    }

}
