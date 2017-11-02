using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtilities;

namespace NinjaUVS
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
