using System;

namespace DataUtilities.Models.Transactions
{
    public abstract class TransactionBase
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
    }
}
