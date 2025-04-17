using Company.Project.BusinessLayer.Interfaces;
using Company.Project.BusinessLayer.Repositories;
using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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
                if (_unitOfWork.DepartmentRepository.Add(department) >0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int? id, string viewName="Details") {
            if (id is null) return BadRequest("Invalid ID");
            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound(new {statusCode=404 , message=$"Department with Id : {id} is not found"});
            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id) // Action to go to the view of the Update 
        {
            // Return the Deatails Action (not the View Action)
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Department department) // Action to go to submit the updated values 
        {
           
            if (ModelState.IsValid) {

               // if (id != department.Id) return BadRequest(); 
                if (id == department.Id)
                {
                    var count = _unitOfWork.DepartmentRepository.Update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                
            }
            return View(department);
        }


        [HttpGet]
        public IActionResult Delete(int? id)  // Redirect to the Delete Page
        {
            return Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department) // Action to actually delete
        {

            if (ModelState.IsValid)
            {

                // if (id != department.Id) return BadRequest(); 
                if (id == department.Id)
                {
                    var count = _unitOfWork.DepartmentRepository.Delete(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(department);
        }


        

    }
}
