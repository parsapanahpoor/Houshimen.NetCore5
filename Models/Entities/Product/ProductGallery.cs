using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Product
{
    public class ProductGallery
    {
        public ProductGallery()
        {

        }

        [Key]
        public int ProductGalleryId { get; set; }


        public int ProductID { get; set; }

        [Display(Name = "عنوان ")]
        [Required]
        public string Title { get; set; }

        [Required]
        public string ImageName { get; set; }

        #region Relations

        public virtual Product Product { get; set; }

        #endregion


    }
}
