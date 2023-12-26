using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? departmentName, string departmentDescription, string company,int employeeLimit);
    void TransferEmployeeToDepartment(int departmentName,int employeeId);
    void UpdateDepartment(string? newDepartmentName, int newEmployeeLimit, int departmentId);
    void GetDepartmentEmployees(int departmentId);
    public void DeleteDepartment(int departmentId);
    Department? FindDepartmentByName(string? departmentName);
}
