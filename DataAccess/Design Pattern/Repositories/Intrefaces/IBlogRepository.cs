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
    public interface IBlogRepository : IGernericRepository<Blog>
    {

        List<Blog> GetAllBlogs();
        int AddBlog(Models.Entities.Blog.Blog blog, IFormFile imgBlogUp);
        Blog GetBlogByID(int id);
        int GetBlogByImageName(string imagename);
        void UpdateBlog(Blog blog, IFormFile imgBlogUp);
        void DeleteBlog(Models.Entities.Blog.Blog blog);
        void IsActive(Blog blog);
    }
}
