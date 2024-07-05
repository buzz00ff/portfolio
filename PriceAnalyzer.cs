using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PriceAnalyzer
{
    class Program
    {
        static void Main()
        {
            string csvFilePath = GetCsvFilePath();

            List<List<string>> dataAndPrice = ReadDataFromCsv(csvFilePath);

            if (dataAndPrice.Count > 0)
            {
                string startDate = GetStartDateFromUser();
                string endDate = GetEndDateFromUser();

                List<List<string>> filteredData = FilterDataByDate(dataAndPrice, startDate, endDate);

                List<List<string>> analyzedData = AnalyzePriceChanges(filteredData);

                bool analyze = true;
                while (analyze)
                {
                    Console.WriteLine("Wybierz analizę którą chcesz przeprowadzić\n 1. Analiza wzrostu/ spadku cen\n 2. Średnia ruchoma\n 3. Wskaźnik względnej siły\n 4. Analiza zmienności\n 5. Wyjście\n");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            int score = ClosePricesAnalyzeW(analyzedData);
                            if (score >= 5)
                            {
                                Console.WriteLine("Strong buy");
                            }
                            else if (score >= 3 && score <= 4)
                            {
                                Console.WriteLine("Buy");
                            }
                            else
                            {
                                score = ClosePricesAnalyzeL(analyzedData);
                                if (score >= 5)
                                {
                                    Console.WriteLine("Strong sell");
                                }
                                else if (score >= 3 && score <= 4)
                                {
                                    Console.WriteLine("Sell");
                                }
                                else
                                {
                                    Console.WriteLine("Make another analysis.");
                                }
                            }
                            break;
                        case 2:
                            SimpleMovingAverage.AnalyzeMovingAverageCrossover(filteredData, 3, 5);  // Przykładowe okresy
                            break;
                        case 3:
                            RelativeStrengthIndex.AnalyzeRSI(filteredData, 14); //Typowy okres dla RSI
                            break;
                        case 4:
                            VolatilityAnalysis.AnalyzeVolatility(filteredData, 5); //Przykładowy okres
                            break;
                        case 5:
                            analyze = false;
                            break;
                    }
                }
            }

            Console.ReadLine();
        }

        static string GetCsvFilePath()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            return Path.Combine(projectDirectory, "btc-usd-max.csv");
        }

        static List<List<string>> ReadDataFromCsv(string csvFilePath)
        {
            List<List<string>> dataAndPrice = new List<List<string>>();

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                {
                    bool isFirstLine = true;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }

                        var values = line.Split(',');

                        if (values.Length < 2)
                            continue;

                        var date = FormatDate(values[0]);
                        var price = values[1];
                        List<string> innerList = new List<string> { date, price };
                        dataAndPrice.Add(innerList);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading data from CSV file: " + ex.Message);
            }

            return dataAndPrice;
        }

        static string FormatDate(string text)
        {
            string[] words = text.Split(' ');
            return words[0];
        }

        static string GetStartDateFromUser()
        {
            Console.WriteLine("Enter the start date (YYYY-MM-DD):");
            string startDate = Console.ReadLine();
            while (!DateTime.TryParse(startDate, out _))
            {
                Console.WriteLine("Invalid date format. Please enter the start date (YYYY-MM-DD):");
                startDate = Console.ReadLine();
            }
            return startDate;
        }

        static string GetEndDateFromUser()
        {
            Console.WriteLine("Enter the end date (YYYY-MM-DD):");
            string endDate = Console.ReadLine();
            while (!DateTime.TryParse(endDate, out _))
            {
                Console.WriteLine("Invalid date format. Please enter the end date (YYYY-MM-DD):");
                endDate = Console.ReadLine();
            }
            return endDate;
        }

        static List<List<string>> FilterDataByDate(List<List<string>> dataAndPrice, string startDate, string endDate)
        {
            List<List<string>> filteredData = new List<List<string>>();

            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            foreach (var innerList in dataAndPrice)
            {
                if (DateTime.TryParse(innerList[0], out DateTime dateValue))
                {
                    if (dateValue >= start && dateValue <= end)
                    {
                        filteredData.Add(innerList);
                    }
                }
            }

            return filteredData;
        }

        static List<List<string>> AnalyzePriceChanges(List<List<string>> filteredData)
        {
            for (int i = 1; i < filteredData.Count; i++)
            {
                if (double.TryParse(filteredData[i][1], NumberStyles.Any, CultureInfo.InvariantCulture, out double currentPrice) &&
                    double.TryParse(filteredData[i - 1][1], NumberStyles.Any, CultureInfo.InvariantCulture, out double previousPrice))
                {
                    filteredData[i].Add(currentPrice > previousPrice ? "W" : "L");
                }
            }

            return filteredData;
        }

        static int ClosePricesAnalyzeW(List<List<string>> analyzedData)
        {
            int scoreW = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (analyzedData[analyzedData.Count - i][2] == "W") { scoreW++; }
                else break;
            }
            return scoreW;
        }

        static int ClosePricesAnalyzeL(List<List<string>> analyzedData)
        {
            int scoreL = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (analyzedData[analyzedData.Count - i][2] == "L") { scoreL++; }
                else break;
            }
            return scoreL;
        }

        static void DisplayData(List<List<string>> data)
        {
            foreach (var innerList in data)
            {
                Console.WriteLine(string.Join(", ", innerList));
            }
        }
    }

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
