using HR.Business.Services;
CompanyService companyService= new CompanyService();
companyService.Create("code", "");
companyService.Create("tech", "");
DepartmentService departmentService= new DepartmentService();
departmentService.Create("ux", "", "code", 4);
departmentService.Create("ui", "", "code", 4);
EmployeeService employeeService= new EmployeeService();
employeeService.Create("Enel", "", 10, "code", "ux");
departmentService.AddEmployeeToDepartment("ui", "", "eNel", 0);
departmentService.GetDepartmentEmployees("ui", "code");
//o biri dep den silmelidir


