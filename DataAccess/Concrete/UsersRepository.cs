using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
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

        public User GetUserByEmail(string email)
        {
            using (var context = new CopytubeContext())
            {
                var result = from Users in context.Users where Users.Email == email select Users;
                if (result.ToList().Count>0)
                {
                    return result.ToList().First();
                }
                return new User();
            }
        }

        public void UploadProfilePhoto(byte[] file, int uid)
        {
            using (var context = new CopytubeContext())
            {
                var userData = context.Users.Find(uid);
                userData.ProfilePhoto = file;
                context.Update(userData);
                context.SaveChanges();
            }
        }
    }
}
