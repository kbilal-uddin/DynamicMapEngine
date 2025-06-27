using DynamicMapEngine.Interfaces;
using DynamicMapEngine.Processor;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMapEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly IMapProcessor _mapProcessor;

        public MapController(IMapProcessor mapProcessor)
        {
            _mapProcessor = mapProcessor;
        }

        [HttpPost]
        public object Map([FromBody] object data, [FromQuery] string sourceType = "", [FromQuery] string targetType = "")
        {
            return new JsonResult(_mapProcessor.Map(data, sourceType, targetType));
        }
    }
}
