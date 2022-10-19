using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Api funcionando");
    }
}
