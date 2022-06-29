using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class AuthRepository:IAuthRepository
    {
        public User Login(UserLoginDto user)
        {
            using (var context = new CopytubeContext())
            {
                var result = from Users in context.Users where Users.Email == user.Email && Users.Password == user.Password select Users;
                if (result.Any())
                {
                    return result.First();
                }
                else
                {
                    return new User();
                }
            }
        }

        public User Register(User user)
        {
            using (var context = new CopytubeContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }
    }
}
