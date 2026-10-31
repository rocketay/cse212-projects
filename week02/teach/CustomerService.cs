using System;
using System.Collections.Generic;

/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var service = new CustomerService(10);
        // Console.WriteLine(service);

        // Test Cases
        // NOTE: AddNewCustomer reads from the console, so type the name,
        // account id and problem every time the program asks for them.

        // Test 1
        // Requirements covered: enqueue a customer, dequeue and display it
        // Scenario: Can I add one customer and then serve the customer?
        // Expected Result: This should display the customer that was added
        Console.WriteLine("Test 1");
        var service = new CustomerService(4);
        service.AddNewCustomer();
        service.ServeCustomer();
        // Defect(s) Found: ServeCustomer must save the customer BEFORE deleting it from the list

        Console.WriteLine("=================");

        // Test 2
        // Requirements covered: FIFO order when serving customers
        // Scenario: Can I add two customers and then serve the customers in the right order?
        // Expected Result: This should display the customers in the same order that they were entered
        Console.WriteLine("Test 2");
        service = new CustomerService(4);
        service.AddNewCustomer();
        service.AddNewCustomer();
        Console.WriteLine($"Before serving customers: {service}");
        service.ServeCustomer();
        service.ServeCustomer();
        Console.WriteLine($"After serving customers: {service}");
        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 3
        // Requirements covered: error message when the queue is empty
        // Scenario: Can I serve a customer if there is no customer?
        // Expected Result: This should display an error message
        Console.WriteLine("Test 3");
        service = new CustomerService(4);
        service.ServeCustomer();
        // Defect(s) Found: ServeCustomer must check the queue length and display an error message

        Console.WriteLine("=================");

        // Test 4
        // Requirements covered: error message when the queue is full
        // Scenario: Does the max queue size get enforced?
        // Expected Result: This should display an error message when the 5th one is added
        Console.WriteLine("Test 4");
        service = new CustomerService(4);
        service.AddNewCustomer();
        service.AddNewCustomer();
        service.AddNewCustomer();
        service.AddNewCustomer();
        service.AddNewCustomer();
        Console.WriteLine($"Service Queue: {service}");
        // Defect(s) Found: AddNewCustomer must use >= instead of > when checking the max size

        Console.WriteLine("=================");

        // Test 5
        // Requirements covered: invalid size (<= 0) defaults to 10
        // Scenario: Does the max size get defaulted to 10 if an invalid value is provided?
        // Expected Result: Both lines should display max_size=10
        Console.WriteLine("Test 5");
        service = new CustomerService(0);
        Console.WriteLine($"Size should be 10: {service}");
        service = new CustomerService(-3);
        Console.WriteLine($"Size should be 10: {service}");
        // Defect(s) Found: None
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        // Requirement: an invalid size (<= 0) defaults to 10
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId}): {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue.
        // Fixed: use >= (not >) so the queue never holds more than _maxSize customers
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the back of the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // Fixed: check that there are customers in the queue first
        if (_queue.Count <= 0) {
            Console.WriteLine("No Customers in the queue");
        }
        else {
            // Fixed: read and save the customer BEFORE removing it from the queue
            var customer = _queue[0];
            _queue.RemoveAt(0);
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}