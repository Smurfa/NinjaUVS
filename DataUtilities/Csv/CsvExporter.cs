using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace DataUtilities.Csv
{
    public class CsvExporter : IExporter
    {
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
