using System;

class Program
{
    static void Main(string[] args)
    {
        // Customer 1 (USA)
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Customer customer1 = new Customer("John Doe", address1);

        Product product1 = new Product("Laptop", "P001", 1200, 1);
        Product product2 = new Product("Mouse", "P002", 25, 2);

        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        // Customer 2 (International)
        Address address2 = new Address("456 Market Rd", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Jane Smith", address2);

        Product product3 = new Product("Headphones", "P003", 100, 1);
        Product product4 = new Product("Keyboard", "P004", 75, 1);

        Order order2 = new Order(customer2);
        order2.AddProduct(product3);
        order2.AddProduct(product4);

        // Display results
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalCost()}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalCost()}");
    }
}
