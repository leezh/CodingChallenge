﻿using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// API regarding hotels
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        /// <summary>
        /// Retrieves and returns a combined list of hotels from all available
        /// sources.
        /// </summary>
        /// <returns>List of hotels</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Hotel>>> Get()
        {
            var sources = new List<IHotelSource>
            {
                new HotelApiSource("https://skizoominterviewchallenge.azurewebsites.net/api/hotels?code=7hnVjCOrnMYAA49pAy/sBnxf4OZZpv8j8fwQ4B8tSNyowaN4cfaKYQ=="),
                new ResidenzaApiSource("https://skizoominterviewchallenge.azurewebsites.net/api/residenza?code=En1OvN8w29jYh0BYv5ogeN2JVZt_zB8PZqTtpRK_PvB9AzFuTk3FYQ=="),
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