using DataAccess.Design_Pattern.GnericRepositories;
using Microsoft.AspNetCore.Http;
using Models.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Intrefaces
{
    public  interface IEmployeeRepository : IGernericRepository<Employee>
    {

        List<Models.Entities.Employee.Employee> GetAllkEmployees();
        void AddEmployees(Models.Entities.Employee.Employee employee, IFormFile imgBlogUp);


    }
}
