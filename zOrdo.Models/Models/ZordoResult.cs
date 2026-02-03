using System.Net;

namespace zOrdo.Models.Models;

public class ZordoResult<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ZordoResult<T> CreateSuccess(T data, string? message = null)
    {
        Success = true;
        Data = data;
        Message = null;
        StatusCode = HttpStatusCode.OK;
        return this;
    }
    
    public ZordoResult<T> CreateBadRequest(string message = "Bad Request")
    {
        Success = false;
        Message = message;
        Data = default;
        StatusCode = HttpStatusCode.BadRequest;
        return this;
    }
    
    public ZordoResult<T> CreateNotFound(string message = "Not found")
    {
        Success = false;
        Message = message;
        Data = default;
        StatusCode = HttpStatusCode.NotFound;
        return this;
    }
    
    public ZordoResult<T> CreateConflict(string message = "Conflict")
    {
        Success = false;
        Message = message;
        Data = default;
        StatusCode = HttpStatusCode.Conflict;
        return this;
    }
}