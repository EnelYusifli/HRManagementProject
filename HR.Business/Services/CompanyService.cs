using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Context;


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
            throw new AlreadyExistException($"{dbCompany.Name.ToUpper()} Company is already exist");
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
            Console.WriteLine($"Departments in {companyName.ToUpper()} Company:");
            foreach ( var department in HRDbContext.Departments)
            {
                if (department.Company.Name.ToLower() == dbCompany.Name.ToLower())
                {
                    counter++;
                Console.WriteLine($"{department.Id}){department.Name.ToUpper()} Department\n");
                }
            }
               if(counter==0) Console.WriteLine($"{companyName} company does not have any department");
        }
        else throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
    }
    public void GetAllEmployees(string? companyName)
    {
        int counter = 0;
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is not null)
        {
            Console.WriteLine($"Employees in {companyName.ToUpper()} Company:\n");
            foreach (var employee in HRDbContext.Employees)
            {
                if (employee.Company.Name.ToLower() == dbCompany.Name.ToLower())
                {
                    counter++;
                    Console.WriteLine($"Id:{employee.Id}/Full Name:{employee.Name.ToUpper()} {employee.Surname.ToUpper()}\n" +
                        $"Department:{employee.Department.Name.ToUpper()}/ Position:{employee.Position.ToUpper()}\n\n");
                }
            }
            if (counter == 0) Console.WriteLine($"{companyName} company does not have any department");
        }
        else throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
    }
    public Company? FindCompanyByName(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        return HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
    }
    public void DeleteCompany(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();

        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());

        if (dbCompany is not null)
        {
            bool hasDepartments = HRDbContext.Departments.Any(d => d.Company.Name.ToLower() == dbCompany.Name.ToLower());
            if (hasDepartments)
                throw new NotFoundException($"Cannot delete {companyName.ToUpper()} Company as it has associated departments");
            else
            {
                HRDbContext.Companies.Remove(dbCompany);
                Console.WriteLine($"{companyName.ToUpper()} Company has been successfully deleted");
            }
        }
        else
        {
            throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
        }
    }

    
}
