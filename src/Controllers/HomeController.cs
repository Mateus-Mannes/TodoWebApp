using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("TodoApp Api");
    }
}
