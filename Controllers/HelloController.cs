using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        //app.MapGet("/welcome/{name}", (string name) => $"Hello {name}!");
        [HttpGet("hello/{name}")]
        public string GetGreetingByName(string name) => $"Hello {name}!";
    }
}
