using Microsoft.AspNetCore.Mvc;

public class HelloController : BaseController
{
    //app.MapGet("/welcome/{name}", (string name) => $"Hello {name}!");
    [HttpGet("hello/{name}")]
    public string GetGreetingByName(string name) => $"Hello {name}!";
}
