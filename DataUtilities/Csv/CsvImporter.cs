using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;    
using CsvHelper.Configuration;
using NLog;
using DataUtilities.Models;
using DataUtilities.Models.Transactions;

namespace DataUtilities.Csv
{
    public class CsvImporter : IImporter
    {
        private readonly ILogger _logger;

        public CsvImporter(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<ShareHistoryPoint> ReadShareHistoryPoints(string filepath)
        {
            using (var streamReader = File.OpenText(filepath))
            using (var csvReader = new CsvReader(streamReader, new Configuration { Delimiter = ";", CultureInfo = new CultureInfo("sv-SE") }))
            {
                csvReader.Read();
                return csvReader.GetRecords<dynamic>()
                    .Select(record => new ShareHistoryPoint
                    {
                        ClosingValue = csvReader.GetField<float>(6),
                        Date = csvReader.GetField<DateTime>(0)
                    })
                    .ToList();
            }
        }

        public IEnumerable<TransactionBase> ReadTransactions(string filepath)
        {
            using (var streamReader = File.OpenText(filepath))
            using (var csvReader = new CsvReader(streamReader, new Configuration { Delimiter = ",", CultureInfo = new CultureInfo("sv-SE") }))
            {
                var list = new List<TransactionBase>();
                foreach (var record in csvReader.GetRecords<dynamic>())
                {
                    switch (csvReader.GetField<string>(2))
                    {
                        case "Deposit":
                        case "Insättning":
                            {
                                list.Add(NewDeposit(csvReader));
                                break;
                            }
                        case "Purchase":
                        case "Köp":
                            {
                                list.Add(NewPurchase(csvReader));
                                break;
                            }
                        default:
                            {
                                _logger.Log(LogLevel.Warn, $"Unknown transaction type: {csvReader.GetField<string>(2)}");
                                break;
                            }
                    }
                }
                return list;
            }
        }

        private static Deposit NewDeposit(IReaderRow reader)
        {
            return new Deposit
            {
                Date = reader.GetField<DateTime>(0),
                Amount = reader.GetField<float>(6),
                Currency = reader.GetField<string>(7),
                Name = reader.GetField<string>(3)
            };
        }

        private static Purchase NewPurchase(IReaderRow reader)
        {
            return new Purchase
            {
                Date = reader.GetField<DateTime>(0),
                Amount = reader.GetField<float>(6),
                Currency = reader.GetField<string>(7),
                Name = reader.GetField<string>(3),
                NumOfShares = reader.GetField<int>(4),
                SharePrice = reader.GetField<float>(5)
            };
        }
    }
}
