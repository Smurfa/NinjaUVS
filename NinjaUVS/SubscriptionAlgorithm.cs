using System;
using System.Collections.Generic;
using System.Linq;
using DataUtilities.Models;
using DataUtilities.Models.Transactions;

namespace NinjaUVS
{
    public class SubscriptionAlgorithm
    {
        private readonly IEnumerable<TransactionBase> _transactions;
        private readonly IDictionary<string, IEnumerable<ShareHistoryPoint>> _sharesHistory;

        private float _transactionsSum;
        private float _clubAssets;
        private float _clubUnits;

        private readonly IDictionary<string, int> _sharesCount;

        public SubscriptionAlgorithm(IEnumerable<TransactionBase> transactions,
            IDictionary<string, IEnumerable<ShareHistoryPoint>> sharesHistory)
        {
            _transactions = transactions;
            _sharesHistory = sharesHistory;
            _sharesCount = new Dictionary<string, int>();
            foreach (var key in sharesHistory.Keys)
            {
                _sharesCount.Add(key, 0);
            }
        }

        public IEnumerable<Subscription> RunTransactions()
        {
            var subscriptions = new List<Subscription>();
            foreach (var transaction in _transactions)
            {
                if (transaction is Purchase)
                {
                    NewPurchase(transaction);
                }
                else
                {
                    subscriptions.Add(NewSubscription(transaction));
                }
            }
            return subscriptions;
        }

        private void NewPurchase(TransactionBase transaction)
        {
            _transactionsSum += transaction.Amount;
            _sharesCount[transaction.Name] += (transaction as Purchase).NumOfShares;
        }

        private float CalculateMarketValue(DateTime date)
        {
            return _sharesCount.Keys.Aggregate(0.0f, (current, key) =>
                _sharesHistory[key].OrderBy(x => x.Date)
                    .TakeWhile(x => x.Date < date)
                    .Last()
                    .ClosingValue * _sharesCount[key] + current);
        }
        
        private Subscription NewSubscription(TransactionBase transaction)
        {
            var deposit = transaction as Deposit;
            var marketValue = CalculateMarketValue(deposit.Date);
            var unitValue = _clubUnits == 0 ? 100.0f : (_transactionsSum + marketValue) / _clubUnits;
            var purchasedUnits = deposit.Amount / unitValue;

            _transactionsSum += deposit.Amount;
            _clubAssets = _transactionsSum + marketValue;
            _clubUnits += purchasedUnits;
            
            return new Subscription
            {
                Date = deposit.Date,
                Member = deposit.Name,
                Payment = deposit.Amount,
                PurchasedUnits = purchasedUnits,
                ClubAssets = _clubAssets,
                ClubUnits = _clubUnits,
                UnitValue = unitValue,
                MarketValue = marketValue
            };
        }
    }
}
