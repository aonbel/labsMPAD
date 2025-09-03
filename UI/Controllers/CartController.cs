using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class CartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add(int gameId, string? returnUrl)
    {
        return View();
    }
}