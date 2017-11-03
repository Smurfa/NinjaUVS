using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;    
using CsvHelper.Configuration;
using DataUtilities.Models;

namespace DataUtilities.Csv
{
    public class CsvImporter : IImporter
    {
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
                    TransactionBase transaction;
                    if (csvReader.GetField<string>(2) == "Deposit")
                    {
                        transaction = new Deposit();
                    }
                    else
                    {
                        transaction = new Purchase
                        {
                            NumOfShares = csvReader.GetField<int>(4),
                            SharePrice = csvReader.GetField<float>(5)
                        };
                    }
                    transaction.Date = csvReader.GetField<DateTime>(0);
                    transaction.Amount = csvReader.GetField<float>(6);
                    transaction.Currency = csvReader.GetField<string>(7);
                    transaction.Name = csvReader.GetField<string>(3);
                    list.Add(transaction);
                }
                return list;
            }
        }
    }
}
