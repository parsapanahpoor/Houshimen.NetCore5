using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Models.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoriesRepository
    {
        private readonly HoushimenContext _db;

        public BlogCategoryRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddBlogCategory(BlogCategory blogCategory)
        {
            BlogCategory blog = new BlogCategory()
            {
                ParentId = blogCategory.ParentId,
                CategoryTitle = blogCategory.CategoryTitle,
                IsDelete = false,



            };


            Add(blog);
        }

        public void DeleteBlogCategory(int id)
        {
            var category = GetById(id);
            var sub = GetAll(p => p.ParentId == id).ToList();
            if (sub != null)
            {

                foreach (var item in sub)
                {
                    item.IsDelete = true;
                    Update(item);
                }
            }

            category.IsDelete = true;


            Update(category);
        }

        public List<BlogCategory> GetAllBlogCategories()
        {
            return GetAll().ToList();
        }

        public BlogCategory GetBlogCategoryById(int id)
        {
            return GetById(id);
        }

        public void UpdateBlogCategroy(BlogCategory blogCategory, int id)
        {
            var category = GetBlogCategoryById(id);

            category.CategoryTitle = blogCategory.CategoryTitle;

            Update(category);


        }
    }
}
