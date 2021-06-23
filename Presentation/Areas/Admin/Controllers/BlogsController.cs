using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Blog;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

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


            return View(_context.blogRepository.GetAllBlogs());
        }

        public IActionResult Create()
        {

            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);
            ViewData["Username"] = User.Identity.Name;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BlogId,UserId,BlogTitle,ShortDescription,LongDescription,BlogImageName,Tags,CreateDate,IsActive,IsDelete,UserName")] Blog blog, IFormFile imgBlogUp)
        {



            if (ModelState.IsValid)
            {
                var blogid = _context.blogRepository.AddBlog(blog, imgBlogUp);
                _context.SaveChangesDB();



                return Redirect("/Admin/Blogs/Index?Create=true");
            }
            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);
            ViewData["Username"] = User.Identity.Name;




            return View(blog);
        }

        public IActionResult AddCategorieToBlogs(int id)
        {

            var blog = _context.blogRepository.GetBlogByID(id);


         

            var categories = _context.blogCategoryRepository.GetAllBlogCategories();

            var model = new AddCategoryToBlogViewModel() { BlogId = id };
            foreach (var item in categories)
            {
                if (!_context.blogSelectedCategoryRepository.IsBlogInCategory(blog.BlogId, item.BlogCategoryId))
                {
                    model.SelectedBlogCategory.Add(new BlogCategoriesViewModel()
                    {
                        CategoriName = item.CategoryTitle,
                        CategoryId = item.BlogCategoryId
                    });
                }
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategorieToBlogs(AddCategoryToBlogViewModel blo)
        {


            var blog = _context.blogRepository.GetBlogByID(blo.BlogId);

            if (ModelState.IsValid)
            {
                var Categories = blo.SelectedBlogCategory.Where(r => r.IsSelected)
                                             .Select(u => u.CategoryId)
                                                       .ToList();
                _context.blogSelectedCategoryRepository.AddCategoryToBlog(Categories,    blog.BlogId );

                _context.SaveChangesDB();
                return Redirect("/Admin/Blogs/Index?Create=true");

            }




            return View(blog);
        }

        public IActionResult DeleteSelectedCategories(int id)
        {
            var blog =  _context.blogRepository.GetBlogByID(id);
            if (blog == null) return NotFound();
            var categories =_context.blogCategoryRepository.GetAllBlogCategories();
            var model = new AddCategoryToBlogViewModel() { BlogId = id };

            foreach (var category in categories)
            {
                if (_context.blogSelectedCategoryRepository.IsBlogInCategory(blog.BlogId, category.BlogCategoryId))
                {
                    model.SelectedBlogCategory.Add(new BlogCategoriesViewModel()
                    {
                        CategoriName = category.CategoryTitle,
                        CategoryId = category.BlogCategoryId
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSelectedCategories(AddCategoryToBlogViewModel blog)
        {       
            var blo =  _context.blogRepository.GetBlogByID(blog.BlogId);

            if (ModelState.IsValid)
            {
                var Categories = blog.SelectedBlogCategory.Where(r => r.IsSelected)
                                             .Select(u => u.CategoryId)
                                                       .ToList();

                _context.blogSelectedCategoryRepository.DeleteSelectedBlogCategories(Categories, blog.BlogId);
                _context.SaveChangesDB();

                return Redirect("/Admin/Blogs/Index?Delete=true");


            }

            return View(blog);
        }

        public IActionResult Edit(int? id , bool Detail = false , bool Delete = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _context.blogRepository.GetBlogByID((int)id);
            if (blog == null)
            {
                return NotFound();
            }

            if (Detail == true)
            {
                ViewData["Detail"] = true;
            }

            if (Delete == true)
            {
                ViewData["Delete"] = true;
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( Blog blog, IFormFile imgBlogUp)
        {

            if (ModelState.IsValid)
            {

                 _context.blogRepository.UpdateBlog(blog, imgBlogUp);
                _context.SaveChangesDB();

                return Redirect("/Admin/Blogs/Index?Edit=true");
            }
 
            return View(blog);
        }

        public IActionResult Delete(int id )
        {
            var blog = _context.blogRepository.GetBlogByID(id);
            _context.blogRepository.DeleteBlog(blog);
            _context.blogSelectedCategoryRepository.DeleteAllBlogCategory(id);
            _context.SaveChangesDB();
            return Redirect("/Admin/Blogs/Index?Edit=true");

        }
        public IActionResult LockUser(int blogid, int id)
        {
            var blog = _context.blogRepository.GetBlogByID(blogid);

            if (id == 1)
            {
                blog.IsActive = false;

            }
            if (id == 2)
            {
                blog.IsActive = true;

            }
            _context.blogRepository.IsActive(blog);
            _context.SaveChangesDB();
            return RedirectToAction(nameof(Index));
        }
    }
}
