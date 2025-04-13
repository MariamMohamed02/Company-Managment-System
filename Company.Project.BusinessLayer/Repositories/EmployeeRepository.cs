using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.BusinessLayer.Interfaces;
using Company.Project.DataLayer.Data.Contexts;
using Company.Project.DataLayer.Models;

namespace Company.Project.BusinessLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // Ask CLR to ceate object from CompanyDbCOntext
        public EmployeeRepository(CompanyDbContext context) : base(context) { 
        
        } 
        
    }
}
