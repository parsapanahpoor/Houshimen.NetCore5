using DataAccess.Design_Pattern.GnericRepositories;
using DataContext;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.Repositories.Classes
{
     public  class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly HoushimenContext _db;

        public UserRepository(HoushimenContext db) : base(db)
        {
            _db = db;
        }

        public string GetUserByUserName(string username)
        {
            return GetAll(p=>p.UserName == username).Select(p=>p.Id).First();
        }

        public bool IsExistEmail(string email)
        {
            return GetAll().Any(p => p.Email == email);

        }

        public bool IsExistPhoneNumber(string phonenumber)
        {
            return GetAll().Any(p => p.PhoneNumber == phonenumber);
        }

        public bool IsExistUserName(string username)
        {
            return GetAll().Any(p => p.UserName == username);
        }
    }
}
