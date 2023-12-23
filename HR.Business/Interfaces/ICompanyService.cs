namespace HR.Business.Interfaces;

public interface ICompanyService
{
    void Create(string? companyName, string companyDescription);
    void GetAllDepartments(string? companyName);

}
