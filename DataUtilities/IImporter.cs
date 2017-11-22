using System.Collections.Generic;
using DataUtilities.Models;

namespace DataUtilities
{
    public interface IImporter
    {
        IEnumerable<ShareHistoryPoint> ReadShareHistoryPoints(string filepath);
        IEnumerable<Transaction> ReadTransactions(string filepath);
    }
}
