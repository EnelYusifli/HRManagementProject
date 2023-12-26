﻿using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{
    public IDepartmentService departmentService { get; }
    public ICompanyService? companyService { get; }
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
        if (dbDepartment is not null && dbDepartment.CompanyName==company)
         throw new AlreadyExistException($"{dbDepartment.Name.ToUpper()} Department is already exist");
        if (employeeLimit < 4)
         throw new LessThanMinimumException($"The {departmentName.ToUpper()} department should have at least 4 employees ");
        Department department = new(departmentName, employeeLimit, company);
        HRDbContext.Departments.Add(department);
        Console.WriteLine($"The new department- {department.Name.ToUpper()} has been successfully created \n");

    }
    //add employee dediyiniz methodu bele adlandirmisam ki daha basa dusulen olsun
    public void TransferEmployeeToDepartment(int departmentId=-1,int employeeId=-1)
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

    public void GetDepartmentEmployees(int departmentId=-1)
    {
        if (departmentId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");
        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is not null)
        {
            foreach (var employee in HRDbContext.Employees)
            {
                if (employee.DepartmentId == dbDepartment.Id)
                {
                    Console.WriteLine($"Employees:\n Id: {employee.Id}\n Full Name: {employee.Name.ToUpper()} {employee.Surname.ToUpper()}\n Position: {employee.Position.ToUpper()}\n \n");
                }
            }
        }
        else throw new NotFoundException($"Department cannot be found");

    }
    public void UpdateDepartment( string? newDepartmentName, int newEmployeeLimit ,int departmentId= -1)
    {
        if (departmentId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");
        if (String.IsNullOrEmpty(newDepartmentName))
            throw new ArgumentNullException();
        Department? dbDepartment =
          HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department cannot be found");
        Department? dbNewDepartment =
          HRDbContext.Departments.Find(d => d.Name.ToLower() == newDepartmentName.ToLower());
        if (dbNewDepartment is not null && dbNewDepartment.Company==dbDepartment.Company)
            throw new AlreadyExistException($"{newDepartmentName.ToUpper()} department is already exist");
        if (newEmployeeLimit < 4)
            throw new AlreadyExistException($"Employee count should be more than 3");
       dbDepartment.Name = newDepartmentName;
        dbDepartment.EmployeeLimit=newEmployeeLimit;
        Console.WriteLine($"{newDepartmentName.ToUpper()} Department has been successfully updated");


    }
    public void DeleteDepartment(int departmentId)
    {
        if (departmentId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");

        Department? dbDepartment =
            HRDbContext.Departments.Find(d => d.Id == departmentId);

        if (dbDepartment is not null)
        {
            bool hasEmployee = HRDbContext.Employees.Any(e => e.DepartmentId == dbDepartment.Id);
            if (hasEmployee)
                throw new NotFoundException($"Cannot delete Department as it has associated employees");
            else
            {
                HRDbContext.Departments.Remove(dbDepartment);
                Console.WriteLine($"Department has been successfully deleted");
            }
        }
        else
        {
            throw new NotFoundException($"Department cannot be found");
        }
    }
}
