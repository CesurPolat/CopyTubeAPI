using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        public List<Comment> GetCommentsByVideoId(int id)
        {
            using(var context = new CopytubeContext())
            {
                return context.Comments.Where(c=> c.Video_id==id).ToList();
            }
        }
    }
}
