using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entites.Comment
{
    public  class ProductType
    {
        public ProductType()
        {
                
        }


        [Key]
        public int ProductTypeId { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProductTypeTitle { get; set; }


        #region Relations




        #endregion
    }
}
