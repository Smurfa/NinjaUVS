using System;
using System.Collections.Generic;
using DataUtilities.Csv;
using DataUtilities.Model;

namespace NinjaUVS
{
    internal class Program
    {
        private static IDictionary<string, IEnumerable<ShareHistoryPoint>> _shares;
        private static IEnumerable<Transaction> _transactions;

        internal static void Main(string[] args)
        {
            var loader = new DataLoader(new CsvImporter());
            LoadData(loader);
        }

        private static void LoadData(DataLoader loader)
        {
            _shares = loader.GetSharesHistory();
            _transactions = loader.GetTransactions();
        }
    }
}
