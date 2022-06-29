using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.DTOs;
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
    }
}
