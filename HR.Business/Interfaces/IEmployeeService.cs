using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string? employeeName, string? employeeSurname, int employeeSalary,string? position,string companyName, string? departmentName);
    void UpdateSalary(int employeeId,string? departmentName,string? companyName,int newSalary);
}
