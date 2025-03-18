using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.Project.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.DataLayer.Data.Contexts
{
    internal class CompanyDbContext: DbContext 
    {

        public CompanyDbContext():base()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=CompanyManagment; Trusted_Connection=True; TrustServerCertificate=True")
        }

        public DbSet<Department> Departments { get; set; }

    }


}
