using HR.Business.Services;
CompanyService companyService= new CompanyService();
companyService.Create("code", "");
companyService.Create("tech", "");
DepartmentService departmentService= new DepartmentService();
departmentService.Create("ux", "", "code", 4);
departmentService.Create("ui", "", "code", 4);
EmployeeService employeeService= new EmployeeService();
employeeService.Create("Enel", "a", 10, "code", "ux");
employeeService.Create("Aylin", "a", 10, "code", "ux");
employeeService.Create("Ramin", "a", 10, "code", "ux");
employeeService.Create("Jale", "a", 10, "code", "ux");
departmentService.AddEmployeeToDepartment("ui", "code", "eNel", 0);
departmentService.AddEmployeeToDepartment("ui", "code", "jale", 3);
departmentService.GetDepartmentEmployees("ui", "code");
Console.WriteLine("------------");
departmentService.GetDepartmentEmployees("ux", "code");



