using AutoMapper;
using Company.Project.BusinessLayer.Interfaces;
using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Company.Project.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
          private readonly IUnitOfWork _unitOfWork;
    //    private readonly IEmployeeRepository _employeeRepository;
    //    private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository, 
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees= _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);

            }
            
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var department = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"]=department; // since u cant have 2 datatypes in the employe view @model therefore send this info in the dictionary
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                   model.ImageName= DocumentSettings.UploadFile(model.Image, "images");
                }

                
                var employee= _mapper.Map<Employee>(model);
                if (_unitOfWork.EmployeeRepository.Add(employee) > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid ID");
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} is not found" });
           
            
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id) // Action to go to the view of the Update 
        {

            var department = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = department; // since u cant have 2 datatypes in the employe view @model therefore send th


            // Return the Deatails Action (not the View Action)
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee) // Action to go to submit the updated values 
        {

            if (ModelState.IsValid)
            {

                // if (id != department.Id) return BadRequest(); 
                if (id == employee.Id)
                {
                    var count = _unitOfWork.EmployeeRepository.Update(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(employee);
        }







        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee == null) return NotFound();

            ViewData["departments"] = _unitOfWork.DepartmentRepository.GetAll();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);

            if (employee == null)
                return NotFound();

            var count = _unitOfWork.EmployeeRepository.Delete(employee);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            // Optional: show error if deletion failed
            return View(employee);
        }





    }
}
