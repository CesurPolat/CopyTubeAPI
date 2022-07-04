using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentsManager:ICommentsService
    {
        public ICommentsRepository commentsRepository;
        public CommentsManager()
        {
            commentsRepository = new CommentsRepository();
        }

        public IResult<Comment> PostComment(CommentDto comment,int uid)
        {
            var commentData = new Comment { 
                Context=comment.Context,
                Channel_id=uid,
                Timestamp= (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                Video_id=comment.Video_id,
                Reply_id=comment.Reply_id,
            };
            commentData = commentsRepository.PostComment(commentData);
            return new IResult<Comment>{Data=commentData,Message="Successfully Comment Posted",Success=true };
        }

        public IResult<List<RelationUser<Comment>>> GetCommentsByVideoId(int id)
        {
            var commentsData=commentsRepository.GetCommentsByVideoId(id);
            if(commentsData.Count==0)
            {
                return new IResult<List<RelationUser<Comment>>> { Message="There aren't comment in this video",Success=true };
            }
            else
            {
                return new IResult<List<RelationUser<Comment>>> { Data=commentsData,Message = "There are comment in this video", Success = true };
            }
        }
    }
}
