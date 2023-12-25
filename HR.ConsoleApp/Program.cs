using HR.Business.Services;
using HR.Business.Utilities.Helper;
using HR.DataAccess.Context;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Welcome!");
bool isContinue = true;
while (isContinue)
{

CompanyService companyService = new();
DepartmentService departmentService = new();
EmployeeService employeeService = new();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Company: \n");
Console.ResetColor();
Console.WriteLine("1)Create Company");
Console.WriteLine("2)Get All Departments In Company\n");
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Department: \n");
Console.ResetColor();
Console.WriteLine("3)Create Department");
Console.WriteLine("4)Add Employee To Department");
Console.WriteLine("5)Update Department");
Console.WriteLine("6)Get Department Employee \n");

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Employee: \n");
Console.ResetColor();
Console.WriteLine("7)Create Employee");
Console.WriteLine("8)Update The Salary Of An Employee\n");
Console.WriteLine("0)Exit");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("Choose an option(with a number) :)");
Console.ResetColor();
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption >= 0 && intOption <= 8)
        {
            switch (intOption)
            {
                case (int)ConsoleApp.CreateCompany:
                    try
                    {
                        Console.WriteLine("Enter Company Name:");
                        string? companyName= Console.ReadLine();
                        Console.WriteLine("Enter Company Description:");
                        string? companyDesc= Console.ReadLine();
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
                        Console.WriteLine("Enter Company Name:");
                        string? companyName=Console.ReadLine();
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
                case (int)ConsoleApp.CreateDepartment:
                    try
                    {
                        Console.WriteLine("Enter Department Name:");
                        string? departmentName= Console.ReadLine();
                        Console.WriteLine("Enter Department Description:");
                        string? departmentDescription = Console.ReadLine();
                        Console.WriteLine("Enter Company of Department:");
                        string? departmentCompany= Console.ReadLine();
                        Console.WriteLine("Enter Employee Limit");
                        int employeeLimit=Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.Create(departmentName,departmentDescription,departmentCompany, employeeLimit);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                    case (int)ConsoleApp.AddEmployeeToDepartment:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name} \n Company:{department.Company}\n \n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Department Id:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        foreach (var employee in HRDbContext.Employees)
                        {
                            if (employee.DepartmentId == departmentId)
                                Console.WriteLine($"Id:{employee.Id}/Full Name: {employee.Name} {employee.Surname} ");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Employee Id:\n");
                        int employeeId=Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.AddEmployeeToDepartment(departmentId,employeeId);
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
                        Console.WriteLine("Enter Department Name:");
                        string? oldDepartmentName=Console.ReadLine();
                        Console.WriteLine("Enter New Name For Department:");
                        string? newDepartmentName=Console.ReadLine();
                        Console.WriteLine("Enter Company Name:");
                        string? companyName=Console.ReadLine();
                        Console.WriteLine($"Enter New Employee Limit:");
                        int newEmployeeLimit=Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.UpdateDepartment(oldDepartmentName,newDepartmentName,companyName,newEmployeeLimit);
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
                        Console.WriteLine("Enter Department Name:");
                        string? departmentName = Console.ReadLine();
                        Console.WriteLine("Enter Company Name:");
                        string? companyName = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        departmentService.GetDepartmentEmployees(departmentName, companyName);
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
                        int employeeSalary=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Employee Position:");
                        string? employeePosition=Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name} \n Company:{department.Company}\n \n");
                        }
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Enter Employee Department Id:\n");
                        //foreach (var employee in HRDbContext.Departments)
                        //{
                        //    Console.WriteLine($"Employees:\n Id: {employee.Id}\n Name: {employee.Name} \n Company:{employee.Company}\n \n");
                        //Console.ResetColor();
                        //}
                        int employeeDepartmentId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        employeeService.Create(employeeName,employeeSurname,employeeSalary,employeeDepartmentId,employeePosition);
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
                        foreach (var department in HRDbContext.Departments)
                        {
                            Console.WriteLine($"Departments:\n Id: {department.Id}\n Name: {department.Name} \n Company:{department.Company}\n \n");
                        }
                        Console.ResetColor();

                        Console.WriteLine("Enter Department Id:");

                        int departmentId=Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        departmentService.GetDepartmentEmployees(departmentName,companyName);
                        Console.WriteLine("Enter Employee Id:");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Console.ResetColor();
                        Console.WriteLine("Enter New Salary");
                        int newSalary = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        employeeService.UpdateSalary(employeeId, departmentName, companyName, newSalary);
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





