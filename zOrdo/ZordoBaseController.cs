using Microsoft.AspNetCore.Mvc;
using zOrdo.Models;
using zOrdo.Models.Models;

namespace zOrdo;

public class ZordoBaseController : ControllerBase
{
    protected ActionResult<T> MapToActionResult<T>(ZordoResult<T> zordoResult)
    {
        if (zordoResult.IsSuccessfull)
            return Ok(zordoResult.Result);

        if (string.IsNullOrEmpty(zordoResult.Message))
            return StatusCode((int)zordoResult.StatusCode);

        return StatusCode((int)zordoResult.StatusCode, new ErrorResult { Message = zordoResult.Message });
    }
}