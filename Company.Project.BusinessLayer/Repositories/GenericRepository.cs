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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly CompanyDbContext _context;
        public GenericRepository(CompanyDbContext context) {
            _context = context;
         }
        public int Add(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }

        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }

        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();  
        }

        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
    }
}
