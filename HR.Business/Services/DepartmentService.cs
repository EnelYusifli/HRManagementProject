using HR.Business.Interfaces;
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
        department.Company= dbCompany;
        HRDbContext.Departments.Add(department);
        Console.WriteLine($"The new department- {department.Name.ToUpper()} has been successfully created \n");

    }
    //add employee dediyiniz methodu bele adlandirmisam ki daha basa dusulen olsun
    public void TransferEmployeeToDepartment(int departmentId,int employeeId)
    {
        {
            if (departmentId < 0)
                throw new LessThanMinimumException($"Id cannot be negative");
            if (employeeId < 0)
                throw new ArgumentOutOfRangeException();

            Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
            if (dbDepartment is null)
                throw new NotFoundException($"Department cannot be found");

            Employee? dbEmployee = HRDbContext.Employees.Find(e => e.Id == employeeId);
            if (dbEmployee is null)
                throw new NotFoundException($"Employee cannot be found");
            Department? employeeDepartment=dbDepartment;
            foreach (var department in HRDbContext.Departments)
            {
                if (department.Id == dbEmployee.DepartmentId)
                {
                    employeeDepartment = department;
                    break;
                }
            }
            if (dbEmployee.DepartmentId != dbDepartment.Id && employeeDepartment.CompanyName == dbDepartment.CompanyName)
            {
                dbEmployee.Department = dbDepartment;
                dbEmployee.DepartmentId = dbDepartment.Id;
                dbDepartment.currentEmployeeCount++;
                Console.WriteLine($"The new employee - {dbEmployee.Name.ToUpper()} has been successfully added \n");
            }
            else if (dbEmployee.DepartmentId == dbDepartment.Id && employeeDepartment.CompanyName == dbDepartment.CompanyName)
            {
                throw new AlreadyExistException($"Employee {dbEmployee.Name.ToUpper()} is already in {dbDepartment.Name.ToUpper()} Department");
            }
            else throw new NotFoundException($"Employee cannot be found in company");
        }
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
                    Console.WriteLine($"Employees:\n Id: {employee.Id}\n Full Name: {employee.Name.ToUpper()} {employee.Surname.ToUpper()}\n Position: {employee.Position.ToUpper()} \n Salary: {employee.Salary}\n \n");
                }
            }
        }
        else throw new NotFoundException($"Department cannot be found");

    }
    public void UpdateDepartment( string? newDepartmentName, int newEmployeeLimit ,int departmentId)
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
        if (newEmployeeLimit < 4 || newEmployeeLimit< dbDepartment.currentEmployeeCount)
            throw new LessThanMinimumException($"Employee count cannot be less than 3 or current employee count");
       dbDepartment.Name = newDepartmentName;
        dbDepartment.EmployeeLimit=newEmployeeLimit;
        Console.WriteLine($"{newDepartmentName.ToUpper()} Department has been successfully updated");

B
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
