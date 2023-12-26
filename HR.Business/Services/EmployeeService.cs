﻿using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Data.Common;

namespace HR.Business.Services;

public class EmployeeService : IEmployeeService
{
    public IDepartmentService departmentService { get; }
    public IEmployeeService employeeService { get; }
    public EmployeeService()
    {
        departmentService = new DepartmentService();
    }
  
   


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
        
        Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department cannot be found");
        if (dbDepartment.currentEmployeeCount == dbDepartment.EmployeeLimit)
            throw new AlreadyFullException($"{dbDepartment.Name.ToUpper()} Department is already full");
        Employee employee = new Employee(employeeName, employeeSurname, departmentId, employeeSalary, position);
        employee.Department=dbDepartment;
        employee.Company=dbDepartment.Company;
        HRDbContext.Employees.Add(employee);
        dbDepartment.currentEmployeeCount++;
        Console.WriteLine($"The new employee- {employee.Name.ToUpper()} has been successfully created \n");

    }

    public void UpdateSalary( int newSalary, int employeeId, int departmentId)
    {

        if (departmentId < 0)
            throw new LessThanMinimumException($"Id cannot be negative"); 
        if (employeeId < 0)
            throw new LessThanMinimumException($"Id cannot be negative"); 
        if (newSalary <= 0)
            throw new LessThanMinimumException($"Salary cannot be 0 or negative"); 
        Department? department = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (department is null)
            throw new NotFoundException("Department cannot be found");
        Employee? employee =
            HRDbContext.Employees.Find(e => e.Id == employeeId);
        if (employee is null)
            throw new NotFoundException("Employee cannot be found");
        if (employee.Company == department.Company)
        {
            employee.Salary = newSalary;
        }
        else 
            throw new NotFoundException($"Employee {employee.Name} cannot be found in Company ");
    }
    public void DeleteEmployee(int employeeId)
    {
        if (employeeId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");

        Employee? dbEmployee =
            HRDbContext.Employees.Find(e => e.Id == employeeId);

        if (dbEmployee is not null)
        {
                HRDbContext.Employees.Remove(dbEmployee);
                Console.WriteLine($"Employee has been successfully deleted");
            
        }
        else
        {
            throw new NotFoundException($"Employee cannot be found");
        }
    }
}
