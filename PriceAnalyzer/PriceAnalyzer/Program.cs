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
}
