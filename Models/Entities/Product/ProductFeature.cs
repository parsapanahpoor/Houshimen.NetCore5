using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Product
{
    public class ProductFeature
    {
        public ProductFeature()
        {

        }

        [Key]
        public int FeatureID { get; set; }


        public int ProductID { get; set; }

        [Display(Name = "عنوان ")]
        [Required]
        public string FeatureTitle { get; set; }

        [Display(Name = "مقدار ")]
        [Required]
        public string FeatureValue { get; set; }

        #region Relations

        public virtual Product Product { get; set; }

        #endregion
    }
}
