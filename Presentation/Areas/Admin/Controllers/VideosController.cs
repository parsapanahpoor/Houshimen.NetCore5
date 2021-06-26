using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class VideosController : Controller
    {
        private readonly IUnitOfWork _context;
        public VideosController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.videosRepository.GetAllVideos());
        }

        public IActionResult Create()
        {
            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);

            return View();
        }


        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]

        public IActionResult Create( Video video, IFormFile imgBlogUp, IFormFile demoUp)
        {
            video.Username = User.Identity.Name;

            if (ModelState.IsValid)
            {

                _context.videosRepository.AddVideo(video , imgBlogUp , demoUp);
                _context.SaveChangesDB();

                return Redirect("/Admin/Videos/Index?Create=true");
            }
            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);

            return View(video);
        }

        public IActionResult Edit(int? id, bool Detail = false, bool Delete = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = _context.videosRepository.GetVideoById((int)id);
            if (video == null)
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
            return View(video);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Video video, IFormFile imgBlogUp, IFormFile demoUp)
        {



            if (ModelState.IsValid)
            {
                _context.videosRepository.UpdateVideo(video, imgBlogUp, demoUp);
                _context.SaveChangesDB();

                return Redirect("/Admin/Videos/Index?Edit=true");
            }

            return View(video);
        }

        public IActionResult AddCategorieToVideos(int id)
        {

            var videos = _context.videosRepository.GetVideoById(id);




            var categories = _context.blogCategoryRepository.GetAllBlogCategories();

            var model = new AddCategoryToVideosViewModel() { VideoId = id };
            foreach (var item in categories)
            {
                if (!_context.videosSelectedCategory.IsVideoInCategory(videos.VideoId, item.BlogCategoryId))
                {
                    model.SelectedVideoCategory.Add(new VideosCategoriesViewModel()
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
        public IActionResult AddCategorieToVideos(AddCategoryToVideosViewModel video)
        {

            var videos = _context.videosRepository.GetVideoById(video.VideoId);

            if (ModelState.IsValid)
            {
                var Categories = video.SelectedVideoCategory.Where(r => r.IsSelected)
                                             .Select(u => u.CategoryId)
                                                       .ToList();
                _context.videosSelectedCategory.AddCategoryToVideo(Categories, video.VideoId);

                _context.SaveChangesDB();
                return Redirect("/Admin/Videos/Index?Create=true");

            }

            return View(video);
        }

        public IActionResult DeleteSelectedCategories(int id)
        {
            var video = _context.videosRepository.GetVideoById(id);
            if (video == null) return NotFound();
            var categories = _context.blogCategoryRepository.GetAllBlogCategories();
            var model = new AddCategoryToVideosViewModel() { VideoId = id };

            foreach (var category in categories)
            {
                if (_context.videosSelectedCategory.IsVideoInCategory(video.VideoId, category.BlogCategoryId))
                {
                    model.SelectedVideoCategory.Add(new VideosCategoriesViewModel()
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
        public IActionResult DeleteSelectedCategories(AddCategoryToVideosViewModel video)
        {
            var blo = _context.blogRepository.GetBlogByID(video.VideoId);

            if (ModelState.IsValid)
            {
                var Categories = video.SelectedVideoCategory.Where(r => r.IsSelected)
                                             .Select(u => u.CategoryId)
                                                       .ToList();

                _context.videosSelectedCategory.DeleteSelectedVideoCategories(Categories, video.VideoId);
                _context.SaveChangesDB();

                return Redirect("/Admin/Videos/Index?Delete=true");


            }

            return View(video);
        }

        public IActionResult Delete(int id)
        {
            var video = _context.videosRepository.GetVideoById(id);
            _context.videosRepository.DeleteVideo(video);
            _context.videosSelectedCategory.DeleteAllVideoCategory(id);
            _context.SaveChangesDB();
            return Redirect("/Admin/Videos/Index?Edit=true");

        }
        public IActionResult LockUser(int videoid, int id)
        {
            var video = _context.videosRepository.GetVideoById(videoid);

            if (id == 1)
            {
                video.IsActive = false;

            }
            if (id == 2)
            {
                video.IsActive = true;

            }
            _context.videosRepository.IsActive(video);
            _context.SaveChangesDB();
            return RedirectToAction(nameof(Index));
        }
    }
}
