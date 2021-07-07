using DataAccess.Design_Pattern.GnericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
   public interface IProductReposiotry :IGernericRepository<Product>
    {

        List<Product> GetAllProducts();
        int  AddProduct(Product product, IFormFile imageProduct);
    }
}
