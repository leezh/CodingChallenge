using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sources = new List<IHotelSource>
            {
                new HotelApiSource(),
                new ResidenzaApiSource(),
            };
            var tasks = sources.Select(t => t.GetHotels());
            await Task.WhenAll(tasks.ToArray());

            var result = new List<Hotel>();
            foreach (var task in tasks) {
                result.AddRange(task.Result);
            }

            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("italian")]
        public async Task<IActionResult> GetNewProviderHotels()
        {
            var residenzaSource = new ResidenzaApiSource();
            var content = await residenzaSource.GetHotels();

            return new OkObjectResult(content);
        }
    }
}