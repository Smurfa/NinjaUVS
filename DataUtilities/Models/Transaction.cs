using System;

namespace DataUtilities.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; }
        public float? Rate { get; set; }
        public float? Amount { get; set; }
        public int? BrokerageFee { get; set; }
        public string Currency { get; set; }
        public string Isin { get; set; }
    }
}
