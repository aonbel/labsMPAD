using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        
        return View();
    }
}