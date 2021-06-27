using DataAccess.Design_Pattern.GnericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
    public interface ISldierRepository : IGernericRepository<Slider>
    {

        List<Slider> GetAllSliders();
        void AddSlider(Slider slider, IFormFile imgBlogUp);
        Slider GetSliderById(int id);
        void UpdateSlider(Slider slider, IFormFile imgBlogUp);
        void DeleteSldier(Slider slider);
        Slider GetSldierForShow();

    }
}
