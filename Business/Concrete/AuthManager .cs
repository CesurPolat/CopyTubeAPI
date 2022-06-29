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
        public AuthManager()
        {
            _authRepository = new AuthRepository();
        }

        public string Login(UserLoginDto user)
        {
            User userData = _authRepository.Login(user);
            if (userData.Id == 0) { return ""; };
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
            return tokenString;
        }

        public User Register(User user)
        {
            return _authRepository.Register(user);
        }
    }
}
