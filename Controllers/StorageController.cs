using Microsoft.AspNetCore.Mvc;

public class StorageController : BaseController
{
    [HttpGet("SetString/{value}")]
    public void SetString(string value)
    {
        DataContext.Str = value;
    }

    [HttpGet("GetString")]
    public string GetString()
    {
        return DataContext.Str;
    }
}
