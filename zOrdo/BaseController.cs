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
}