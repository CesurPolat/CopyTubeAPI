using Entities.DTOs;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IVideosRepository
    {
        IResult<List<RelationUser>> GetAllVideos();
        IResult<RelationUser> GetVideoById(int id);
        void DeleteVideoById(int id,int uid);
        Video PostVideo(Video video);
    }
}
