using System;

namespace OperatorOverloadingDemo
{
    // Employee class that demonstrates operator overloading with validation
    public class Employee
    {
        private int _id;
        private string _firstName;
        private string _lastName;

        // Properties with validation
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Employee ID must be a positive number.");
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty.");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty.");
                _lastName = value;
            }
        }

        // Constructor with validation
        public Employee(int id, string firstName, string lastName)
        {
            // Property setters will handle validation
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        // Format employee details for display
        public override string ToString()
        {
            return $"Employee(ID: {Id}, Full Name: {FirstName} {LastName})";
        }

        // Overload equality operator (==) with detailed comparison
        public static bool operator ==(Employee emp1, Employee emp2)
        {
            // Handle null reference cases
            if (ReferenceEquals(emp1, null))
            {
                Console.WriteLine("First employee is null");
                return ReferenceEquals(emp2, null);
            }
            if (ReferenceEquals(emp2, null))
            {
                Console.WriteLine("Second employee is null");
                return false;
            }

            // Compare IDs and provide detailed output
            bool areEqual = emp1.Id == emp2.Id;
            if (areEqual)
                Console.WriteLine($"Employees are equal (Same ID: {emp1.Id})");
            else
                Console.WriteLine($"Employees are different (IDs: {emp1.Id} vs {emp2.Id})");

            return areEqual;
        }

        // Overload inequality operator (!=)
        public static bool operator !=(Employee emp1, Employee emp2)
        {
            return !(emp1 == emp2);
        }

        // Override Equals method for consistency
        public override bool Equals(object obj)
        {
            if (obj is Employee employee)
                return this == employee;
            return false;
        }

        // Override GetHashCode for consistency
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Creating and Comparing Employees:\n");

                // Create employee objects with validation
                Employee employee1 = new Employee(101, "Sarah", "Wilson");
                Employee employee2 = new Employee(101, "Michael", "Brown");
                Employee employee3 = new Employee(102, "Emma", "Davis");

                // Display employee information
                Console.WriteLine("Employee Details:");
                Console.WriteLine(employee1);
                Console.WriteLine(employee2);
                Console.WriteLine(employee3);
                Console.WriteLine();

                // Demonstrate operator overloading with detailed output
                Console.WriteLine("Comparison Results:");
                Console.WriteLine("Comparing employee1 and employee2:");
                bool result1 = employee1 == employee2;
                Console.WriteLine($"Result: {result1}\n");

                Console.WriteLine("Comparing employee1 and employee3:");
                bool result2 = employee1 != employee3;
                Console.WriteLine($"Result: {result2}");

                // Demonstrate validation
                Console.WriteLine("\nTrying to create invalid employee:");
                Employee invalidEmployee = new Employee(-1, "Invalid", "Employee");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }

            // Wait for user input
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}