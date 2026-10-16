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
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Create Order");
                Console.WriteLine("4. Add Line to Order");
                Console.WriteLine("5. Mark Order as Paid");
                Console.WriteLine("6. Print All Customers");
                Console.WriteLine("7. Print Products");
                Console.WriteLine("8. Print All Orders");
                Console.WriteLine("9. Print Order by ID");
                Console.WriteLine("10. Show Total Sales (Paid Orders Only)");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Enter your choice:");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        //add customer
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
                        //add product
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
                        //create order
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

                        while (_orderSystem.Orders.Exists(c => c.Id == orderId) || orderId <= 0 || !isOrderIdValid)
                        {
                            Console.WriteLine("Order with this ID already exists or is invalid. Please enter a different ID.");
                            isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);
                        }

                        _orderSystem.CreateOrder(orderId, selectedCustomer.Id, DateTime.Now);

                        Console.WriteLine("Order created successfully.");
                        break;

                    case 4:
                        //add line to order
                        _orderSystem.PrintAllOrders();

                        Console.WriteLine("Enter order ID: ");

                        isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);

                        while (!_orderSystem.Orders.Exists(c => c.Id == orderId) || orderId <= 0 || !isOrderIdValid)
                        {
                            Console.WriteLine("Order with this ID not found or is invalid. Please enter a different ID.");
                            isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);
                        }

                        _orderSystem.PrintProducts();
                        Console.WriteLine("Enter product ID to add to order: ");

                        bool isProductIdValid = int.TryParse(Console.ReadLine(), out productId);

                        while (!_orderSystem.Products.Exists(c => c.Id == productId) || productId <= 0 || !isProductIdValid)
                        {
                            Console.WriteLine("Product with this ID not found or is invalid. Please enter a different ID.");
                            isProductIdValid = int.TryParse(Console.ReadLine(), out productId);
                        }

                        Console.WriteLine("Enter quantity: ");

                        bool isQuantityValid = int.TryParse(Console.ReadLine(), out int quantity);

                        while (!isQuantityValid || quantity <= 0)
                        {
                            Console.WriteLine("Invalid quantity. Please enter a valid quantity (greater than 0):");
                            isQuantityValid = int.TryParse(Console.ReadLine(), out quantity);
                        }

                        try
                        {
                            _orderSystem.AddLineToOrder(orderId, productId, quantity);
                            Console.WriteLine("Order line added successfully.");

                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");

                        }
                            break;
                    case 5:
                        //mark order as paid
                        _orderSystem.PrintAllOrders();

                        Console.WriteLine("Enter order ID to mark as paid: ");

                        isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);

                        while (!_orderSystem.Orders.Exists(c => c.Id == orderId) || orderId <= 0 || !isOrderIdValid)
                        {
                            Console.WriteLine("Order with this ID not found or is invalid. Please enter a different ID.");
                            isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);
                        }

                        try
                        {
                            _orderSystem.MarkOrderPaid(orderId);
                            Console.WriteLine("Order marked as paid successfully.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }


                        break;
                    case 6:
                        _orderSystem.PrintAllCustomers();
                        break;
                    case 7:
                        _orderSystem.PrintProducts();
                        break;
                    case 8:
                        _orderSystem.PrintAllOrders();
                        break;
                    case 9:
                        Console.WriteLine("Enter order ID: ");
                        isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);
                        while (!_orderSystem.Orders.Exists(c => c.Id == orderId) || orderId <= 0 || !isOrderIdValid)
                        {
                            Console.WriteLine("Order with this ID not found or is invalid. Please enter a different ID.");
                            isOrderIdValid = int.TryParse(Console.ReadLine(), out orderId);
                        }
                        _orderSystem.PrintOrderById(orderId);
                        break;
                    case 10:
                        decimal result = _orderSystem.TotalSalesPaidOnly();
                        Console.WriteLine($"Total sales (paid only): {result}");
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
