using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.DataLayer.Models;

namespace Company.Project.BusinessLayer.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        Task<int> Add(T model);
        int Update(T model);

        int Delete(T model);
    }
}
