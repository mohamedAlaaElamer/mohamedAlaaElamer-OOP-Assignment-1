using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class OrderLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal LineTotal => Product.Price * Quantity;

        public OrderLine(Product product, int quantity)
        {
            Product = product;

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity = quantity;
        }

        
    }
}
