using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Microsoft.AspNetCore.Http;
using Models.Entities.Slider;
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
    public class SliderRepository : GenericRepository<Slider>, ISldierRepository
    {
        private readonly HoushimenContext _db;

        public SliderRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddSlider(Slider slider, IFormFile imgBlogUp)
        {
            slider.BlogImageName = "no-photo.png";  //تصویر پیشفرض



            if (imgBlogUp != null && imgBlogUp.IsImage())
            {
                slider.BlogImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", slider.BlogImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider/thumb", slider.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            Add(slider);

        }

        public void DeleteSldier(Slider slider)
        {

            if (slider.BlogImageName != "no-photo.png")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", slider.BlogImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider/Thumb", slider.BlogImageName);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }
            }
            Delete(slider);

        }

        public List<Slider> GetAllSliders()
        {
            return GetAll().ToList();
        }

        public Slider GetSldierForShow()
        {
            return GetFirstOrDefault();
        }

        public Slider GetSliderById(int id)
        {
            return GetById(id);
        }

        public void UpdateSlider(Slider slider, IFormFile imgBlogUp)
        {
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {

                if (slider.BlogImageName != "no-photo.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", slider.BlogImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider/Thumb", slider.BlogImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }



                slider.BlogImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", slider.BlogImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider/Thumb", slider.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            Update(slider);
        }
    }
}
