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
        Department? dbDepartment =
       HRDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower());
        if (dbDepartment is null || companyName != dbDepartment.Company)
            throw new NotFoundException($"{departmentName} department cannot be found in {companyName} Company");
    }
}
