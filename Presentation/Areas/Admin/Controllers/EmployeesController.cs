using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _context;
        public EmployeesController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;



            return View(_context.employeeRepository.GetAllkEmployees());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId,UserName,EmployeeLevel,Description,SocialMedia1,SocialMedia2,PersonalLink,UserAvatar,IsDelete")] Employee employee, IFormFile imgBlogUp)
        {
            if (ModelState.IsValid)
            {
                _context.employeeRepository.AddEmployees(employee, imgBlogUp);
                _context.SaveChangesDB();

                return Redirect("/Admin/Employees/Index?Create=true");
            }
            return View(employee);
        }

        public IActionResult Edit(int? id , bool Delete = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.employeeRepository.GetEmployeeById((int)id);
            if (employee == null)
            {
                return NotFound();
            }
            if (Delete == true)
            {
                ViewData["Delete"] = true;
            }
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Employee employee, IFormFile imgBlogUp)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.employeeRepository.UpdateEmployee(employee, imgBlogUp);
                _context.SaveChangesDB();
                return Redirect("/Admin/Employees/Index?Edit=true");
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.employeeRepository.GetEmployeeById(id);
            _context.employeeRepository.DeleteEmployee(employee);
            _context.SaveChangesDB();

            return Redirect("/Admin/Employees/Index?Delete=true");
        }

    }
}
