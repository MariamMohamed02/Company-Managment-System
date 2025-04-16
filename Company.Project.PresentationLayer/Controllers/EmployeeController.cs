using AutoMapper;
using Company.Project.BusinessLayer.Interfaces;
using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,AutoMapper.IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees= _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetByName(SearchInput);

            }
            
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var department = _departmentRepository.GetAll();
            ViewData["departments"]=department; // since u cant have 2 datatypes in the employe view @model therefore send this info in the dictionary
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                // store data in the databse
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId=model.DepartmentId
                //};
                var employee= _mapper.Map<Employee>(model);
                if (_employeeRepository.Add(employee) > 0)
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
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} is not found" });
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id) // Action to go to the view of the Update 
        {

            var department = _departmentRepository.GetAll();
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
                    var count = _employeeRepository.Update(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(employee);
        }


        [HttpGet]
        public IActionResult Delete(int? id)  // Redirect to the Delete Page
        {
            var department = _departmentRepository.GetAll();
            ViewData["departments"] = department; // since u cant have 2 datatypes in the employe view @model therefore send th
            return Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee) // Action to actually delete
        {

            if (ModelState.IsValid)
            {

                //  if (id != department.Id) return BadRequest(); 
                if (id == employee.Id)
                {
                    var count = _employeeRepository.Delete(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }


            }
            return View(employee);
        }




    }
}
