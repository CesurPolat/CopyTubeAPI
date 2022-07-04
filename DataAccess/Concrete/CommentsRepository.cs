using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CommentsRepository:ICommentsRepository
    {
        public Comment PostComment(Comment comment)
        {
            using (var context = new CopytubeContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
                return comment;
            }
        }

        public List<RelationUser<Comment>> GetCommentsByVideoId(int id)
        {
            using(var context = new CopytubeContext())
            {
                var result=from Comments in context.Comments where Comments.Video_id == id join Users in context.Users on Comments.Channel_id equals Users.Id select new RelationUser<Comment> { Id=Users.Id,Name=Users.Name,ProfilePhoto=Users.ProfilePhoto,Data=Comments};
                return result.ToList();
            }
        }
    }
}
