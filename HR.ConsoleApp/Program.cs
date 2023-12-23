using HR.Business.Services;
CompanyService companyService= new CompanyService();
companyService.Create("code", "");
companyService.Create("tech", "");
DepartmentService departmentService= new DepartmentService();
departmentService.Create("ux", "", "code", 4);
EmployeeService employeeService= new EmployeeService();
employeeService.Create("enel", "", 10, "code", "ux");

