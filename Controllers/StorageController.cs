using Microsoft.AspNetCore.Mvc;

public class StorageController : BaseController
{
    private DataContext _dataContext;

    public StorageController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet("SetString/{value}")]
    public void SetString(string value)
    {
        _dataContext.Info = value;
    }

    [HttpGet("GetString")]
    public string GetString()
    {
        return _dataContext.Info;
    }
}
