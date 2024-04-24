using System.Collections;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API.Tests
{
    public class HotelsControllerUnitTest
    {
        [Fact]
        public async void Get()
        {
            var controller = new HotelsController();

            var response = await controller.Get();

            var result = Assert.IsType<OkObjectResult>(response.Result);
            var hotels = Assert.IsAssignableFrom<IEnumerable>(result.Value);
            Assert.NotNull(hotels);
            Assert.NotEmpty(hotels);
        }
    }
}
