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
                        LoanCalculator.CalculateLoanPayment();
                        break;
                    case "2":
                        CompoundInterestCalculator.CalculateCompoundInterest();
                        break;
                    case "3":
                        AmortizationCalculator.CalculateAmortization();
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
    }
}
