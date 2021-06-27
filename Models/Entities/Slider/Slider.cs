using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities.Slider
{
    public class Slider
    {
        public Slider()
        {

        }

        [Key]
        public int SliderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }


        [Display(Name = " متن اول ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstText { get; set; }

        [Display(Name = " متن دوم ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string SecondeText { get; set; }

        [Display(Name = " متن سوم ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ThirdText { get; set; }

        [MaxLength(350)]
        public string Link { get; set; }


        [MaxLength(50)]
        public string BlogImageName { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }


        #region Relations


        public virtual User.User Users { get; set; }

        #endregion

    }
}
