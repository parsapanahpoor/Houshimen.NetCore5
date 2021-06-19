using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public  class HoushimenContext : IdentityDbContext<User>
    {
        public HoushimenContext(DbContextOptions<HoushimenContext> options)
        : base(options)
        {

        }




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




            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
