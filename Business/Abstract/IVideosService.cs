using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVideosService
    {
        IResult<List<RelationUser<Video>>> GetAllVideos();
        IResult<RelationUser<Video>> GetVideoById(int id);
        void DeleteVideoById(int id,int uid);
        IResult<Video> PostVideo(Video video);
    }
}
