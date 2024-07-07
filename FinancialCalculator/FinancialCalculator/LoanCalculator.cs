using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCalculator
{
    public class LoanCalculator
    {
        public static void CalculateLoanPayment()
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
    }
}
