using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using DataImporter.Model;

namespace DataImporter.Csv
{
    public class CsvImporter
    {
        private CsvReader _reader;

        public IEnumerable<ShareHistory> ReadFile(string filepath, string name)
        {
            using (var reader = File.OpenText(filepath))
            {
                _reader = new CsvReader(reader, new Configuration{ Delimiter = ";", CultureInfo = new CultureInfo("sv-SE") });
                _reader.Read();

                return _reader.GetRecords<dynamic>()
                    .Select(record => new ShareHistory
                    {
                        ClosingValue = _reader.GetField<float>(6),
                        Date = _reader.GetField<DateTime>(0),
                        Name = name
                    }).ToList();
            }
        }
    }
}
