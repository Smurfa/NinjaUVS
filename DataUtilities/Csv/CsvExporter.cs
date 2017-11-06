using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using NLog;

namespace DataUtilities.Csv
{
    public class CsvExporter : IExporter
    {
        private readonly ILogger _logger;

        public CsvExporter(ILogger logger)
        {
            _logger = logger;
        }

        public void Export<T>(IEnumerable<T> data, string outputPath)
        {
            using (var textWriter = File.CreateText(outputPath))
            using (var csv = new CsvWriter(textWriter, new Configuration { Delimiter = ";", CultureInfo = new CultureInfo("sv-SE") }))
            {
                csv.WriteRecords(data);
            }
        }
    }
}
