using Microsoft.AspNetCore.Mvc;

namespace WEB_353502_Belavusau.Controllers;

public class FirstLabController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}