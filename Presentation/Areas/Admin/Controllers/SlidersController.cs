using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IUnitOfWork _context;
        public SlidersController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;


            return View(_context.sliderRepository.GetAllSliders());
        }
        public IActionResult Create()
        {
            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);
            ViewData["UserName"] = User.Identity.Name;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Slider slider, IFormFile imgBlogUp)
        {
            if (ModelState.IsValid)
            {
                
                _context.sliderRepository.AddSlider(slider, imgBlogUp);
                _context.SaveChangesDB();

                return Redirect("/Admin/Sliders/Index?Create=true");
            }

            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);
            ViewData["UserName"] = User.Identity.Name;

            return View(slider);
        }
        public IActionResult Edit(int? id, bool Delete = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = _context.sliderRepository.GetSliderById((int)id);
            if (slider == null)
            {
                return NotFound();
            }
            if (Delete == true)
            {
                ViewData["Delete"] = true;
            }


        

            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Slider slider, IFormFile imgBlogUp)
        {
            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                _context.sliderRepository.UpdateSlider(slider, imgBlogUp);
                _context.SaveChangesDB();

                return Redirect("/Admin/Sliders/Index?Edit=true");
            }


            return View(slider);
        }

        public IActionResult Delete(int id)
        {
            var slider = _context.sliderRepository.GetSliderById(id);
            _context.sliderRepository.DeleteSldier(slider);
            _context.SaveChangesDB();

            return RedirectToAction(nameof(Index));
        }

    }
}
