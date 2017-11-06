namespace DataUtilities.Models.Transactions
{
    public class Purchase : TransactionBase
    {
        public int NumOfShares { get; set; }
        public float SharePrice { get; set; }
    }
}
