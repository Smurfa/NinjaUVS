namespace DataUtilities.Models.Transactions
{
    public class Dividend : TransactionBase
    {
        public int NumOfShares { get; set; }
        public float SharePrice { get; set; }
    }
}
