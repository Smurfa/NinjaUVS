using System;
using System.Collections.Generic;
using DataUtilities;
using DataUtilities.Csv;
using DataUtilities.Models;
using NLog;

namespace NinjaUVS
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static IDictionary<string, IEnumerable<ShareHistoryPoint>> _shares;
        private static IEnumerable<TransactionBase> _transactions;

        internal static void Main(string[] args)
        {
            try
            {
                Logger.Log(LogLevel.Info, "Starting up");
                var loader = new DataLoader(new CsvImporter());
                LoadData(loader);
                Logger.Log(LogLevel.Info, "Shutting down");
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
            }
        }

        private static void LoadData(DataLoader loader)
        {
            _shares = loader.LoadSharesHistory(Properties.Settings.Default.HistoryPath);
            _transactions = loader.LoadTransactions(Properties.Settings.Default.TransactionPath);
        }
    }
}
