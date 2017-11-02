using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataUtilities.Csv;
using DataUtilities.Model;

namespace NinjaUVS
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var importer = new CsvImporter();
            var shares = LoadSharesHistory(importer);
            var transactions = LoadTransactions(importer);



            var i = 0;
        }

        private static IDictionary<string, IEnumerable<ShareHistoryPoint>> LoadSharesHistory(CsvImporter importer)
        {
            return Directory.GetFiles(Properties.Settings.Default.HistoryPath)
                .ToDictionary(GetShareNameFromFile, importer.ReadShareHistoryPoints);
        }

        private static string GetShareNameFromFile(string path)
        {
            var filename = Path.GetFileName(path);
            if (string.IsNullOrEmpty(filename))
                throw new NullReferenceException("Filename is null");

            var shareName = Regex.Match(filename, @"^[^0-9]*").ToString();
            return shareName.Substring(0, shareName.Length - 1);
        }

        private static IEnumerable<Transaction> LoadTransactions(CsvImporter importer)
        {
            return importer.ReadTransactions(Properties.Settings.Default.TransactionPath);
        }

    }
}
