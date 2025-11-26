using Microsoft.AspNetCore.Mvc;
using UI.Extensions;
using UI.Models;

namespace UI.Components;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.Get<Cart>("cart");

        if (cart is null) return View(new CartViewModel());

        var cartViewModel = new CartViewModel
        {
            TotalAmount = cart.Price,
            ItemsCount = cart.Quantity
        };

        return View(cartViewModel);
    }
}