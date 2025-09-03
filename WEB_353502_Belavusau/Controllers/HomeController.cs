using Microsoft.AspNetCore.Mvc;

namespace WEB_353502_Belavusau.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        
        return View();
    }
}