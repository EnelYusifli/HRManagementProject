using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Core.Entities;

namespace HR.DataAccess.Context
{
    public static class HRDbContext
    {
        public static List<Company> Companies { get; set; } = new List<Company>();
        public static List<Department> Departments { get; set; } = new List<Department>();
        public static List<Employee> Employees { get; set; } = new List<Employee>();
    }
    
}
