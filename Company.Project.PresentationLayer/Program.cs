using Company.Project.BusinessLayer;
using Company.Project.BusinessLayer.Interfaces;
using Company.Project.BusinessLayer.Repositories;
using Company.Project.DataLayer.Data.Contexts;
using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Company.Project.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // allow dependency injection for the department repository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // allow dependency injection for the employee repository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // allow di for dbcontext
            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>()
                .AddDefaultTokenProviders(); // allow DI for user,role and sign in managers

            builder.Services.ConfigureApplicationCookie(
                config => {config.LoginPath = "/Account/SignIn";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
