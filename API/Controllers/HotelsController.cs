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
            var HTTP = new HttpClient();

            var data_response = await HTTP.GetAsync("https://skizoominterviewchallenge.azurewebsites.net/api/residenza?code=En1OvN8w29jYh0BYv5ogeN2JVZt_zB8PZqTtpRK_PvB9AzFuTk3FYQ==");

            var content = await data_response.Content.ReadAsStringAsync();

            return new OkObjectResult(content);
        }
    }
}