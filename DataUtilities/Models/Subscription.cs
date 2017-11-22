using System;

namespace DataUtilities.Models
{
    public class Subscription
    {
        public DateTime Date { get; set; }
        public string Member { get; set; }
        public float? Payment { get; set; }
        public float? PurchasedUnits { get; set; }
        public float? ClubAssets { get; set; }
        public float? ClubUnits { get; set; }
        public float? UnitValue { get; set; }
        public  float? MarketValue { get; set; }
    }
}
