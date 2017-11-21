using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using DataUtilities.Models;
using NLog;

namespace DataUtilities.Csv
{
    internal class TransactionTypeConverter : ITypeConverter
    {
        //private readonly ILogger _logger;

        //public TransactionTypeConverter(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((TransactionType?) value)?.ToString() ?? TransactionType.Unknown.ToString();
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            switch (text)
            {
                case "Insättning":
                case "Deposit":
                    {
                        return TransactionType.Deposit;
                    }
                case "Purchase":
                case "Köp":
                    {
                        return TransactionType.Purchase;
                    }
                case "Dividend":
                case "Utdelning":
                    {
                        return TransactionType.Dividend;
                    }
                case "Sale":
                case "Sälj":
                    {
                        return TransactionType.Sale;
                    }
                case "Withdrawal":
                case "Uttag":
                    {
                        return TransactionType.Withdrawal;
                    }
                default:
                    {
                        //_logger.Log(LogLevel.Warn, $"Unknown transaction type: {text}");
                        return TransactionType.Unknown;
                    }
            }
        }
    }
}
