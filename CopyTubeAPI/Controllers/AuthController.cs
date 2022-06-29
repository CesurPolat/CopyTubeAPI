using Business.Abstract;
using Business.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopyTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController()
        {
            _authService = new AuthManager();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto body)
        {
            var result = _authService.Login(body);
            if (result == "")
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }

        }

        //TODO Return Token
        [HttpPost("register")]
        public User Register([FromBody] User body)
        {
            return _authService.Register(body);
        }
    }
}
