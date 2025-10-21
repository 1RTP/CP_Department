using System.Collections.Generic;

namespace MvcRazor
{

    internal sealed class Category
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public List<Product> Products { get; } = new List<Product>();

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
    internal sealed class Product
    {
        public Product(string name, decimal unitPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
        }
    
        public string Name { get; private set; }

        public decimal UnitPrice { get; private set; }
    }
}