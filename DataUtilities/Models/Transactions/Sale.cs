namespace DataUtilities.Models.Transactions
{
    public class Sale : TransactionBase
    {
        public int NumOfShares { get; set; }
        public float SharePrice { get; set; }
    }
}
