
using Entities.DTOs;

namespace Core.Utilities.Results
{
    public class IResult<T>
    {

        public List<T> Data { get; set; }
        public string Message { get; set; }

    }
}
