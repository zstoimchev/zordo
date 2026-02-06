using System.Net;

namespace zOrdo.Models.Models;

public class ZordoResult<T>
{
    public bool IsSuccessfull { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ZordoResult<T> CreateSuccess(T data, string? message = null)
    {
        IsSuccessfull = true;
        Result = data;
        Message = null;
        StatusCode = HttpStatusCode.OK;
        return this;
    }
    
    public ZordoResult<T> CreateBadRequest(string message = "Bad Request")
    {
        IsSuccessfull = false;
        Message = message;
        Result = default;
        StatusCode = HttpStatusCode.BadRequest;
        return this;
    }
    
    public ZordoResult<T> CreateNotFound(string message = "Not found")
    {
        IsSuccessfull = false;
        Message = message;
        Result = default;
        StatusCode = HttpStatusCode.NotFound;
        return this;
    }
    
    public ZordoResult<T> CreateConflict(string message = "Conflict")
    {
        IsSuccessfull = false;
        Message = message;
        Result = default;
        StatusCode = HttpStatusCode.Conflict;
        return this;
    }
}