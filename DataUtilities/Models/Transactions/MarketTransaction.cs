namespace DataUtilities.Models.Transactions
{
    public class MarketTransaction : TransactionBase
    {
        public int NumOfShares { get; set; }
        public float SharePrice { get; set; }
    }
}
