using UI.Extensions;

namespace UI.Services.CartService;

public class CartService(IHttpContextAccessor httpContextAccessor) : Cart
{
    public override Dictionary<int, CartItem> CartGames =>
        httpContextAccessor.HttpContext!.Session.Get<Cart>("cart")?.CartGames ?? new Dictionary<int, CartItem>();

    public override int Quantity => httpContextAccessor.HttpContext!.Session.Get<Cart>("cart")?.Quantity ?? 0;

    public override decimal Price => httpContextAccessor.HttpContext!.Session.Get<Cart>("cart")?.Price ?? 0;

    public override void AddToCart(Game game)
    {
        var cart = httpContextAccessor.HttpContext!.Session.Get<Cart>("cart") ?? new Cart();

        cart.AddToCart(game);

        httpContextAccessor.HttpContext!.Session.Set("cart", cart);
    }

    public override void ClearCart()
    {
        var cart = httpContextAccessor.HttpContext!.Session.Get<Cart>("cart") ?? new Cart();

        cart.ClearCart();

        httpContextAccessor.HttpContext!.Session.Set("cart", cart);
    }

    public override void RemoveFromCart(int gameId)
    {
        var cart = httpContextAccessor.HttpContext!.Session.Get<Cart>("cart")!;

        cart.RemoveFromCart(gameId);

        httpContextAccessor.HttpContext!.Session.Set("cart", cart);
    }
}