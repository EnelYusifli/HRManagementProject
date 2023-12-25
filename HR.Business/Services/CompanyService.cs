using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;
using System.Xml.Linq;


namespace HR.Business.Services;

public class CompanyService : ICompanyService
{
    public void Create(string? companyName, string? companyDescription)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        Company? dbCompany =
             HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
            throw new AlreadyExistException($"{dbCompany.Name.ToUpper()} is already exist");
        Company company = new(companyName);
        HRDbContext.Companies.Add(company);
        Console.WriteLine($"The new company- {company.Name.ToUpper()} has been successfully created \n");
    }

    public void GetAllDepartments(string? companyName)
    {
        int counter = 0;
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
        {
            foreach ( var department in HRDbContext.Departments)
            {
                if (department.CompanyName.ToLower() == dbCompany.Name.ToLower())
                {
                    counter++;
                Console.WriteLine($" Departments in {companyName.ToUpper()} Company:\n{department.Id}){department.Name.ToUpper()} Department\n");
                }
            }
               if(counter==0) Console.WriteLine($"{companyName} company does not have any department");
        }
        else throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
    }
    public Company? FindCompanyByName(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        return HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
    }

    public Company? FindCompanyById(int companyId=-1)
    {
        if (companyId <0)
            throw new ArgumentNullException();
        return HRDbContext.Companies.Find(c => c.Id == companyId);
    }
}
