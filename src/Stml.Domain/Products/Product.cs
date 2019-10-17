using Stml.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Products
{
    public class Product : IAggregateRoot
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
