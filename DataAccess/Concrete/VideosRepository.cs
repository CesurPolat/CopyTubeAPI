using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class VideosRepository:IVideosRepository
    {
        public IResult<List<RelationUser<Video>>> GetAllVideos()
        {
            using (var context = new CopytubeContext())
            {
                var result=from Videos in context.Videos join Users in context.Users on Videos.Channel_id equals Users.Id select new RelationUser<Video>{ Id=Users.Id,Name=Users.Name,ProfilePhoto=Users.ProfilePhoto,Data=Videos };
                return new IResult<List<RelationUser<Video>>> { Data= result.ToList(), Message="Success",Success=true};
            }
        }

        public IResult<RelationUser<Video>> GetVideoById(int id)
        {
            using (var context = new CopytubeContext())
            {
                var videoData = context.Videos.Find(id);
                if (videoData == null) { return new IResult<RelationUser<Video>> { Message = "No Video", Success = false }; }
                videoData.View += 1;
                context.Update(videoData);
                context.SaveChanges();

                var result = from Videos in context.Videos join Users in context.Users on Videos.Channel_id equals Users.Id where Videos.Id == id select new RelationUser<Video> { Id = Users.Id, Name = Users.Name,ProfilePhoto=Users.ProfilePhoto, Data = Videos };
                return new IResult<RelationUser<Video>> { Data = result.ToList().First<RelationUser<Video>>(), Message = "Success", Success = true };

            }
        }

        //TODO Turn into IResult
        public void DeleteVideoById(int id,int uid)
        {
            using (var context = new CopytubeContext())
            {
                var video = context.Videos.Find(id);
                if (video.Channel_id == uid)
                {
                    context.Videos.Remove(video);
                    context.SaveChanges();
                }
            }
        }

        public IResult<Video> PostVideo(Video video)
        {
            using (var context = new CopytubeContext())
            {
                context.Videos.Add(video);
                context.SaveChanges();
                return new IResult<Video> { Data=video,Message="Video Successfully Posted",Success=true};
            }
        }
    }
}
