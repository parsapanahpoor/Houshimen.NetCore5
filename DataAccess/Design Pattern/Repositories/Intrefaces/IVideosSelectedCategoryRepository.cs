using DataAccess.Design_Pattern.GnericRepositories;
using Models.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
   public  interface IVideosSelectedCategoryRepository : IGernericRepository<VideoSelectedCategory>
    {
         bool IsVideoInCategory(int video, int categoriid);
        public void AddCategoryToVideo(List<int> Categories, int VideoId);
        public void DeleteSelectedVideoCategories(List<int> Categories, int VideoID);
        public void DeleteAllVideoCategory(int id);

    }
}
