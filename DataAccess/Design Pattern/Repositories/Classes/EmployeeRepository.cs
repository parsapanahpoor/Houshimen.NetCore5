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

        public void DeleteEmployee(Employee employee)
        {
            employee.IsDelete = true;
            if (employee.UserAvatar != "no-photo.png")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", employee.UserAvatar);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }


            }

            Update(employee);
        }

        public List<Employee> GetAllkEmployees()
        {
            return GetAll().ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return GetById(id);
        }

        public void UpdateEmployee(Employee employee, IFormFile imgBlogUp)
        {
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {

                if (employee.UserAvatar != "no-photo.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", employee.UserAvatar);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                   
                }



                employee.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", employee.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

            
            }


            Update(employee);
        }

    }
}
