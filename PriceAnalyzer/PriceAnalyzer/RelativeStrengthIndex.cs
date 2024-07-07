using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PriceAnalyzer
{
    class RelativeStrengthIndex
    {
        public static List<double> CalculateRSI(List<double> prices, int period)
        {
            List<double> rsi = new List<double>();

            if (period <= 0 || prices.Count < period)
            {
                throw new ArgumentException("Invalid period or insufficient price data.");
            }

            List<double> gains = new List<double>();
            List<double> losses = new List<double>();

            for (int i = 1; i < prices.Count; i++)
            {
                double change = prices[i] - prices[i - 1];
                if (change > 0)
                {
                    gains.Add(change);
                    losses.Add(0);
                }
                else
                {
                    gains.Add(0);
                    losses.Add(Math.Abs(change));
                }
            }

            double averageGain = 0;
            double averageLoss = 0;
            for (int i = 0; i < period; i++)
            {
                averageGain += gains[i];
                averageLoss += losses[i];
            }
            averageGain /= period;
            averageLoss /= period;

            double rs = averageGain / averageLoss;
            rsi.Add(100 - (100 / (1 + rs)));

            for (int i = period; i < gains.Count; i++)
            {
                averageGain = ((averageGain * (period - 1)) + gains[i]) / period;
                averageLoss = ((averageLoss * (period - 1)) + losses[i]) / period;

                rs = averageGain / averageLoss;
                rsi.Add(100 - (100 / (1 + rs)));
            }

            return rsi;
        }

        public static void AnalyzeRSI(List<List<string>> filteredData, int period)
        {
            List<double> prices = new List<double>();

            foreach (var data in filteredData)
            {
                if (double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                {
                    prices.Add(price);
                }
            }

            if (prices.Count < period)
            {
                Console.WriteLine("Not enough data for the specified period.");
                return;
            }

            List<double> rsi = CalculateRSI(prices, period);
            bool signalFound = false;
            for (int i = 0; i < rsi.Count; i++)
            {
                if (rsi[i] < 30)
                {
                    Console.WriteLine($"Buy signal on day {i + period}: RSI = {rsi[i]:F2}");
                    signalFound = true;
                }
                else if (rsi[i] > 70)
                {
                    Console.WriteLine($"Sell signal on day {i + period}: RSI = {rsi[i]:F2}");
                    signalFound = true;
                }
                if (!signalFound)
                {
                    Console.WriteLine($"No buy or sell signals found in the period {filteredData.First()[0]} to {filteredData.Last()[0]}.");
                }


            }
        }
    }
}
