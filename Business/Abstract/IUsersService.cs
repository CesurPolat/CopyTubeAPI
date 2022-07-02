using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUsersService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void UploadProfilePhoto(IFormFile file,int uid);
    }
}
