using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IUnitOfWork _context;
        public BlogsController(IUnitOfWork context)
        {
            _context = context;
        }
    
        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {

            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;


            return View();
        }
    }
}
