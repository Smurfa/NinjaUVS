using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataUtilities.Csv;

namespace NinjaUVSTest.DataUtilities
{
    [TestClass]
    public class TestNanNullIntConverter
    {
        private readonly NanNullIntConverter _converter = new NanNullIntConverter();

        [TestMethod]
        public void Int_ConvertFromString_NullInputShouldReturnNull()
        {
            Assert.IsNull(_converter.ConvertFromString(null, null, null));
        }

        [TestMethod]
        public void Int_ConvertFromString_CommaInputShouldBeTruncated()
        {
            var value = (int?)_converter.ConvertFromString("1,5", null, null);
            Assert.IsTrue(value == 1);
        }

        [TestMethod]
        public void Int_ConvertFromString_PointInputShouldBeTruncated()
        {
            var value = (int?)_converter.ConvertFromString("1.5", null, null);
            Assert.IsTrue(value == 1);
        }

        [TestMethod]
        public void Int_ConvertFromString_InvalidOrEmptyInputShouldReturnNull()
        {
            Assert.IsNull(_converter.ConvertFromString("-", null, null));

            Assert.IsNull(_converter.ConvertFromString(string.Empty, null, null));
        }

        [TestMethod]
        public void Int_ConvertToString_ValidInputshouldSucceed()
        {
            const int value = 2;
            Assert.IsTrue(_converter.ConvertToString(value, null, null) == value.ToString(NumberFormatInfo.CurrentInfo));
        }

        [TestMethod]
        public void Int_ConvertToString_NullInputShouldReturnDash()
        {
            Assert.IsTrue(_converter.ConvertToString(null, null, null) == "-");
        }
    }
}
