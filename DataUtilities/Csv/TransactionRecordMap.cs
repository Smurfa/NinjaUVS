﻿using CsvHelper.Configuration;
using DataUtilities.Models;

namespace DataUtilities.Csv
{
    internal sealed class TransactionRecordMap : ClassMap<Transaction>
    {
        public TransactionRecordMap()
        {
            Map(m => m.Date).Name("Datum");
            Map(m => m.Account).Name("Konto");
            Map(m => m.TransactionType).Name("Typ av transaktion").TypeConverter<TransactionTypeConverter>();
            Map(m => m.Description).Name("Värdepapper/beskrivning");
            Map(m => m.NumOfShares).Name("Antal").TypeConverter<NanNullIntConverter>();
            Map(m => m.Rate).Name("Kurs").TypeConverter<NanNullFloatConverter>();
            Map(m => m.Amount).Name("Belopp").TypeConverter<NanNullFloatConverter>();
            Map(m => m.BrokerageFee).Name("Courtage").TypeConverter<NanNullIntConverter>();
            Map(m => m.Currency).Name("Valuta");
            Map(m => m.Isin).Name("ISIN");
        }
    }
}
