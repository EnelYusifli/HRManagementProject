using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string? employeeName, string? employeeSurname, int employeeSalary, int departmentId,string? position);
    void UpdateSalary(int employeeId,string? departmentName,string? companyName,int newSalary);
    Employee? FindEmployeeById(int employeeId);
}
