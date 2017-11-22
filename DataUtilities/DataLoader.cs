using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DataUtilities.Models;
using DataUtilities.Models.Transactions;

namespace DataUtilities
{
    public class DataLoader
    {
        private readonly IImporter _importer;

        public DataLoader(IImporter importer)
        {
            _importer = importer;
        }
        
        public IEnumerable<TransactionBase> LoadTransactions(string path)
        {
            return _importer.ReadTransactions(path);
        }
        

        public IDictionary<string, IEnumerable<ShareHistoryPoint>> LoadSharesHistory(string directory)
        {
            return Directory.GetFiles(directory)
                .ToDictionary(GetShareNameFromFile, _importer.ReadShareHistoryPoints);
        }

        private static string GetShareNameFromFile(string path)
        {
            var filename = Path.GetFileName(path);
            return Regex.Match(filename, @"^[A-Za-z0-9&\s]*[^-]").ToString();
        }
    }
}
