using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.UnitOfWork
{
   public  interface IUnitOfWork  : IDisposable
    {




        void SaveChangesDB();
        Task<int> SaveChangesDBAsync();
    }
}
