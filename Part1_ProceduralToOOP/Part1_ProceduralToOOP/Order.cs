using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class Order
    {

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; private set; }
        public  List<OrderLine> OrderLines { get; private set; }

        public Order(int id, Customer customer, DateTime date)
        {
            Id = id;
            Customer = customer;
            Date = date;
            IsPaid = false;
            OrderLines = new List<OrderLine>();
        }


        public void AddLineToOrder(OrderLine ol)
        {
            if (IsPaid)
            {
                throw new InvalidOperationException("Cannot add line to a paid order.");
            }


            ol.Product.ReduceStock(ol.Quantity);

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

        public void PrintOrder()
        {
            Console.WriteLine($"Order ID: {Id}");
            Console.WriteLine($"Customer: {Customer.Name}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Is Paid: {IsPaid}");
            Console.WriteLine($"Order Lines:");

            foreach (var line in OrderLines)
            {
                Console.WriteLine($"  - {line.Product.Name}: {line.Quantity} x ${line.Product.Price:F2} = ${line.LineTotal:F2}");
            }

            Console.WriteLine($"Total: ${CalculateTotal():F2}");
        }
    }
}
