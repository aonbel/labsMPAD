namespace Domain.Entities;

public class Cart
{
    public virtual Dictionary<int, CartItem> CartGames { get; } = new();

    public virtual int Quantity => CartGames.Sum(cartGame => cartGame.Value.Quantity);

    public virtual decimal Price => CartGames.Sum(cartGame => cartGame.Value.Game.Price * cartGame.Value.Quantity);

    public virtual void AddToCart(Game game)
    {
        if (CartGames.TryGetValue(game.Id, out var cartGame))
        {
            cartGame.Quantity++;
            return;
        }

        CartGames.Add(game.Id, new CartItem
        {
            Game = game,
            Quantity = 1
        });
    }

    public virtual void RemoveFromCart(int gameId)
    {
        CartGames.Remove(gameId);
    }

    public virtual void ClearCart()
    {
        CartGames.Clear();
    }
}