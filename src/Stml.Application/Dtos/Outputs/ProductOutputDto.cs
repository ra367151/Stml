using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Outputs
{
    public class ProductOutputDto
    {
        public ProductOutputDto(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
