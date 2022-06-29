using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult<string> Login(UserLoginDto user);
        IResult<string> Register(UserRegisterDto user);
    }
}
