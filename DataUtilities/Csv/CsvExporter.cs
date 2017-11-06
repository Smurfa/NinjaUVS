using System.Collections.Generic;
using System.IO;
using CsvHelper;
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
            using (var csv = new CsvWriter(textWriter))
            {
                csv.WriteRecords(data);
            }
        }
    }
}
