﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataUtilities.Models;
using NLog;

namespace NinjaUVS
{
    public class SubscriptionAlgorithm
    {
        private readonly IEnumerable<Transaction> _transactions;
        private readonly IDictionary<string, IEnumerable<ShareHistoryPoint>> _sharesHistory;

        private float? _accountAssets;
        private float? _clubUnits;

        private readonly IDictionary<string, int?> _sharesCount;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        
        public float? UnitValue { get; private set; }

        public SubscriptionAlgorithm(IEnumerable<Transaction> transactions,
            IDictionary<string, IEnumerable<ShareHistoryPoint>> sharesHistory)
        {
            _transactions = transactions;
            _sharesHistory = sharesHistory;
            _sharesCount = new Dictionary<string, int?>();
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
                switch (transaction.TransactionType)
                {
                    case TransactionType.Deposit:
                    case TransactionType.Withdrawal:

                        {
                            subscriptions.Add(AccountDeposit(transaction));
                            break;
                        }
                    case TransactionType.Purchase:
                    case TransactionType.Sale:
                        {
                            MarketBuySell(transaction);
                            break;
                        }
                    case TransactionType.Dividend:
                        {
                            AdjustDividend(transaction.Amount);
                            break;
                        }
                    default:
                        {
                            _logger.Log(LogLevel.Warn, $"Unknown transaction type '{transaction.TransactionType}' for '{transaction.Description}'");
                            break; 
                        }
                }
            }
            return subscriptions;
        }

        private void AdjustDividend(float? amount)
        {
            _accountAssets += amount ?? throw new ArgumentNullException(nameof(amount));
        }

        private void MarketBuySell(Transaction transaction)
        {
            _accountAssets += transaction?.Amount;
            _sharesCount[transaction?.Description ?? throw new InvalidOperationException("Transaction description can not be null")] += transaction.NumOfShares;
        }

        /// <summary>
        /// Calculates the total value of owned shares for a given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private float? CalculateMarketAssets(DateTime date)
        {
            return _sharesCount.Keys.Aggregate<string, float?>(0.0f, (current, key) =>
                _sharesHistory[key]
                    .OrderBy(x => x.Date)
                    .TakeWhile(x => x.Date < date)
                    .Last()
                    .ClosingValue * _sharesCount[key] + current);
        }

        /// <summary>
        /// Calculates the value of a unit based on the value of the market and account assets (market + account = club assets).
        /// </summary>
        /// <param name="marketAssets"></param>
        /// <returns></returns>
        private float? CalculateUnitValue(float? marketAssets)
        {
            if (marketAssets == null)
                throw new ArgumentNullException(nameof(marketAssets));

            if (_clubUnits == null)
                return 100.0f;

            return (_accountAssets + marketAssets) / _clubUnits;
        }
        
        private Subscription AccountDeposit(Transaction transaction)
        {
            var marketAssets = CalculateMarketAssets(transaction.Date);
            UnitValue = CalculateUnitValue(marketAssets);

            var purchasedUnits = transaction.Amount / UnitValue;
            _accountAssets = _accountAssets == null ? transaction.Amount : _accountAssets + transaction.Amount;
            var clubAssets = _accountAssets + marketAssets;
            _clubUnits = _clubUnits == null ? purchasedUnits : _clubUnits + purchasedUnits;
            
            return new Subscription
            {
                Date = transaction.Date,
                Member = transaction.Description,
                Payment = transaction.Amount,
                PurchasedUnits = purchasedUnits,
                ClubAssets = clubAssets,
                ClubUnits = _clubUnits,
                UnitValue = UnitValue,
                MarketValue = marketAssets
            };
        }
    }
}
