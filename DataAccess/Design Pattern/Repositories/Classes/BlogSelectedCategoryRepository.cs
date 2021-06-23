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
    public class BlogSelectedCategoryRepository : GenericRepository<BlogSelectedCategory> , IBlogSelectedCategoryRepository
    {
        private readonly HoushimenContext _db;

        public BlogSelectedCategoryRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddCategoryToBlog(List<int> Categories, int BlogId)
        {
            foreach (var item in Categories)
            {
                Add(new BlogSelectedCategory()
                {

                    BlogCategoryId = item,
                    BlogId = BlogId

                });
               
            }
        }

        public void DeleteAllBlogCategory(int id)
        {
            GetAll().Where(p => p.BlogId == id).ToList()
                            .ForEach(p => Delete(p));
        }

        public void DeleteSelectedBlogCategories(List<int> Categories, int BlogId)
        {
            var selectedcategories = GetAll().Where(p => p.BlogId == BlogId).ToList();

            foreach (var item in Categories)
            {

                 var cat =   GetAll().Where(p => p.BlogId == BlogId && p.BlogCategoryId == item).Single();

                 Delete(cat);
            }

        }

        public List<BlogSelectedCategory> GetAllBlogSelectedCategory()
        {
            return GetAll().ToList();
        }

        public bool IsBlogInCategory(int blogid, int categoriid)
        {
            return GetAll().Any(p => p.BlogId == blogid && p.BlogCategoryId == categoriid);
        }
    }
}
