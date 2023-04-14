namespace elk_demo.Models;

public class Product
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}