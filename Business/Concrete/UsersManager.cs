using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UsersManager:IUsersService
    {
        public IUsersRepository _usersRepository;

        public UsersManager()
        {
            _usersRepository = new UsersRepository();
        }

        public List<User> GetAllUsers()
        {
            return _usersRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _usersRepository.GetUserById(id);
        }

        public void UploadProfilePhoto(IFormFile file, int uid)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
                _usersRepository.UploadProfilePhoto(fileBytes, uid);
            }

        }
    }
}
