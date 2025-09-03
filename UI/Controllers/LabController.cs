using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Controllers;

public class LabController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}