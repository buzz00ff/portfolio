using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCalculator
{
    public class CompoundInterestCalculator
    {
        public static void CalculateCompoundInterest()
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
    }
}
