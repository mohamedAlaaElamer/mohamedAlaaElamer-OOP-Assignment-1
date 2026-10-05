using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }


        public void ReduceStock(int quantity)
        {
            if(quantity <= Stock)
            {
                Stock -= quantity;
            }
            else
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

        }
    }
}
