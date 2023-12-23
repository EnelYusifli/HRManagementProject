namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? departmentName, string departmentDescription,int employeeLimit, string company);
    void AddEmployeeToDepartment(string departmentName,string employeeName);
    void UpdateDepartment(string oldDepartmentName,string newDepartmentName, int newEmployeeLimit);
    void GetDepartmentEmployees(string departmentName);

}
