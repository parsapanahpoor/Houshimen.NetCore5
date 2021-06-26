using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Microsoft.AspNetCore.Http;
using Models.Entities.Blog;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;
using Utilities.Security;

namespace DataAccess.Design_Pattern.Repositories.Classes
{


    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly HoushimenContext _db;

        public BlogRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public int AddBlog(Blog blog, IFormFile imgBlogUp)
        {

            Blog blo = new Blog();


            blo.UserId = blog.UserId;
            blo.IsActive = true;
            blo.CreateDate = DateTime.Now;
            blo.BlogImageName = "no-photo.png";  //تصویر پیشفرض
            blo.BlogTitle = blog.BlogTitle;
            blo.ShortDescription = blog.ShortDescription;
            blo.LongDescription = blog.LongDescription;
            blo.Tags = blog.Tags;
            blo.IsDelete = false;
            blo.UserName = blog.UserName;


            //TODO Check Image
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {
                blo.BlogImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image", blo.BlogImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blo.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);

                ImageConvertor imgResizer400 = new ImageConvertor();
                string thumbPath400 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image/400px", blo.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath400, 400);
            }

            Add(blo);

            return blo.BlogId;

        }

        public void DeleteBlog(Models.Entities.Blog.Blog blog)
        {
            Delete(blog);

            //TODO Check Image
            if (blog.BlogImageName != null)
            {

                if (blog.BlogImageName != "no-photo.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image", blog.BlogImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blog.BlogImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                    string deletethumb400Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image/400px", blog.BlogImageName);
                    if (File.Exists(deletethumb400Path))
                    {
                        File.Delete(deletethumb400Path);
                    }
                }

            }
        }

        public List<Blog> GetAllBlogs()
        {
            return GetAll().OrderByDescending(p => p.CreateDate).ToList();
        }

        public Blog GetBlogByID(int id)
        {
            return GetById(id);
        }

        public int GetBlogByImageName(string imagename)
        {
            return GetAll(p => p.BlogImageName == imagename).Select(p => p.BlogId).Single();
        }

        public void IsActive(Blog blog)
        {
            Update(blog);
        }

        public void UpdateBlog(Blog blog, IFormFile imgBlogUp)
        {
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {

                if (blog.BlogImageName != "no-photo.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image", blog.BlogImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                }



                blog.BlogImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image", blog.BlogImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }


                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blog.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);

                ImageConvertor imgResizer400 = new ImageConvertor();
                string thumbPath400 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Image/400px", blog.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath400, 400);
            }

            Update(blog);
        }
    }
}
