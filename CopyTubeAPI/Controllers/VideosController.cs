using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
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
        public IWebHostEnvironment _environment;

        public VideosController(IWebHostEnvironment environment)
        {
            videosService = new VideosManager();
            _environment = environment;
            
        }

        [HttpGet]
        public IActionResult GetAllVideos()
        {
            return Ok(videosService.GetAllVideos());
        }

        [HttpGet("{id}")]
        public IActionResult VideoById(int id)
        {
            var result = videosService.GetVideoById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
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
        public IActionResult PostVideo([FromForm] VideoDto body, [FromForm] List<IFormFile> thumbnail, [FromForm] List<IFormFile> video)
        {
            var videoFolder = _environment.WebRootPath + "\\Videos\\";
            var videoUrl = new Random().Next()+ "." + video[0].FileName.Split('.')[^1];
            byte[] fileBytes;
            try
            {
                if (video.Count > 0)
                {
                    if (!Directory.Exists(videoFolder))
                    {
                        Directory.CreateDirectory(videoFolder);
                    }
                    using (FileStream fileStream = System.IO.File.Create(videoFolder + videoUrl ))
                    {
                        video[0].CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
            }
            catch
            {
                return BadRequest(new IResult<string>{Message= "Couldn't Upload Video",Success=false });
            }

            using (var ms = new MemoryStream())
            {
                thumbnail[0].CopyTo(ms);
                fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
            }

            var videoData = new Video
            {
                Title=body.Title,
                Description=body.Description,
                Thumbnail= fileBytes,
                VideoUrl = videoUrl,
            };
            videoData.Channel_id = int.Parse(User.Claims.First(x => x.Type.IndexOf("nameidentifier") != -1).Value);
            var resp = videosService.PostVideo(videoData);
            if (resp.Success)
            {
                return Ok(resp);
            }
            else
            {
                System.IO.File.Delete(videoFolder + videoUrl);
                return BadRequest(resp);
            }
        }
    }
}
