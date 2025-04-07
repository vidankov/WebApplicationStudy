using Microsoft.AspNetCore.Mvc;

public class TestController : BaseController
{
    // app.MapGet("/test", () => "Hello world!");
    [HttpGet("test")]
    public string GetHelloWorldText() => "Hello world!";
}
