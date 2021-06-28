using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Blog;
using Models.Entities.Employee;
using Models.Entities.Slider;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class HoushimenContext : IdentityDbContext<User>
    {
        public HoushimenContext(DbContextOptions<HoushimenContext> options)
        : base(options)
        {

        }

        #region Blog
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogSelectedCategory> BlogSelectedCategories { get; set; }
        public DbSet<VideoSelectedCategory> VideoSelectedCategory { get; set; }


        #endregion

        #region Comment

        public DbSet<Models.Entities.Comment.Comment> Comment { get; set; }
        public DbSet<Models.Entites.Comment.ProductType> ProductType { get; set; }


        #endregion

        #region Slider

        public DbSet<Slider> Slider { get; set; }

        #endregion

        #region Employee

        public DbSet<Employee> Employee { get; set; }

        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;



            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<BlogCategory>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<User>()
             .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Blog>()
             .HasQueryFilter(u => !u.IsDelete);


            modelBuilder.Entity<BlogCategory>()
             .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Employee>()
             .HasQueryFilter(u => !u.IsDelete);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
