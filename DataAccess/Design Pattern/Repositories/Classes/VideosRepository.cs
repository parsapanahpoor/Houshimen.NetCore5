using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Microsoft.AspNetCore.Http;
using Models.Entities.Blog;
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
    public class VideosRepository : GenericRepository<Video>, IVideosRepository
    {
        private readonly HoushimenContext _db;

        public VideosRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp)
        {
            video.IsActive = true;
            video.CreateDate = DateTime.Now;
            video.VideoImageName = "no-photo.png";  //تصویر پیشفرض
            //TODO Check Image
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {
                video.VideoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.VideoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);

                string pxPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos/400px", video.VideoImageName);

                imgResizer.Image_resize(imagePath, pxPath, 400);


            }

            if (demoUp != null)
            {
                video.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(demoUp.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demoUp.CopyTo(stream);
                }
            }

            Add(video);
        }

        public void UpdateVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp)
        {
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {

                if (video.VideoImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.VideoImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }

                    string deletet400bPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos/400px", video.VideoImageName);
                    if (File.Exists(deletet400bPath))
                    {
                        File.Delete(deletet400bPath);
                    }

                }


                video.VideoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.VideoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);

                string pxPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos/400px", video.VideoImageName);

                imgResizer.Image_resize(imagePath, pxPath, 400);
            }

            if (demoUp != null)
            {

                if (video.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }

                video.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(demoUp.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demoUp.CopyTo(stream);
                }
            }

            Update(video);

        }

        public List<Video> GetAllVideos()
        {
            return GetAll().OrderByDescending(p => p.CreateDate).ToList();
        }

        public Video GetVideoById(int id)
        {
            return GetById(id);
        }

        public void DeleteVideo(Video video)
        {
            Delete(video);

            //TODO Check Image
            if (video.VideoImageName != "no-photo.jpg")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.VideoImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }

                string deletet400bPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos/400px", video.VideoImageName);
                if (File.Exists(deletet400bPath))
                {
                    File.Delete(deletet400bPath);
                }

            }

            if (video.DemoFileName != null)
            {
                string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                if (File.Exists(deleteDemoPath))
                {
                    File.Delete(deleteDemoPath);
                }
            }


        }

        public void IsActive(Video video)
        {
            Update(video);
        }
    }
    }
