using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Components;

public class CartViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cartViewModel = new CartViewModel()
        {
            TotalAmount = 5.23m,
            ItemsCount = 3
        };
        
        return View(cartViewModel);
    }
}