using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUsersRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
    }
}
