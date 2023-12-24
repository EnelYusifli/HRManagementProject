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
  
   

    //department id yerine company name ve department name etdim, cunki id'ler yadimizda qalmaga biler.
    public void Create(string employeeName, string employeeSurname, int employeeSalary,string companyName, string departmentName)
    {
        if (String.IsNullOrEmpty(employeeName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(employeeSurname))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(departmentName))
            throw new ArgumentNullException();
        if(employeeSalary <= 0) 
            throw new ArgumentOutOfRangeException();
        Company? dbCompany =
       HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null || companyName.ToLower() != dbCompany.Name.ToLower())
            throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
        Company? company = companyService.FindCompanyByName(companyName.ToLower());
        Department? department = departmentService.FindDepartmentByName(departmentName.ToLower());
        if (department is null || company is null || companyName.ToLower() != department.Company.ToLower())
            throw new NotFoundException($"{departmentName.ToUpper()} department cannot be found in {companyName.ToUpper()} Company");
        if(department.currentEmployeeCount==department.EmployeeLimit)
            throw new AlreadyFullException($"{departmentName.ToUpper()} Department is already full");
        Employee employee = new Employee(employeeName, employeeSurname, company, department, employeeSalary);
        employee.Department=department;
        employee.Company=dbCompany;
        HRDbContext.Employees.Add(employee);
        department.currentEmployeeCount++;
        Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully created \n");

    }
}
