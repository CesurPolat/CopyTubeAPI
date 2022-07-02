using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private IAuthRepository _authRepository;
        private IUsersRepository _usersRepository;
        public AuthManager()
        {
            _authRepository = new AuthRepository();
            _usersRepository = new UsersRepository();
        }

        public IResult<string> Login(UserLoginDto user)
        {
            User userData = _usersRepository.GetUserByEmail(user.Email);

            //User userData = _authRepository.Login(user);
            if (userData.Id == 0)
            {
                return new IResult<string> { Message = "Wrong Email or Password", Success = false };
            };
            

            using (var hcmac = new System.Security.Cryptography.HMACSHA512(userData.PasswordSalt))
            {
                var computedHash = hcmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userData.PasswordHash[i])
                    {
                        return new IResult<string> { Data = "", Message = "Wrong Email or Password", Success = false };
                    }
                }

            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userData.Id.ToString()),
            };

            //Config den veri çek
            var token = new JwtSecurityToken(
                issuer: "http://localhost/",
                audience: "http://localhost/",
                expires: DateTime.Now.AddDays(1),
                notBefore: DateTime.Now,
                claims:claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecretkeycesurpolat")),
                    SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new IResult<string> { Data= tokenString ,Message="Success",Success=true};
        }

        public IResult<String> Register(UserRegisterDto user)
        {
            if (_usersRepository.GetUserByEmail(user.Email).Id>0)
            {
                return new IResult<String> { Message = "User exist!",Success=false };
            }

            byte[] passwordHash, passwordSalt;
            using (var hcmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hcmac.Key;
                passwordHash = hcmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            }

            var userData = new User
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _authRepository.Register(userData);
            string tokenString = Login(new UserLoginDto{Email= user.Email,Password=user.Password }).Data.ToString();

            return new IResult<String>{Data= tokenString, Message="Success Account Created",Success=true};
        }
    }
}
