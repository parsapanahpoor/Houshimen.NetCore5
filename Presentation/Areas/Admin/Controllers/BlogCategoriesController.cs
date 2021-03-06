using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogCategoriesController : Controller
    {
        private protected IUnitOfWork _context;
        public BlogCategoriesController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {

            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;


            return View(_context.blogCategoryRepository.GetAllBlogCategories());
        }
        public IActionResult Create(int? id)
        {
            return View(new BlogCategory()
            {

                ParentId = id

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BlogCategoryId,CategoryTitle,IsDelete,ParentId")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                _context.blogCategoryRepository.AddBlogCategory(blogCategory);
                _context.SaveChangesDB();
                return Redirect("/Admin/BlogCategories/Index?Create=true");


            }
            return View(blogCategory);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = _context.blogCategoryRepository.GetBlogCategoryById((int)id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BlogCategoryId,CategoryTitle,IsDelete,ParentId")] BlogCategory blogCategory)
        {
            if (id != blogCategory.BlogCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.blogCategoryRepository.UpdateBlogCategroy(blogCategory, id);
                _context.SaveChangesDB();

                return Redirect("/Admin/BlogCategories/Index?Edit=true");
            }
            return View(blogCategory);
        }

        public IActionResult Delete(int id)
        {


            _context.blogCategoryRepository.DeleteBlogCategory((int)id);
            _context.SaveChangesDB();

            return Redirect("/Admin/BlogCategories/Index?Delete=true");
        }

    }
}
