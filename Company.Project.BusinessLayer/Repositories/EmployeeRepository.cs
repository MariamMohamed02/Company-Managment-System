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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _context;
        public EmployeeRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public int Add(Employee model)
        {
            _context.Employees.Add(model);
            return _context.SaveChanges();
        }

        public int Delete(Employee model)
        {
            _context.Employees.Remove(model);
            return _context.SaveChanges();
        }

        public Employee? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public int Update(Employee model)
        {
            _context.Employees.Update(model);
            return _context.SaveChanges();
        }
    }
}
