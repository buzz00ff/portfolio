using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCalculator
{
    public class AmortizationCalculator
    {
        public static void CalculateAmortization()
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
