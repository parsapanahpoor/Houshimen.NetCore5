using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Product
{
    public class ProductSelectedCategory
    {

        public ProductSelectedCategory()
        {

        }

        [Key]
        public int ProductSelectedCategoryID { get; set; }

        public int ProductCategoryId { get; set; }
        public int ProductID { get; set; }
        public int? ProductCategoriesProductCategoryId { get; set; }

        #region Relations

        public virtual ProductCategories ProductCategories { get; set; }
        public virtual Product Product { get; set; }
        #endregion

    }
}
