using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceAnalyzer
{
    class SimpleMovingAverage
    {
        public static List<double> CalculateSMA(List<double> prices, int period)
        {
            List<double> sma = new List<double>();

            if (period <= 0 || prices.Count < period)
            {
                throw new ArgumentException("Invalid period or insufficient price data.");
            }

            for (int i = 0; i <= prices.Count - period; i++)
            {
                double sum = 0;
                for (int j = 0; j < period; j++)
                {
                    sum += prices[i + j];
                }
                sma.Add(sum / period);
            }

            return sma;
        }

        public static List<double> CalculateEMA(List<double> prices, int period)
        {
            List<double> ema = new List<double>();
            double multiplier = 2.0 / (period + 1);

            if (period <= 0 || prices.Count < period)
            {
                throw new ArgumentException("Invalid period or insufficient price data.");
            }

            double sma = 0;
            for (int i = 0; i < period; i++)
            {
                sma += prices[i];
            }
            sma /= period;
            ema.Add(sma);

            for (int i = period; i < prices.Count; i++)
            {
                double currentEma = ((prices[i] - ema[ema.Count - 1]) * multiplier) + ema[ema.Count - 1];
                ema.Add(currentEma);
            }

            return ema;
        }

        public static void AnalyzeMovingAverageCrossover(List<double> prices, int shortPeriod, int longPeriod)
        {
            List<double> shortSMA = CalculateSMA(prices, shortPeriod);
            List<double> longSMA = CalculateSMA(prices, longPeriod);

            for (int i = 1; i < Math.Min(shortSMA.Count, longSMA.Count); i++)
            {
                if (shortSMA[i - 1] < longSMA[i - 1] && shortSMA[i] > longSMA[i])
                {
                    Console.WriteLine($"Buy signal on day {i + longPeriod - 1}");
                }
                else if (shortSMA[i - 1] > longSMA[i - 1] && shortSMA[i] < longSMA[i])
                {
                    Console.WriteLine($"Sell signal on day {i + longPeriod - 1}");
                }
            }
        }

        public static void AnalyzeMovingAverageCrossover(List<List<string>> filteredData, int shortPeriod, int longPeriod)
        {
            List<double> prices = new List<double>();

            foreach (var data in filteredData)
            {
                if (double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                {
                    prices.Add(price);
                }
            }

            if (prices.Count < longPeriod)
            {
                Console.WriteLine("Not enough data for the specified periods.");
                return;
            }

            List<double> shortSMA = CalculateSMA(prices, shortPeriod);
            List<double> longSMA = CalculateSMA(prices, longPeriod);

            for (int i = 1; i < Math.Min(shortSMA.Count, longSMA.Count); i++)
            {
                if (shortSMA[i - 1] < longSMA[i - 1] && shortSMA[i] > longSMA[i])
                {
                    Console.WriteLine($"Buy signal on day {i + longPeriod - 1}");
                }
                else if (shortSMA[i - 1] > longSMA[i - 1] && shortSMA[i] < longSMA[i])
                {
                    Console.WriteLine($"Sell signal on day {i + longPeriod - 1}");
                }
            }
        }
    }
}
