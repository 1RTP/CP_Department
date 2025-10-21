namespace FastReport.Blazor.Demo.Models
{
    internal sealed class Category
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public List<Product> Products { get; } = new List<Product>();

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    internal sealed class Product
    {
        public string Name { get; init; }

        public decimal UnitPrice { get; init; }

        public Product(string name, decimal unitPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
        }
    }

}
