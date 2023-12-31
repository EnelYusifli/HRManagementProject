using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;

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
            throw new LessThanMinimumException($"Id cannot be negative");        Department? dbDepartment = HRDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department cannot be found");
        if (dbDepartment.IsActive == false && dbDepartment is not null)
            throw new IsDeactiveException($"{dbDepartment.Name.ToUpper()} is deactive");
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
        if (department.IsActive == false && department is not null)
            throw new IsDeactiveException($"{department.Name.ToUpper()} is deactive");
        Employee? employee =
            HRDbContext.Employees.Find(e => e.Id == employeeId);
        if (employee is null)
            throw new NotFoundException("Employee cannot be found");
        if (employee.Company == department.Company)
        {
            employee.Salary = newSalary;
            Console.WriteLine("Salary is updated");
        }
        else 
            throw new NotFoundException($"Employee cannot be found in Company ");
    }
    public void UpdatePosition(int employeeId,string? newPosition)
    {
        if (employeeId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");
        if (String.IsNullOrEmpty(newPosition))
            throw new ArgumentNullException();
        Employee? employee =
           HRDbContext.Employees.Find(e => e.Id == employeeId);
        if (employee is null)
            throw new NotFoundException("Employee cannot be found");
        if (newPosition == employee.Position)
            throw new AlreadyExistException($"Employee's position is already {newPosition.ToUpper()}");
        employee.Position=newPosition;
        Console.WriteLine("Position of employee is updated successfully");
    }


    public void DeleteEmployee(int employeeId)
    {
        if (employeeId < 0)
            throw new LessThanMinimumException($"Id cannot be negative");
        Employee? dbEmployee =
            HRDbContext.Employees.Find(e => e.Id == employeeId);
        if (dbEmployee is not null)
        {
                dbEmployee.Department.currentEmployeeCount--;
                HRDbContext.Employees.Remove(dbEmployee);
                Console.WriteLine($"Employee has been successfully deleted");
        }
        else
        {
            throw new NotFoundException($"Employee cannot be found");
        }
    }
}
