namespace Domain.Entities;

public class CartItem
{
    public Game Game { get; set; } = new();

    public int Quantity { get; set; }
}