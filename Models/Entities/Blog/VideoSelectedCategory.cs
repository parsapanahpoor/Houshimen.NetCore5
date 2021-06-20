using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities.Blog
{
   public  class VideoSelectedCategory
    {


        public VideoSelectedCategory()
        {

        }

        [Key]
        public int VideoSelectedCategoryId { get; set; }

        public int BlogCategoryId { get; set; }
        public int VideoId { get; set; }

        #region Relations

        public virtual BlogCategory BlogCategory { get; set; }
        public virtual Video Video { get; set; }
        #endregion


    }
}
