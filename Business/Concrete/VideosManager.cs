using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class VideosManager:IVideosService
    {
        public IVideosRepository _videosRepository;

        public VideosManager()
        {
            _videosRepository = new VideosRepository();
        }

        public IResult<List<RelationUser>> GetAllVideos()
        {
            return _videosRepository.GetAllVideos();
        }

        public IResult<RelationUser> GetVideoById(int id)
        {
            return _videosRepository.GetVideoById(id);
        }

        public void DeleteVideoById(int id,int uid)
        {
            
            _videosRepository.DeleteVideoById(id,uid);
        }

        public IResult<Video> PostVideo(Video video)
        {
            video.Timestamp = (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return new IResult<Video>
            {
                Data= _videosRepository.PostVideo(video),
                Message="Video Successfully Uploaded",
                Success=true
            };
        }
    }
}
