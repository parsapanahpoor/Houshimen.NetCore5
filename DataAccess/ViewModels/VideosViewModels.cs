using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class AddCategoryToVideosViewModel
    {
        public AddCategoryToVideosViewModel()
        {
            SelectedVideoCategory = new List<VideosCategoriesViewModel>();
        }

        public int VideoId { get; set; }

        public List<VideosCategoriesViewModel> SelectedVideoCategory { get; set; }
    }

    public class VideosCategoriesViewModel
    {
        public int CategoryId { get; set; }
        public string CategoriName { get; set; }
        public bool IsSelected { get; set; }
    }
}
