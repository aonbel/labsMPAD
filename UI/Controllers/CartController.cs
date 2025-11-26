using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class CartController(IGameService gameService, Cart cart) : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(cart);
    }

    public async Task<IActionResult> AddAsync(int gameId, string? returnUrl)
    {
        var getByIdResponse = await gameService.GetByIdAsync(gameId);

        if (!getByIdResponse.Successful) return BadRequest(getByIdResponse.ErrorMessage);

        var game = getByIdResponse.Data!;

        cart.AddToCart(game);

        return LocalRedirect(returnUrl ?? "/");
    }

    public IActionResult Remove(int gameId, string? returnUrl)
    {
        cart.RemoveFromCart(gameId);

        return LocalRedirect(returnUrl ?? "/");
    }

    public IActionResult Clear(string? returnUrl)
    {
        cart.ClearCart();

        return LocalRedirect(returnUrl ?? "/");
    }
}