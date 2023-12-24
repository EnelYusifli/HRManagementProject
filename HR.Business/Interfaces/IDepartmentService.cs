using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? departmentName, string departmentDescription, string company,int employeeLimit);
    void AddEmployeeToDepartment(string departmentName,string companyName,string employeeName, int employeeId);
    void UpdateDepartment(string oldDepartmentName,string newDepartmentName,string companyName, int newEmployeeLimit);
    void GetDepartmentEmployees(string departmentName,string companyName);
    Department? FindDepartmentByName(string? departmentName);
}
