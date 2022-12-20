using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jala.Custom.ModelBinder.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ModelBinderController : ControllerBase
{
    [HttpGet]
    public IActionResult Index([FromHeader(Name = "User-Agent")]string useragent)
    {
        return Ok(useragent);
    }

    [HttpGet]
    public IActionResult GetId([FromQuery] int id)
    {
        return Ok(id);
    }

    [HttpPost]
    public IActionResult Create(Page page)
    {
        return Ok(page);
    }
}

public class Page
{
    public string? Name { get; set; }
    public int Id { get; set; }
}