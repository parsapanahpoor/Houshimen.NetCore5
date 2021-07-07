using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Product
{
    public class Product
    {


        public Product()
        {

        }

        [Key]
        public int ProductID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductCode { get; set; }

        [Required]
        public String UserId { get; set; }

        [Required]
        public String UserName { get; set; }

        [Display(Name = "عنوان محصول ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProductTitle { get; set; }

        [Display(Name = "شرح کوتاه   ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ShortDescription { get; set; }

        [Display(Name = "شرح کامل  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LongDescription { get; set; }

        [MaxLength(50)]
        public string ProductImageName { get; set; }

        public int? OfferPercent { get; set; }
        public bool? IsInOffer { get; set; }

        [Display(Name = "تعداد موجودی    ")]
        [Required]
        public int ProductCount { get; set; }
        [Display(Name = "قیمت     ")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "قیمت قبلی      ")]
        public decimal? OldPrice { get; set; }

        [Display(Name = "تگ ها   ")]
        public string Tags { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }



        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        [Required]
        public bool IsGroup { get; set; }

        [Display(Name = "تگ ها   ")]
        public string GroupDescription { get; set; }




        #region Relations

        public virtual List<Comment.Comment> Comments { get; set; }
        public virtual List<WholeSaleOff> WholeSaleOff { get; set; }
        public virtual User.User Users { get; set; }

        #endregion
    }
}
