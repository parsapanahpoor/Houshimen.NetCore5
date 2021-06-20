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
    public   class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly HoushimenContext _db;

        public BlogRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

    }
}
