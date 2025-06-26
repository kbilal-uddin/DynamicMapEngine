using DynamicMapEngine.Processor;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMapEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        [HttpPost]
        public object Map([FromBody] object data, [FromQuery] string sourceType = "", [FromQuery] string targetType = "")
        {
            return new JsonResult(new MapProcessor().Map(data, sourceType, targetType));
        }
    }
}
