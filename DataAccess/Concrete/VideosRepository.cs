using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class VideosRepository:IVideosRepository
    {
        public IResult<List<Video>> GetAllVideos()
        {
            using (var context = new CopytubeContext())
            {
                var result=from Videos in context.Videos join Users in context.Users on Videos.Channel_id equals Users.Id select new { Videos,Channel=Users };
                return new IResult<List<Video>> { Data= context.Videos.ToList() ,Message="Success"};
            }
        }

        public Video GetVideoById(int id)
        {
            using (var context = new CopytubeContext())
            {
                var videoData = context.Videos.Find(id);
                videoData.View += 1;
                context.Update(videoData);
                context.SaveChanges();
                return videoData;
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

        public Video PostVideo(Video video)
        {
            using (var context = new CopytubeContext())
            {
                context.Videos.Add(video);
                context.SaveChanges();
                return video;
            }
        }
    }
}
