using System;

namespace FinancialCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Financial Calculator!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Loan Payment Calculation");
                Console.WriteLine("2. Compound Interest Calculation");
                Console.WriteLine("3. Amortization Calculation");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice (1-4): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CalculateLoanPayment();
                        break;
                    case "2":
                        CalculateCompoundInterest();
                        break;
                    case "3":
                        CalculateAmortization();
                        break;
                    case "4":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                        break;
                }
            }
        }

        static void CalculateLoanPayment()
        {
            Console.WriteLine();
            Console.WriteLine("Loan Payment Calculation");

            Console.Write("Enter principal amount: ");
            double principal = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter annual interest rate (%): ");
            double annualRate = Convert.ToDouble(Console.ReadLine()) / 100;

            Console.Write("Enter loan period in years: ");
            int years = Convert.ToInt32(Console.ReadLine());

            int n = years * 12; // number of monthly payments
            double monthlyRate = annualRate / 12;
            double monthlyPayment = principal * monthlyRate / (1 - Math.Pow(1 + monthlyRate, -n));

            Console.WriteLine($"Monthly Payment: {monthlyPayment:C2}");
        }

        static void CalculateCompoundInterest()
        {
            Console.WriteLine();
            Console.WriteLine("Compound Interest Calculation");

            Console.Write("Enter principal amount: ");
            double principal = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter annual interest rate (%): ");
            double annualRate = Convert.ToDouble(Console.ReadLine()) / 100;

            Console.Write("Enter number of years: ");
            int years = Convert.ToInt32(Console.ReadLine());

            double amount = principal * Math.Pow(1 + annualRate, years);

            Console.WriteLine($"Amount after {years} years: {amount:C2}");
        }

        static void CalculateAmortization()
        {
            Console.WriteLine();
            Console.WriteLine("Amortization Calculation");

            Console.Write("Enter principal amount: ");
            double principal = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter annual interest rate (%): ");
            double annualRate = Convert.ToDouble(Console.ReadLine()) / 100;

            Console.Write("Enter number of years: ");
            int years = Convert.ToInt32(Console.ReadLine());

            int numberOfPayments = years * 12; // liczba miesięcznych płatności w ciągu okresu

            // Obliczenie miesięcznej stopy procentowej i miesięcznej płatności
            double monthlyRate = annualRate / 12;
            double monthlyPayment = principal * (monthlyRate * Math.Pow(1 + monthlyRate, numberOfPayments)) / (Math.Pow(1 + monthlyRate, numberOfPayments) - 1);

            // Obliczenie amortyzacji na podstawie miesięcznej płatności
            double totalAmortization = monthlyPayment * numberOfPayments;

            Console.WriteLine($"Monthly Payment: {monthlyPayment:C2}");
            Console.WriteLine($"Total Amortization: {totalAmortization:C2}");
        }

    }
}
