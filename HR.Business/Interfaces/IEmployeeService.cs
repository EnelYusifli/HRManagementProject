﻿using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string? employeeName, string? employeeSurname, int employeeSalary, int departmentId,string? position);
    void UpdateSalary(int employeeId,int departmentId,int newSalary);
    Employee? FindEmployeeById(int employeeId);
    
}
