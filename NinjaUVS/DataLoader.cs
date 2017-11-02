using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DataUtilities;
using DataUtilities.Model;

namespace NinjaUVS
{
    public class DataLoader
    {
        private readonly IImporter _importer;

        public DataLoader(IImporter importer)
        {
            _importer = importer;
        }
        
        public IEnumerable<Transaction> LoadTransactions()
        {
            return _importer.ReadTransactions(Properties.Settings.Default.TransactionPath);
        }
        

        public IDictionary<string, IEnumerable<ShareHistoryPoint>> LoadSharesHistory()
        {
            return Directory.GetFiles(Properties.Settings.Default.HistoryPath)
                .ToDictionary(GetShareNameFromFile, _importer.ReadShareHistoryPoints);
        }

        private static string GetShareNameFromFile(string path)
        {
            var filename = Path.GetFileName(path);
            var shareName = Regex.Match(filename, @"^[^0-9]*").ToString();
            return shareName.Substring(0, shareName.Length - 1);
        }
    }
}
