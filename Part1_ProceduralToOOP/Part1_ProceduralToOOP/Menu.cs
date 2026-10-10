using System;
using System.Collections.Generic;
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
                        break;
                    case 3:
                        
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
