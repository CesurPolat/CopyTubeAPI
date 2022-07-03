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
    public class CommentsController : ControllerBase
    {
        public ICommentsService commentsService;
        public CommentsController()
        {
            commentsService = new CommentsManager();
        }

        /// <summary>
        /// Token Required
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult PostComment([FromBody] CommentDto body)
        {
            var result = commentsService.PostComment(body, int.Parse(User.Claims.First(x => x.Type.IndexOf("nameidentifier") != -1).Value));
            return Ok(result);
        }

        [HttpGet("commentsByVideoId/{id}")]
        public IActionResult CommentsByVideoId(int id)
        {
            return Ok(commentsService.GetCommentsByVideoId(id));
        }
    }
}
