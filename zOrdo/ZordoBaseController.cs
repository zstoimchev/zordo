using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;

namespace zOrdo;

public class ZordoBaseController : ControllerBase
{
    protected ActionResult OkOrConflict<T>(ZordoResult<T> itemResult)
    {
        return itemResult.Success
            ? Ok(itemResult)
            : Conflict(itemResult.Message);
    }

    protected ActionResult OkOrNotFound<T>(ZordoResult<T> itemResult)
    {
        return itemResult.Success
            ? Ok(itemResult)
            : NotFound(itemResult.Message);
    }
}