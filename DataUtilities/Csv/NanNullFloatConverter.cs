using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DataUtilities.Csv
{
    internal class NanNullFloatConverter : ITypeConverter
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((float?) value)?.ToString(NumberFormatInfo.CurrentInfo) ?? "-";
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (float.TryParse(text?.Replace(',', '.'), NumberStyles.Float, NumberFormatInfo.InvariantInfo, out var value))
                return value;

            return null;
        }
    }
}
