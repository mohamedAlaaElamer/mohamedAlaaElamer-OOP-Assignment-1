using System;
using System.Collections.Generic;
using System.Text;

namespace Part1_ProceduralToOOP
{
    public class OrderSystem
    {
        public List<Customer> Customers = new List<Customer>();
        public List<Order> Orders = new List<Order>();
        public List<Product> Products = new List<Product>();

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void CreateOrder(int orderId, int customerId, DateTime date)
        {
            Customer? customer = Customers.Find(c => c.Id == customerId);
            if (customer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            Order order = new Order(orderId, customer, date);
            Orders.Add(order);
        }

        public void AddLineToOrder(int orderId, int productId, int quantity)
        {
            Order? order = Orders.Find(o => o.Id == orderId);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            Product? product = Products.Find(p => p.Id == productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            OrderLine orderLine = new OrderLine(product, quantity);

            order.AddLineToOrder(orderLine);
        }

        public void MarkOrderPaid(int orderId)
        {
            Order? order = Orders.Find(o => o.Id == orderId);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            order.MarkAsPaid();
        }

        public decimal TotalSalesPaidOnly()
        {
            decimal totalSales = 0;

            foreach (var order in Orders)
            {
                if (order.IsPaid)
                {
                    totalSales += order.CalculateTotal();
                }
            }

            return totalSales;
        }

        public void PrintAllCustomers()
        {
            foreach (var customer in Customers)
            {
                Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, City: {customer.City}, VIP: {customer.IsVip}");
            }
        }

        public void PrintProducts()
        {
            foreach (var product in Products)
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
            }
        }

        public void PrintAllOrders()
        {
            foreach(var order in Orders)
            {
                order.PrintOrder();
            }
        }

        public void PrintOrderById(int orderId)
        {
            Order? order = Orders.Find(o => o.Id == orderId);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            order.PrintOrder();
        }
    }
}
