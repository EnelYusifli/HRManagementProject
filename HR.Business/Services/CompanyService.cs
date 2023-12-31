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
        Company company = new(companyName, companyDescription);
        HRDbContext.Companies.Add(company);
        Console.WriteLine($"The new company- {company.Name.ToUpper()} has been successfully created \n");
    }

    public void GetAllDepartments(string? companyName)
    {
        int counter = 0;
        string activeOrDeactive;
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
                    if (department.IsActive == true)
                        activeOrDeactive = "active";
                    else
                        activeOrDeactive = "deactive";
                    counter++;
                Console.WriteLine($"{department.Id}){department.Name.ToUpper()} Department\n" +
                    $"Status:{activeOrDeactive}/Employee Limit:{department.EmployeeLimit}/ " +
                    $"Current Employee Count:{department.currentEmployeeCount}");
                }
            }
               if(counter==0) throw new NotFoundException($"{companyName} company does not have any department");
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
            if (counter == 0) throw new NotFoundException($"{companyName} company does not have any employee");
        }
        else throw new NotFoundException($"{companyName.ToUpper()} Company cannot be found");
    }
    public Company? FindCompanyByName(string? companyName)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        return HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
    }
    public void UpdateCompany(string? companyName, string? newCompanyName, string? newDescription)
    {
        if (String.IsNullOrEmpty(companyName))
            throw new ArgumentNullException();
        if (String.IsNullOrEmpty(newCompanyName))
            throw new ArgumentNullException();
        Company? dbCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null)
            throw new NotFoundException($"Company cannot be found");
        Company? dbNewCompany =
            HRDbContext.Companies.Find(c => c.Name.ToLower() == newCompanyName.ToLower());
        if (dbNewCompany is not null )
            throw new AlreadyExistException($"{newCompanyName.ToUpper()} Company is already exist");
        dbCompany.Name = newCompanyName;
        dbCompany.Description = newDescription;
        Console.WriteLine($"{newCompanyName.ToUpper()} Company has been successfully updated");
    }
    public void ShowAllCompanies()
    {
        foreach (var company in HRDbContext.Companies)
        {
            Console.WriteLine($"{company.Id}) {company.Name.ToUpper()}");
        }
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
                throw new NotEmptyException($"Cannot delete {companyName.ToUpper()} Company as it has associated departments");
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
