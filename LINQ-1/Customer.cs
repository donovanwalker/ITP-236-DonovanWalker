using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_1
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        // Modified OrderTotal to calculate the sum of all SalesOrder OrderTotal values
        public double OrderTotal
        {
            get
            {
                return SalesOrders.Sum(order => order.OrderTotal);
            }
        }

        // Modified BackOrdered to calculate the sum of (Quantity - Shipped) for all SalesOrders
        public int BackOrdered
        {
            get
            {
                return SalesOrders.Sum(order => order.Quantity - order.Shipped);
            }
        }

        public List<SalesOrder> SalesOrders { get; set; }

        public Customer()
        {
            SalesOrders = new List<SalesOrder>();
        }
    }

    public class SalesOrder
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public int Quantity { get; set; }
        public int Shipped { get; set; }
    }

    public static class CustomerData
    {
        public static List<Customer> Customers
        {
            get
            {
                return new List<Customer>()
                {
                    new Customer() {
                        CustomerId = 1,
                        Name = "Skip Wythe",
                        Region = "Central Virginia",
                        City = "Richmond",
                        State = "VA",
                        SalesOrders = new List<SalesOrder>()
                        {
                            new SalesOrder() {
                                OrderId = 101,
                                CustomerId = 1,
                                OrderDate = new DateTime(2025,01,02),
                                OrderTotal = 125,
                                Quantity = 5,
                                Shipped = 5
                            },
                            new SalesOrder() {
                                OrderId = 102,
                                CustomerId = 1,
                                OrderDate = new DateTime(2025,01,05),
                                OrderTotal = 2125,
                                Quantity = 17,
                                Shipped = 17
                            },
                            new SalesOrder() {
                                OrderId = 105,
                                CustomerId = 1,
                                OrderDate = new DateTime(2025,02,12),
                                OrderTotal = 1133,
                                Quantity = 21,
                                Shipped = 21
                            }
                        }
                    },
                    new Customer() {
                        CustomerId = 2,
                        Name = "James River",
                        Region = "Tidewater",
                        City = "Williamsburg",
                        State = "VA",
                        SalesOrders = new List<SalesOrder>()
                        {
                            new SalesOrder() {
                                OrderId = 103,
                                CustomerId = 2,
                                OrderDate = new DateTime(2024,12,14),
                                OrderTotal = 377,
                                Quantity = 15,
                                Shipped = 13
                            },
                            new SalesOrder() {
                                OrderId = 104,
                                CustomerId = 2,
                                OrderDate = new DateTime(2024,01,07),
                                OrderTotal = 1833,
                                Quantity = 14,
                                Shipped = 14
                            },
                            new SalesOrder() {
                                OrderId = 107,
                                CustomerId = 2,
                                OrderDate = new DateTime(2025,02,11),
                                OrderTotal = 2024,
                                Quantity = 31,
                                Shipped = 23
                            },
                            new SalesOrder() {
                                OrderId = 109,
                                CustomerId = 2,
                                OrderDate = new DateTime(2025,02,11),
                                OrderTotal = 3480,
                                Quantity = 13,
                                Shipped = 11
                            }
                        }
                    },
                    new Customer() {
                        CustomerId = 3,
                        Name = "Maggie Walker",
                        Region = "Tidewater",
                        City = "Williamsburg",
                        State = "VA",
                        SalesOrders = new List<SalesOrder>()
                        {
                            new SalesOrder() {
                                OrderId = 108,
                                CustomerId = 3,
                                OrderDate = new DateTime(2024,12,11),
                                OrderTotal = 1830,
                                Quantity = 10,
                                Shipped = 10
                            },
                            new SalesOrder() {
                                OrderId = 111,
                                CustomerId = 3,
                                OrderDate = new DateTime(2025,02,15),
                                OrderTotal = 4130,
                                Quantity = 38,
                                Shipped = 0
                            }
                        }
                    }
                };
            }
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            // Display customer information
            foreach (var customer in CustomerData.Customers)
            {
                Display(customer);
            }

            // Calculate the average size order for all customers
            var allOrderTotals = CustomerData.Customers.SelectMany(c => c.SalesOrders)
                                                        .Select(o => o.OrderTotal)
                                                        .ToList();
            double averageOrderTotal = allOrderTotals.Average();

            // Find the customer with the highest OrderTotal
            var highestOrderTotalCustomer = CustomerData.Customers
                .OrderByDescending(c => c.OrderTotal)
                .FirstOrDefault();

            // Display the overall average order total and the customer with the highest order total
            Console.WriteLine($"Overall Average Order Total: {averageOrderTotal:C}");
            Console.WriteLine($"Customer with Highest Order Total: {highestOrderTotalCustomer?.Name} - {highestOrderTotalCustomer?.OrderTotal:C}");
        }

        // Method to display customer details
        public static void Display(Customer customer)
        {
            // Calculate the average order size by OrderTotal for this customer
            double averageOrderSize = customer.SalesOrders.Average(o => o.OrderTotal);

            // Output customer details
            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Total Order Amount: {customer.OrderTotal:C}");
            Console.WriteLine($"BackOrdered Quantity: {customer.BackOrdered}");
            Console.WriteLine($"Average Order Size (by Order Total): {averageOrderSize:C}");
            Console.WriteLine();
        }
    }
}
