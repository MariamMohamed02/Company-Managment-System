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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        // Ask CLR to ceate object from CompanyDbCOntext
        public DepartmentRepository(CompanyDbContext context) : base(context)
        {
        }
    }
}
