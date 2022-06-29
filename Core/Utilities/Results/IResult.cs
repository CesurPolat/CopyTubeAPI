
using Entities.DTOs;

namespace Core.Utilities.Results
{
    public class IResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }

        public bool Success { get; set; }

    }
}
