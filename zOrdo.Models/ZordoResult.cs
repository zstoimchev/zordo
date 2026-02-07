using System.Net;

namespace zOrdo.Models;

public class ZordoResult<T>
{
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ZordoResult<T> CreateSuccess(T data, string? message = null)
    {
        IsSuccessful = true;
        Result = data;
        Message = null;
        StatusCode = HttpStatusCode.OK;
        return this;
    }
    
    public ZordoResult<T> CreateBadRequest(string message = "Bad Request")
    {
        IsSuccessful = false;
        Message = message;
        Result = default;
        StatusCode = HttpStatusCode.BadRequest;
        return this;
    }
    
    public ZordoResult<T> CreateNotFound(string message = "Not found")
    {
        IsSuccessful = false;
        Message = message;
        Result = default;
        StatusCode = HttpStatusCode.NotFound;
        return this;
    }
    
    public ZordoResult<T> CreateConflict(string message = "Conflict")
    {
        IsSuccessful = false;
        Message = message;
        Result = default;
        StatusCode = HttpStatusCode.Conflict;
        return this;
    }
}