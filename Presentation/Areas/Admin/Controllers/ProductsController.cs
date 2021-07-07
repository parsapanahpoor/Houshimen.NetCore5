using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _context;
        public ProductsController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false, bool Offer = false)
        {
            ViewBag.Create = Create;
            ViewBag.Offer = Offer;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_context.productRepository.GetAllProducts() );
        }

        public ActionResult Create()
        {

            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);
            ViewData["UserName"] = User.Identity.Name;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IFormFile imageProduct, int? WholeSaleCount, int? WholeSalePercent)
        {
            if (ModelState.IsValid)
            {

                product.ProductCode = Guid.NewGuid().ToString();

               int productid =  _context.productRepository.AddProduct(product,imageProduct);

                if (WholeSaleCount != null && WholeSalePercent != null)
                {
                    _context.wholeSaleOffRepository.AddWholeSaleOff(productid, (int)WholeSaleCount , (int)WholeSalePercent);
                }


                _context.SaveChangesDB();


                return Redirect("/Admin/Products/Index?Create=true");
            }

            ViewData["UserId"] = _context.userRepository.GetUserByUserName(User.Identity.Name);
            ViewData["UseeName"] = User.Identity.Name;

            return View(product);
        }

    }
}
