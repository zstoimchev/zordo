using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using zOrdo.Models;

namespace zOrdo;

public class BaseController : ControllerBase
{
    protected ActionResult<T> MapToActionResult<T>(ZordoResult<T> zordoResult)
    {
        if (zordoResult.IsSuccessful)
            return Ok(zordoResult.Result);

        if (string.IsNullOrEmpty(zordoResult.Message))
            return StatusCode((int)zordoResult.StatusCode);

        return StatusCode((int)zordoResult.StatusCode, new ErrorResult { Message = zordoResult.Message });
    }

    protected string GetUserEmail()
    {
        return User.FindFirst(ClaimTypes.Email)?.Value
               ?? throw new UnauthorizedAccessException("User email claim not found.");
    }

    protected int GetUserId()
    {
        return int.Parse(
            User.FindFirst("userId")?.Value
            ?? throw new UnauthorizedAccessException("User ID claim not found.")
        );
    }
}