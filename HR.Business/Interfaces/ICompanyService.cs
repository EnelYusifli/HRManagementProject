using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface ICompanyService
{
    void Create(string? companyName, string companyDescription);
    void GetAllDepartments(string? companyName);
    Company? FindCompanyByName(string? companyName);
    Company? FindCompanyById(int companyId);
    void DeleteCompany(string? companyName);

}
