using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Part1_ProceduralToOOP
{
    public class Menu
    {
        private OrderSystem _orderSystem;

        public Menu(OrderSystem orderSystem)
        {
            _orderSystem = orderSystem;
        }
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("6. PrintAllCustomers");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Enter your choice:");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter customer id:");
                        bool isValidId = int.TryParse(Console.ReadLine(), out int customerId);

                        while (_orderSystem.Customers.Exists(c => c.Id == customerId) || customerId <= 0 || !isValidId)
                        {
                            Console.WriteLine("Customer with this ID already exists or is invalid. Please enter a different ID.");
                            isValidId = int.TryParse(Console.ReadLine(), out customerId);
                        }
                        Console.WriteLine("Enter customer name:");
                        string? customerName = Console.ReadLine();

                        while (string.IsNullOrWhiteSpace(customerName))
                        {
                            Console.WriteLine("Customer name cannot be empty. Please enter a valid name.");
                            customerName = Console.ReadLine();
                        }

                        Console.WriteLine("Enter customer email:");
                        string? customerEmail = Console.ReadLine();

                        while (string.IsNullOrWhiteSpace(customerEmail) ||
                               !Regex.IsMatch(customerEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                        {
                            Console.WriteLine("Invalid email format. Please enter a valid email:");
                            customerEmail = Console.ReadLine();
                        }

                        Console.WriteLine("Enter customer address:");
                        string? customerAddress = Console.ReadLine();

                        while (string.IsNullOrWhiteSpace(customerAddress))
                        {
                            Console.WriteLine("Customer address cannot be empty. Please enter a valid address.");
                            customerAddress = Console.ReadLine();
                        }

                        Console.WriteLine("Enter customer isVIP (true/false):");

                        bool isValidInput = bool.TryParse(Console.ReadLine(), out bool isVIP);

                        while (!isValidInput)
                        {
                            Console.WriteLine("Invalid input. Please enter 'true' or 'false' for isVIP:");
                            isValidInput = bool.TryParse(Console.ReadLine(), out isVIP);
                        }

                        _orderSystem.AddCustomer(new Customer(customerId, customerName, customerEmail, customerAddress, isVIP));

                        break;

                    case 2:
                        Console.WriteLine("Enter product id:");
                        bool isPValidId = int.TryParse(Console.ReadLine(), out int productId);

                        while (_orderSystem.Products.Exists(p => p.Id == productId) || productId <= 0 || !isPValidId)
                        {
                            Console.WriteLine("Product with this ID already exists or is invalid. Please enter a different ID.");
                            isPValidId = int.TryParse(Console.ReadLine(), out productId);
                        }

                        Console.WriteLine("Enter product name:");
                        string? productName = Console.ReadLine();

                        while (string.IsNullOrWhiteSpace(productName))
                        {
                            Console.WriteLine("Product name cannot be empty. Please enter a valid name.");
                            productName = Console.ReadLine();
                        }

                        Console.WriteLine("Enter product price:");
                        bool isPriceValid = decimal.TryParse(Console.ReadLine(), out decimal productPrice);

                        while (!isPriceValid || productPrice <= 0)
                        {
                            Console.WriteLine("Invalid price. Please enter a valid price greater than zero:");
                            isPriceValid = decimal.TryParse(Console.ReadLine(), out productPrice);
                        }

                        Console.WriteLine("Enter product stock:");
                        bool isStockValid = int.TryParse(Console.ReadLine(), out int productStock);

                        while (!isStockValid || productStock <= 0)
                        {
                            Console.WriteLine("Invalid stock. Please enter a valid stock (greater than 0):");
                            isStockValid = int.TryParse(Console.ReadLine(), out productStock);
                        }

                        _orderSystem.AddProduct(new Product(productId, productName, productPrice, productStock));
                        break;
                    case 3:
                        _orderSystem.PrintAllCustomers();
                        Console.WriteLine("Choose a customer to create order :");

                        int.TryParse(Console.ReadLine(), out int selectedCustomerId);

                        Customer? selectedCustomer = _orderSystem.Customers.Find(c => c.Id == selectedCustomerId);

                        while (selectedCustomer == null)
                        {
                            Console.WriteLine("Invalid customer ID. Please enter a valid customer ID:");
                            int.TryParse(Console.ReadLine(), out selectedCustomerId);
                            selectedCustomer = _orderSystem.Customers.Find(c => c.Id == selectedCustomerId);
                        }
                        Console.WriteLine("Enter order ID: ");

                        bool isOrderIdValid = int.TryParse(Console.ReadLine(), out int orderId);

                        _orderSystem.Orders.Exists(c => c.Id == orderId);

                        while (_orderSystem.Orders.Exists(c => c.Id == orderId) || orderId <= 0 || !isOrderIdValid)
                        {
                            Console.WriteLine("Order with this ID already exists or is invalid. Please enter a different ID.");
                            isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);
                        }

                        _orderSystem.CreateOrder(orderId, selectedCustomer.Id, DateTime.Now);

                        Console.WriteLine("Order created successfully.");
                        break;
                    case 6:
                        _orderSystem.PrintAllCustomers();
                        break;
                    case 0:
                        return; 
                    default:    
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

    }
}
