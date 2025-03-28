using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // app.MapGet("/test", () => "Hello world!");
        [HttpGet("test")]
        public string GetHelloWorldText() => "Hello world!";
    }
}
