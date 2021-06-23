using DataAccess.Design_Pattern.GnericRepositories;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories
{
   public interface IUserRepository : IGernericRepository<User> 
    {

        bool IsExistUserName(string username);
        bool IsExistEmail(string email);
        bool IsExistPhoneNumber(string phonenumber);
        string GetUserByUserName(string username);


    }
}
