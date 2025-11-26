using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class LabController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}