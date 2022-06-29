using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UsersRepository:IUsersRepository
    {
        public List<User> GetAllUsers()
        {
            using (var context = new CopytubeContext())
            {
                return context.Users.ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var context = new CopytubeContext())
            {
                return context.Users.Find(id);
            }
        }
    }
}
