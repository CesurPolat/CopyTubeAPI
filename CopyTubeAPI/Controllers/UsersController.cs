using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopyTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUsersService usersService;

        public UsersController()
        {
            usersService = new UsersManager();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(usersService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult UserById(int id)
        {
            return Ok(usersService.GetUserById(id));
        }

        [HttpPut("uploadProfilePhoto")]
        public IActionResult ProfilePhoto([FromForm] List<IFormFile> file, [FromForm] int userId)
        {
            usersService.UploadProfilePhoto(file[0],userId);
            return Ok(file);
        }
    }
}
