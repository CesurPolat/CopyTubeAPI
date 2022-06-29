using Business.Abstract;
using Business.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopyTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        public IVideosService videosService;

        public VideosController()
        {
            videosService = new VideosManager();
        }

        [HttpGet]
        public IActionResult GetAllVideos()
        {
            return Ok(videosService.GetAllVideos());
        }

        [HttpGet("{id}")]
        public IActionResult VideoById(int id)
        {
            return Ok(videosService.GetVideoById(id));
        }

        /// <summary>
        /// Token Required
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteVideoById(int id)
        {
            videosService.DeleteVideoById(id, int.Parse(User.Claims.First(x => x.Type.IndexOf("nameidentifier") != -1).Value));
            return Ok();
        }

        /// <summary>
        /// Token Required
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult PostVideo([FromBody] Video body)
        {
            //TODO Like View Gibi Değerlere Açık Kapı
            var videoData = body;
            videoData.Channel_id = int.Parse(User.Claims.First(x => x.Type.IndexOf("nameidentifier") != -1).Value);
            return Ok(videosService.PostVideo(videoData));
        }
    }
}
