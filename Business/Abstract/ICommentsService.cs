using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentsService
    {
        IResult<Comment> PostComment(CommentDto comment,int userId);
        IResult<List<RelationUser<Comment>>> GetCommentsByVideoId(int id);
    }
}
