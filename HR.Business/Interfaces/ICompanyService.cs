﻿using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface ICompanyService
{
    void Create(string? companyName, string companyDescription);
    void GetAllDepartments(string? companyName);
    void GetAllEmployees(string? companyName);
    Company? FindCompanyByName(string? companyName);
    void UpdateCompany(string? companyName,string? newCompanyName,string? newDescription);
    void ShowAllCompanies();
    void DeleteCompany(string? companyName);

}
