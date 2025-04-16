using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.BusinessLayer.Interfaces;
using Company.Project.DataLayer.Data.Contexts;
using Company.Project.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Company.Project.BusinessLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        // Ask CLR to ceate object from CompanyDbCOntext
        public EmployeeRepository(CompanyDbContext context) : base(context) {
            _context = context;
        }

        public List<Employee> GetByName(string name)
        {
            return _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

    }
}
