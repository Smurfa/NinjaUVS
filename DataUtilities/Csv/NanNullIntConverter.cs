using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DataUtilities.Csv
{
    internal class NanNullIntConverter : ITypeConverter
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((int?) value)?.ToString() ?? "-";
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (int.TryParse(text, out var value))
                return value;

            return null;
        }
    }
}
