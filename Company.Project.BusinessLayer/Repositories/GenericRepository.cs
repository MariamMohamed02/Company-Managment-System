using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.BusinessLayer.Interfaces;
using Company.Project.DataLayer.Data.Contexts;
using Company.Project.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.BusinessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly CompanyDbContext _context;
        public GenericRepository(CompanyDbContext context) {
            _context = context;
         }
        public async Task< int> Add(T model)
        {
           await _context.Set<T>().AddAsync(model);
            return _context.SaveChanges();
        }

        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }




        public async Task<T?> Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e=>e.Id==id)as T;
            }
            
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>>GetAll()
        {
            // allow null values for department to be loaded
            if (typeof(T) == typeof(Employee)){
                return (IEnumerable<T>) await _context.Employees.Include(e => e.Department).ToListAsync();
            } 
            return await _context.Set<T>().ToListAsync();  
        }

        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
    }
}
