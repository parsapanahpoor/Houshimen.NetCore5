using DataAccess.Design_Pattern.GnericRepositories;
using Models.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
    public interface IBlogRepository : IGernericRepository<Blog>
    {
    }
}
