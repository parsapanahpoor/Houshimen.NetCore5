using Models.Entites.Comment;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities.Comment
{
    public  class Comment
    {
        public Comment()
        {
                
        }

        [Key]
        public int CommentId { get; set; }
        public int ProductTypeId { get; set; }
        public int UserId { get; set; }

        public int? ParentId { get; set; }
        public int? VideoId { get; set; }
        public int? BlogId { get; set; }
        public int? ProductID { get; set; }

        [MaxLength(700)]
        public string CommentBody { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<Comment> comments { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Models.Entities.User.User Users { get; set; }

        public virtual Models.Entities.Blog.Video Video { get; set; }

        public virtual Models.Entities.Blog.Blog Blog { get; set; }


        #endregion

    }
}
