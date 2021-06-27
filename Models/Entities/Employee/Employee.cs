using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities.Employee
{
    public   class Employee
    {
        public Employee()
        {

        }


        [Key]
        public int EmployeeId { get; set; }

        [Display(Name = "نام شخص ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "سمت شخص در شرکت  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EmployeeLevel  { get; set; }

        [Display(Name = "توضیح درباره ی شخص   ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description  { get; set; }

        [Display(Name = "لینک شبکه های اجتماعی 1  ")]
        public string SocialMedia1  { get; set; }

        [Display(Name = "لینک شبمه های اجتماعی 2    ")]
        public string SocialMedia2 { get; set; }

        [Display(Name = "لینک شخص     ")]
        public string PersonalLink { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserAvatar { get; set; }

        public bool IsDelete { get; set; }


    }
}
