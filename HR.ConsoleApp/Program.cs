using HR.Business.Services;
using HR.Business.Utilities.Helper;
using HR.DataAccess.Context;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("Welcome!");
Console.ResetColor();
bool isContinue = true;
while (isContinue)
{
    CompanyService companyService = new();
    DepartmentService departmentService = new();
    EmployeeService employeeService = new();
    Console.WriteLine("--------------------------------------");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Company: \n");
    Console.ResetColor();
    Console.WriteLine("1)Create Company");
    Console.WriteLine("2)Get All Departments In Company");
    Console.WriteLine("3)Delete Company\n");
    Console.WriteLine("--------------------------------------");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Department: \n");
    Console.ResetColor();
    Console.WriteLine("4)Create Department");
    Console.WriteLine("5)Transfer Employee To Department");
    Console.WriteLine("6)Update Department");
    Console.WriteLine("7)Get Department Employee ");
    Console.WriteLine("8)Delete Department \n");
    Console.WriteLine("--------------------------------------");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Employee: \n");
    Console.ResetColor();
    Console.WriteLine("9)Create Employee");
    Console.WriteLine("10)Update The Salary Of An Employee");
    Console.WriteLine("11)Update The Position Of An Employee");
    Console.WriteLine("12)Delete Employee");
    Console.WriteLine("--------------------------------------");
    Console.WriteLine("0)Exit");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("\n Choose an option(as number) :)\n");
    Console.ResetColor();
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption >= 0 && intOption <= 12)
        {
            switch (intOption)
            {
                case (int)ConsoleApp.CreateCompany:
                    try
                    {
                        Console.WriteLine("Enter Company Name:");
                        string? companyName = Console.ReadLine();
                        Console.WriteLine("Enter Company Description:");
                        string? companyDesc = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        companyService.Create(companyName, companyDesc);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.GetAllDepartments:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Companies:\n");
                        foreach (var company in HRDbContext.Companies)
                        {
                            Console.WriteLine(company.Name.ToUpper());
                        }
                        Console.ResetColor();
                        Console.WriteLine("\n Enter Company Name:");
                        string? companyName = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        companyService.GetAllDepartments(companyName);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.DeleteCompany:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("Companies:\n");
                        foreach (var company in HRDbContext.Companies)
                        {
                            Console.WriteLine($"{company.Name.ToUpper()}");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Company Name:");
                        string? companyName = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        companyService.DeleteCompany(companyName);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.CreateDepartment:
                    try
                    {
                        Console.WriteLine("Enter Department Name:");
                        string? departmentName = Console.ReadLine();
                        Console.WriteLine("Enter Department Description:");
                        string? departmentDescription = Console.ReadLine();
                        Console.WriteLine("Enter Company Id:\n");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Companies:\n");
                        foreach (var company in HRDbContext.Companies)
                        {
                            Console.WriteLine($"Id: {company.Id}\n Name: {company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();
                        int companyId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Employee Limit");
                        int employeeLimit = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.Create(departmentName, departmentDescription,companyId, employeeLimit);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.TransferEmployeeToDepartment:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        foreach (var employee in HRDbContext.Employees)
                        {
                            Console.WriteLine($"Id:{employee.Id}/Full Name: {employee.Name.ToUpper()} {employee.Surname.ToUpper()} ");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Employee Id:\n");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name.ToUpper()} \n Company:{department.Company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter New Department Id:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.TransferEmployeeToDepartment(departmentId, employeeId);
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.UpdateDepartment:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name.ToUpper()} \n Company:{department.Company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();

                        Console.WriteLine("Enter Department Id:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter New Name For Department:");
                        string? newDepartmentName = Console.ReadLine();
                        Console.WriteLine($"Enter New Employee Limit:");
                        int newEmployeeLimit = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.UpdateDepartment(newDepartmentName, newEmployeeLimit, departmentId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.GetDepartmentEmployees:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name.ToUpper()}  \n Company: {department.Company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Department Id:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.GetDepartmentEmployees(departmentId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.DeleteDepartment:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name.ToUpper()}  \n Company: {department.Company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Department Id:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.DeleteDepartment(departmentId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;

                case (int)ConsoleApp.CreateEmployee:
                    try
                    {
                        Console.WriteLine("Enter Employee Name:");
                        string? employeeName = Console.ReadLine();
                        Console.WriteLine("Enter Employee Surname:");
                        string? employeeSurname = Console.ReadLine();
                        Console.WriteLine("Enter Employee Salary:");
                        int employeeSalary = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Employee Position:");
                        string? employeePosition = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name.ToUpper()}  \n Company: {department.Company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Department Id:\n");
                        int employeeDepartmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        employeeService.Create(employeeName, employeeSurname, employeeSalary, employeeDepartmentId, employeePosition);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.UpdateSalary:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Departments:");
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Id: {department.Id}\n Name: {department.Name.ToUpper()}  \n Company: {department.Company.Name.ToUpper()}\n \n");
                        }
                        Console.ResetColor();

                        Console.WriteLine("Enter Department Id:");

                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        departmentService.GetDepartmentEmployees(departmentId);
                        Console.ResetColor();
                        Console.WriteLine("Enter Employee Id:");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Console.ResetColor();
                        Console.WriteLine("Enter New Salary:");
                        int newSalary = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        employeeService.UpdateSalary(newSalary, employeeId, departmentId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.UpdatePosition:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("Employees:\n");
                        foreach (var employee in HRDbContext.Employees)
                        {
                            Console.WriteLine($"Id:{employee.Id}/ Full Name:{employee.Name.ToUpper()} {employee.Surname.ToUpper()}\n" +
                                $"Company:{employee.Company.Name.ToUpper()}/ Department:{employee.Department.Name.ToUpper()}/ Position:{employee.Position.ToUpper()}\n\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Employee Id:");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter New Position:");
                        string? newPosition = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        employeeService.UpdatePosition(employeeId, newPosition);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)ConsoleApp.DeleteEmployee:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        foreach (var employee in HRDbContext.Employees)
                        {
                            Console.WriteLine($"Id:{employee.Id}/Full Name: {employee.Name} {employee.Surname}\nPosition: {employee.Position}\n \n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Employee Id:");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        employeeService.DeleteEmployee(employeeId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;

                default:
                    isContinue = false;
                    break;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter correct option number");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please enter correct format\n");
        Console.ResetColor();
    }
}





