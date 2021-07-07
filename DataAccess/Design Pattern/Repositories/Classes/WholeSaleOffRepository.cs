using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Models.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class WholeSaleOffRepository : GenericRepository<WholeSaleOff>, IWholeSaleOffRepository
    {
        private readonly HoushimenContext _db;

        public WholeSaleOffRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddWholeSaleOff(int productid, int count, int percent)
        {
            WholeSaleOff whole = new WholeSaleOff()
            { 
                ProductID = productid,
                WholeSaleCount = count ,
                WholeSaleOffPercent = percent

            
            
            };

            Add(whole);
        }
    }
}
