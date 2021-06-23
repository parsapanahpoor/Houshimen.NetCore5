using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class AddCategoryToBlogViewModel
    {
        public AddCategoryToBlogViewModel()
        {
            SelectedBlogCategory = new List<BlogCategoriesViewModel>();
        }

        public int  BlogId { get; set; }

        public List<BlogCategoriesViewModel> SelectedBlogCategory { get; set; }
    }

    public class BlogCategoriesViewModel
    {
        public int CategoryId { get; set; }
        public string CategoriName { get; set; }
        public bool IsSelected { get; set; }
    }
}
