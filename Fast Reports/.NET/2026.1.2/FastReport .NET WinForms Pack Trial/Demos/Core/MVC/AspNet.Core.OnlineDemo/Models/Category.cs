namespace AspNet.Demo.Models;

internal sealed class Category(string name, string description)
{
    public string Name { get; init; } = name;

    public string Description { get; init; } = description;

    public List<Product> Products { get; } = new();
}

internal sealed class Product(string name, decimal unitPrice)
{
    public string Name { get; init; } = name;

    public decimal UnitPrice { get; init; } = unitPrice;
}