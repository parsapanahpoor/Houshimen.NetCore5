using DataAccess.Design_Pattern.Repositories.Classes;
using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Context
        private readonly HoushimenContext _db;

        public UnitOfWork(HoushimenContext db)
        {
            _db = db;
            userRepository = new UserRepository(_db);
            blogCategoryRepository = new BlogCategoryRepository(_db);
  
        }

        #endregion

        #region Repositories
        public UserRepository userRepository { get; private set; }
        public BlogCategoryRepository blogCategoryRepository { get; private set; }

        #endregion


        #region Implement

        public void SaveChangesDB()
        {
            _db.SaveChanges();
        }

        public Task<int> SaveChangesDBAsync()
        {
            return _db.SaveChangesAsync();
        }

        #endregion


        #region Dispose

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
