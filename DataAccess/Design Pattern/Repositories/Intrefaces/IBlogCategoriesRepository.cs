using DataAccess.Design_Pattern.GnericRepositories;
using Models.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
    public interface IBlogCategoriesRepository : IGernericRepository<BlogCategory>
    {

        List<BlogCategory> GetAllBlogCategories();
        void AddBlogCategory(BlogCategory blogCategory);
        BlogCategory GetBlogCategoryById(int id);
        void UpdateBlogCategroy(BlogCategory blogCategory, int id);
        void DeleteBlogCategory(int id);

    }
}
