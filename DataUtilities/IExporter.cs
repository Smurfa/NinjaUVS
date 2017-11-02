using System.Collections.Generic;

namespace DataUtilities
{
    public interface IExporter
    {
        void Export<T>(IEnumerable<T> data, string outputPath);
    }
}
