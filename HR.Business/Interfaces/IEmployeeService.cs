namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string employeeName, string employeeSurname, int employeeSalary,string companyName, string departmentName);
   
}
