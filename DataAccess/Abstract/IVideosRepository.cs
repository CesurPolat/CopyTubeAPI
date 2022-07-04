using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IVideosRepository
    {
        IResult<List<RelationUser<Video>>> GetAllVideos();
        IResult<RelationUser<Video>> GetVideoById(int id);
        void DeleteVideoById(int id,int uid);
        IResult<Video> PostVideo(Video video);
    }
}
