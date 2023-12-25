using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? departmentName, string departmentDescription, string company,int employeeLimit);
    void AddEmployeeToDepartment(int departmentName,int employeeId);
    void UpdateDepartment(string? oldDepartmentName,string? newDepartmentName,string? companyName, int newEmployeeLimit);
    void GetDepartmentEmployees(int departmentId);
    Department? FindDepartmentByName(string? departmentName);
    Department? FindDepartmentById(int departmentId);
}
