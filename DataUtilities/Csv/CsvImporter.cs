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

        public IEnumerable<Transaction> ReadTransactions(string filepath)
        {
            using (var streamReader = File.OpenText(filepath))
            using (var csvReader = new CsvReader(streamReader, new Configuration { Delimiter = ",", CultureInfo = new CultureInfo("sv-SE") }))
            {
                return csvReader.GetRecords<dynamic>()
                    .Select(record => new Transaction
                    {
                        Amount = csvReader.GetField<float>(6),
                        Currency = csvReader.GetField<string>(7),
                        Name = csvReader.GetField<string>(3),
                        TransactionType = csvReader.GetField<string>(2)
                    })
                    .ToList();
            }
        }
    }
}
