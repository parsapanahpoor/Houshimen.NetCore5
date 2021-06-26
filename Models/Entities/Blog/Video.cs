using Models.Entities.Blog;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities.Blog
{
    public  class Video
    {
        public Video()
        {

        }

        [Key]
        public int VideoId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Display(Name = "عنوان ویدیو ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string VideoTitle { get; set; }

        [Display(Name = "شرح کوتاه  ویدیو ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ShortDescription { get; set; }

        [Display(Name = "شرح کامل ویدیو ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LongDescription { get; set; }

        [MaxLength(50)]
        public string VideoImageName { get; set; }

        [MaxLength(50)]
        public string DemoFileName { get; set; }
        public string AparatFileName { get; set; }


        [MaxLength(600)]
        public string Tags { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آپارات")]
        public bool IsAparat { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }


        #region Relations

        public virtual List<VideoSelectedCategory> VideoSelectedCategory { get; set; }
        public virtual List<Comment.Comment> Comments { get; set; }
        public virtual Models.Entities.User.User Users { get; set; }

        #endregion


    }
}
