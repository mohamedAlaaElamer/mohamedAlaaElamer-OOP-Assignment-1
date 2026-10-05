using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        public void AddLineToOrder(OrderLine ol)
        {
            if (IsPaid)
            {
                throw new InvalidOperationException("Cannot add line to a paid order.");
            }

            OrderLines.Add(ol);
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (var line in OrderLines)
            {
                total += line.LineTotal;
            }

            decimal discountRate = Customer.GetDiscountRate();

            total -= total * discountRate;

            return total;
        }

        public void MarkAsPaid()
        {
            if (OrderLines == null || OrderLines.Count == 0)
            {
                throw new InvalidOperationException("Cannot mark an order as paid with no order lines.");
            }

            IsPaid = true;
        }
    }
}
