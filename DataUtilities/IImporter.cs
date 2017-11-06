using System.Collections.Generic;
using DataUtilities.Models;
using DataUtilities.Models.Transactions;

namespace DataUtilities
{
    public interface IImporter
    {
        IEnumerable<ShareHistoryPoint> ReadShareHistoryPoints(string filepath);
        IEnumerable<TransactionBase> ReadTransactions(string filepath);
    }
}
