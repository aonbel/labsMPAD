using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_353502_Belavusau.Controllers;

public class LabController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}