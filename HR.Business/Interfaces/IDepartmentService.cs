using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? departmentName, string departmentDescription, int companyId,int employeeLimit);
    void TransferEmployeeToDepartment(int departmentName,int employeeId);
    void UpdateDepartment(string? newDepartmentName, string? newDescription, int newEmployeeLimit, int departmentId);
    void GetDepartmentEmployees(int departmentId);
    void DeleteDepartment(int departmentId);
}
