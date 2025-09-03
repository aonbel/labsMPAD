using Microsoft.AspNetCore.Mvc;
using WEB_353502_Belavusau.Models;

namespace WEB_353502_Belavusau.Components;

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