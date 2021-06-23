using DataAccess.Design_Pattern.GnericRepositories;
using Models.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
    public  interface IBlogSelectedCategoryRepository : IGernericRepository<BlogSelectedCategory>
    {

        void AddCategoryToBlog(List<int> Categories, int BlogId);
        List<BlogSelectedCategory> GetAllBlogSelectedCategory();
        void DeleteSelectedBlogCategories(List<int> Categories, int BlogId);
        bool IsBlogInCategory(int blogid  , int categoriid);
        void DeleteAllBlogCategory(int id);

    }
}
