using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CatchAll : Controller
    {
        [HttpGet]
        [Route("api", Order = 999)]
        public async Task<IActionResult> CatchAllApiGets()
        {
            return BadRequest("Alpaca API route was not found on this driver");
        }

        [HttpPut]
        [Route("api", Order = 999)]
        public async Task<IActionResult> CatchAllApiPuts()
        {
            return BadRequest("Alpaca API route was not found on this driver");
        }
    }
}