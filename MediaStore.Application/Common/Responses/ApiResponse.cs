using MediaStore.Application.StaticDetails;
using System.Net;

namespace MediaStore.Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } = true;
        public int StatusCode { get; set; } = StatusCodes.Ok;
        public List<string> ErrorMessages { get; set; } = new();
        public T? Data { get; set; }
    }
}
