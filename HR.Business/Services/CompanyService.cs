using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Xml.Linq;


namespace HR.Business.Services;

public class CompanyService : ICompanyService
{
    public void Create(string? companyName, string companyDescription)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        Company? dbCompany =
             HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
            throw new AlreadyExistException($"{dbCompany.Name} is already exist");
        Company company = new(companyName);
        HRDbContext.Companies.Add(company);
        Console.WriteLine($"{company.Name} has been successfully created \n");
    }

    public void GetAllDepartments(string? companyName)
    {
        int countForDepartmentlessCompany = 0;
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
        {
            foreach ( var department in HRDbContext.Departments)
            {
                if (department.Company==dbCompany.Name)
                {
                    countForDepartmentlessCompany++;
                Console.WriteLine($"{department.Name} Department is in {companyName} Company");
                }
            }
               if(countForDepartmentlessCompany==0) Console.WriteLine($"{companyName} company does not have any department");
        }
        else throw new NotFoundException($"{companyName} Company cannot be found");
    }

}
