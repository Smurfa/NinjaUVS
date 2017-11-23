using System.Globalization;
using System.Linq;
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
            if (int.TryParse(text?.Split('.', ',').First(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
                return value;

            return null;
        }
    }
}
