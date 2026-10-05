using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public bool IsVip { get; set; }

        public decimal GetDiscountRate()
        {
            if (IsVip)
            {
                return 0.1m;
            }
            else
            {
                return 0.00m;
            }
        }
    }
}
