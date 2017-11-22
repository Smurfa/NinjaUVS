using System;
using CsvHelper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataUtilities.Csv;

namespace NinjaUVSTest
{
    [TestClass]
    public class TestNanNullFloatConverter
    {
        private readonly NanNullFloatConverter _converter = new NanNullFloatConverter();

        [TestMethod]
        public void Float_ConvertFromString_NullInput()
        {
            Assert.IsNull(_converter.ConvertFromString(null, null, null));
        }

        [TestMethod]
        public void Float_ConvertFromString_ValidInput()
        {
            var value = (float?)_converter.ConvertFromString("1,5", null, null);
            Assert.IsTrue(value == 1.5f);
        }

        [TestMethod]
        public void Float_ConvertFromString_InvalidOrEmptyInput()
        {
            Assert.IsNull(_converter.ConvertFromString("-", null, null));

            Assert.IsNull(_converter.ConvertFromString(string.Empty, null, null));
        }

        [TestMethod]
        public void Float_ConvertToString_ValidInput()
        {
            Assert.IsTrue(_converter.ConvertToString(1.5f, null, null) == "1,5");
        }

        [TestMethod]
        public void Float_ConvertToString_NullInput()
        {
            Assert.IsTrue(_converter.ConvertToString(null, null, null) == "-");
        }
    }
}
