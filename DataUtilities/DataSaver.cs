using System.Collections.Generic;
using DataUtilities;

namespace DataUtilities
{
    public class DataSaver
    {
        private readonly IExporter _exporter;

        public DataSaver(IExporter exporter)
        {
            _exporter = exporter;
        }

        public void Save<T>(IEnumerable<T> data, string outputPath)
        {
            _exporter.Export(data, outputPath);
        }
    }
}
