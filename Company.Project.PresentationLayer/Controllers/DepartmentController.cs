using Company.Project.BusinessLayer.Interfaces;
using Company.Project.BusinessLayer.Repositories;
using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmenDto model)
        {
            if (ModelState.IsValid) {
                // store data in the databse
                var department=new Department()
                {
                    Code =model.Code,
                    Name = model.Name,
                    CreatedAt = model.CreatedAt,

                };
                if (_departmentRepository.Add(department) >0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

    }
}
