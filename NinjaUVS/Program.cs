﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataUtilities;
using DataUtilities.Csv;
using DataUtilities.Models;
using NLog;

namespace NinjaUVS
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static IDictionary<string, IEnumerable<ShareHistoryPoint>> _shares;
        private static IEnumerable<Transaction> _transactions;

        private static IEnumerable<Subscription> _subscriptions;
        private static IEnumerable<Ownership> _ownerships;

        internal static void Main(string[] args)
        {
            try
            {
                Logger.Log(LogLevel.Info, "Start...");
                var loader = new DataLoader(new CsvImporter(Logger));
                LoadData(loader);

                var calcSubscriptions = new SubscriptionAlgorithm(_transactions.Reverse(), _shares);
                _subscriptions = calcSubscriptions.RunTransactions();

                _ownerships = SummarizeOwnerships(_subscriptions, calcSubscriptions.UnitValue);

                var savior = new DataSaver(new CsvExporter(Logger)); // :DD
                SaveData(savior);
                Logger.Log(LogLevel.Info, "Done");
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
            }
        }

        private static void LoadData(DataLoader loader)
        {
            _shares = loader.LoadSharesHistory(Properties.Settings.Default.HistoryPath);
            _transactions = loader.LoadTransactions(Properties.Settings.Default.TransactionFile);
        }

        private static void SaveData(DataSaver saver)
        {
            saver.Save(_ownerships.OrderBy(x => x.Name), "Ownership.csv");
            saver.Save(_subscriptions, "Subscriptions.csv");
        }

        private static IEnumerable<Ownership> SummarizeOwnerships(IEnumerable<Subscription> subscriptions, float? unitValue)
        {
            var members = subscriptions.DistinctBy(x => x.Member).Select(x => x.Member);

            foreach (var member in members)
            {
                var units = subscriptions.Where(x => x.Member == member)
                    .Aggregate<Subscription, float?>(0.0f, (current, next) => current + next.PurchasedUnits);
                var paid = subscriptions.Where(x => x.Member == member)
                    .Aggregate<Subscription, float?>(0.0f, (current, next) => current + next.Payment);

                yield return new Ownership
                {
                    Name = member,
                    Paid = paid,
                    Units = units,
                    TotalValue = units * unitValue
                };
            }
        }
    }
}
