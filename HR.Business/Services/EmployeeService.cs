using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Data.Common;

namespace HR.Business.Services;

public class EmployeeService : IEmployeeService
{
    public void Create(string employeeName, string employeeSurname, int employeeSalary,string companyName, string departmentName)
    {
        if (String.IsNullOrEmpty(employeeName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(departmentName))
            throw new ArgumentNullException();
        if(employeeSalary <= 0) 
            throw new ArgumentOutOfRangeException();
        Company? dbCompany =
       HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null || companyName.ToLower() != dbCompany.Name.ToLower())
            throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
        Department? dbDepartment =
       HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        if (dbDepartment is null || companyName.ToLower() != dbDepartment.Company.ToLower())
            throw new NotFoundException($"{departmentName.ToUpper()} department cannot be found in {companyName.ToUpper()} Company");
        if(dbDepartment.currentEmployeeCount==dbDepartment.EmployeeLimit)
            throw new AlreadyFullException($"{departmentName.ToUpper()} Department is already full");
        Employee employee = new Employee(employeeName, employeeSurname, companyName, departmentName, employeeSalary);
        employee.Department=dbDepartment;
        employee.Company=dbCompany;
        HRDbContext.Employees.Add(employee);
        dbDepartment.currentEmployeeCount++;
        Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully created \n");

    }
}
