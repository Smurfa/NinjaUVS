using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataUtilities.Csv;
using DataUtilities.Models;

namespace NinjaUVSTest.DataUtilities
{
    [TestClass]
    public class TestTransactionTypeConverter
    {
        private readonly TransactionTypeConverter _converter = new TransactionTypeConverter();
        private const string svPurchase = "Köp";
        private const string enPurchase = "Purchase";
        private const string svSale = "Sälj";
        private const string enSale = "Sale";
        private const string svDividend = "Utdelning";
        private const string enDividend = "Dividend";
        private const string svDeposit = "Insättning";
        private const string enDeposit = "Deposit";
        private const string svWithdrawal = "Uttag";
        private const string enWithdrawal = "Withdrawal";
        private const string unknown = "Unknown";

        [TestMethod]
        public void TransactionType_ConvertFromString_NullShouldRetunUnknown()
        {
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(null, null, null) == TransactionType.Unknown);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_UndefinedShouldRetunUnknown()
        {
            const string transaction = "Steal";
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(transaction, null, null) == TransactionType.Unknown);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_PurchaseShouldRetunPurchase()
        {
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(svPurchase, null, null) == TransactionType.Purchase);
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(enPurchase, null, null) == TransactionType.Purchase);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_SaleShouldRetunSale()
        {
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(svSale, null, null) == TransactionType.Sale);
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(enSale, null, null) == TransactionType.Sale);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_DividendShouldRetunDividend()
        {
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(svDividend, null, null) == TransactionType.Dividend);
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(enDividend, null, null) == TransactionType.Dividend);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_DepositShouldRetunDeposit()
        {
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(svDeposit, null, null) == TransactionType.Deposit);
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(enDeposit, null, null) == TransactionType.Deposit);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_WithdrawalShouldRetunWithdrawal()
        {
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(svWithdrawal, null, null) == TransactionType.Withdrawal);
            Assert.IsTrue((TransactionType)_converter.ConvertFromString(enWithdrawal, null, null) == TransactionType.Withdrawal);
        }

        [TestMethod]
        public void TransactionType_ConvertToString_NullShouldReturnUnknown()
        {
            Assert.IsTrue(_converter.ConvertToString(null, null, null) == unknown);
        }

        [TestMethod]
        public void TransactionType_ConvertFromString_UnknownShouldRetunUnknown()
        {
            Assert.IsTrue(_converter.ConvertToString(TransactionType.Unknown, null, null) == unknown);
        }

        [TestMethod]
        public void TransactionType_ConvertToString_PurchaseShouldRetunPurchase()
        {
            Assert.IsTrue(_converter.ConvertToString(TransactionType.Purchase, null, null) == enPurchase);
        }

        [TestMethod]
        public void TransactionType_ConvertToString_SaleShouldRetunSale()
        {
            Assert.IsTrue(_converter.ConvertToString(TransactionType.Sale, null, null) == enSale);
        }

        [TestMethod]
        public void TransactionType_ConvertToString_DividendShouldRetunDividend()
        {
            Assert.IsTrue(_converter.ConvertToString(TransactionType.Dividend, null, null) == enDividend);
        }

        [TestMethod]
        public void TransactionType_ConvertToString_DepositShouldRetunDeposit()
        {
            Assert.IsTrue(_converter.ConvertToString(TransactionType.Deposit, null, null) == enDeposit);
        }

        [TestMethod]
        public void TransactionType_ConvertToString_WithdrawalShouldRetunWithdrawal()
        {
            Assert.IsTrue(_converter.ConvertToString(TransactionType.Withdrawal, null, null) == enWithdrawal);
        }
    }
}
