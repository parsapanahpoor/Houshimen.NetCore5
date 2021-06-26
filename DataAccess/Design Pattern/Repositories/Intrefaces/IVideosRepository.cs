using DataAccess.Design_Pattern.GnericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.Blog;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
   public  interface IVideosRepository  : IGernericRepository<Video>
    {

        List<Video> GetAllVideos();
        void AddVideo(Models.Entities.Blog.Video video, IFormFile imgBlogUp, IFormFile demoUp);
        Video GetVideoById(int id);
        void UpdateVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp);
        void DeleteVideo(Models.Entities.Blog.Video video);
        public void IsActive(Video video);

    }
}
