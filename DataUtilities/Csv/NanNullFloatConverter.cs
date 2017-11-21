using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DataUtilities.Csv
{
    internal class NanNullFloatConverter : ITypeConverter
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((float?) value)?.ToString() ?? "-";
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (float.TryParse(text, out var value))
                return value;

            return null;
        }
    }
}
