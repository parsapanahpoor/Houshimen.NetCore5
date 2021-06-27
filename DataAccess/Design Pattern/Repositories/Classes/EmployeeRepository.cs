using DataAccess.Design_Pattern.GnericRepositories;
using DataAccess.Design_Pattern.Repositories.Intrefaces;
using DataContext;
using Microsoft.AspNetCore.Http;
using Models.Entities.Employee;
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
   public  class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly HoushimenContext _db;

        public EmployeeRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public void AddEmployees(Employee employee, IFormFile imgBlogUp)
        {
            employee.UserAvatar = "no-photo.png";  //تصویر پیشفرض

            if (imgBlogUp != null && imgBlogUp.IsImage())
            {
                employee.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", employee.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

              


            }

            Add(employee);
        }

        public List<Employee> GetAllkEmployees()
        {
            return GetAll().ToList();
        }
    }
}
