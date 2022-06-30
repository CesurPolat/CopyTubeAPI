using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVideosService
    {
        IResult<List<RelationUser>> GetAllVideos();
        IResult<RelationUser> GetVideoById(int id);
        void DeleteVideoById(int id,int uid);
        Video PostVideo(Video video);
    }
}
