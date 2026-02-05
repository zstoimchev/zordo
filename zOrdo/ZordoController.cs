using Microsoft.AspNetCore.Mvc;

namespace zOrdo;

public class ZordoController : ControllerBase
{
    protected ActionResult<T> OkOrConflict<T>(T item)
    {
        return item != null ? Ok(item) : Conflict();
    }

    protected ActionResult<T> OkOrNotFound<T>(T item)
    {
        return item != null ? Ok(item) : Conflict();
    }
}