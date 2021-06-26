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
    public class VideosSelectedCategoryRepository : GenericRepository<VideoSelectedCategory>, IVideosSelectedCategoryRepository
    {
        private readonly HoushimenContext _db;

        public VideosSelectedCategoryRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddCategoryToVideo(List<int> Categories, int VideoId)
        {
            foreach (var item in Categories)
            {
                Add(new VideoSelectedCategory()
                {

                    BlogCategoryId = item,
                    VideoId = VideoId

                });

            }
        }

        public void DeleteAllVideoCategory(int id)
        {
            GetAll().Where(p => p.VideoId == id).ToList()
                                      .ForEach(p => Delete(p));
        }

        public void DeleteSelectedVideoCategories(List<int> Categories, int VideoID)
        {
            var selectedcategories = GetAll().Where(p => p.VideoId == VideoID).ToList();

            foreach (var item in Categories)
            {

                var cat = GetAll().Where(p => p.VideoId == VideoID && p.BlogCategoryId == item).Single();

                Delete(cat);
            }
        }

        public bool IsVideoInCategory(int video, int categoriid)
        {
            return GetAll().Any(p => p.VideoId == video && p.BlogCategoryId == categoriid);
        }
    }
}
