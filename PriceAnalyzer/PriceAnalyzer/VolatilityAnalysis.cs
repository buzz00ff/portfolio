using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceAnalyzer
{
    public class VolatilityAnalysis
    {
        public static double CalculateStandardDeviation(List<double> prices)
        {
            if (prices.Count == 0)
            {
                throw new ArgumentException("Price list cannot be empty.");
            }

            double sum = 0;
            foreach (var price in prices)
            {
                sum += price;
            }
            double mean = sum / prices.Count;

            double squaredDifferencesSum = 0;
            foreach (var price in prices)
            {
                squaredDifferencesSum += Math.Pow(price - mean, 2);
            }

            double meanSquaredDifferences = squaredDifferencesSum / prices.Count;

            double standardDeviation = Math.Sqrt(meanSquaredDifferences);

            return standardDeviation;

        }

        public static void AnalyzeVolatility(List<List<string>> filteredData, int period)
        {
            List<double> prices = new List<double>();

            foreach (var data in filteredData)
            {
                if (data.Count > 1)
                {
                    if (double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                    {
                        prices.Add(price);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid price data: {string.Join(", ", data)}");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid data format: {string.Join(", ", data)}");
                }
            }

            if (prices.Count < period)
            {
                Console.WriteLine("Not enough data for the specified period.");
                return;
            }

            double volatility = CalculateStandardDeviation(prices);
            Console.WriteLine($"Volatility for the period: {volatility:F2}");

            double highRiskThreshold = 1400.0;
            double lowRiskThreshold = 700.0;

            if (volatility > highRiskThreshold)
            {
                Console.WriteLine("High volatility detected. Consider the risk before making a decision.");
            }
            else if (volatility < lowRiskThreshold)
            {
                Console.WriteLine("Low volatility detected. It might be a good time to consider buying.");
            }
            else
            {
                Console.WriteLine("Moderate volatility detected. Evaluate your investment strategy carefully.");
            }
        }
    }
}
