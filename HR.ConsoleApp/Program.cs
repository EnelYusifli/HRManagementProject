using HR.Business.Services;

CompanyService companyService = new CompanyService();
companyService.Create("code", "");
companyService.Create("tech", "");
companyService.Create("step", "");
DepartmentService departmentService = new DepartmentService();
departmentService.Create("e", "", 5, "code");
departmentService.Create("l", "", 5, "tech");
//companyService.GetAllDepartments("b");
EmployeeService employeeService = new EmployeeService();
employeeService.Create("enel", "", 10,"code", "a");
