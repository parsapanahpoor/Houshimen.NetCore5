using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Microsoft.AspNetCore.Http;
using Models.Entities.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;
using Utilities.Security;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductReposiotry
    {
        private readonly HoushimenContext _db;

        public ProductRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public int AddProduct(Product product, IFormFile imageProduct)
        {
            product.CreateDate = DateTime.Now;
            product.IsActive = true;

            if (imageProduct != null && imageProduct.IsImage())
            {
                product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imageProduct.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/MainImage", product.ProductImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageProduct.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/MainImage/thumb", product.ProductImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);

                ImageConvertor imgResizer400 = new ImageConvertor();
                string thumbPath400 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/MainImage/400px", product.ProductImageName);

                imgResizer.Image_resize(imagePath, thumbPath400, 400);
            }

            Add(product);

            return product.ProductID;

        }

        public List<Product> GetAllProducts()
        {
            return GetAll().OrderByDescending(p=>p.ProductID)
                                    .ToList();
        }
    }
}
