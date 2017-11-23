using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataUtilities.Csv;

namespace NinjaUVSTest
{
    [TestClass]
    public class TestNanNullFloatConverter
    {
        private readonly NanNullFloatConverter _converter = new NanNullFloatConverter();

        [TestMethod]
        public void Float_ConvertFromString_NullInputShouldReturnNull()
        {
            Assert.IsNull(_converter.ConvertFromString(null, null, null));
        }

        [TestMethod]
        public void Float_ConvertFromString_CommaInputShouldSucceed()
        {
            var value = (float?)_converter.ConvertFromString("1,5", null, null);
            Assert.IsTrue(value == 1.5f);
        }

        [TestMethod]
        public void Float_ConvertFromString_PointInputShouldSucceed()
        {
            var value = (float?)_converter.ConvertFromString("1.5", null, null);
            Assert.IsTrue(value == 1.5f);
        }

        [TestMethod]
        public void Float_ConvertFromString_InvalidOrEmptyInputShouldReturnNull()
        {
            Assert.IsNull(_converter.ConvertFromString("-", null, null));

            Assert.IsNull(_converter.ConvertFromString(string.Empty, null, null));
        }

        [TestMethod]
        public void Float_ConvertToString_ValidInputshouldSucceed()
        {
            var value = 1.5f;
            Assert.IsTrue(_converter.ConvertToString(value, null, null) == value.ToString());
        }

        [TestMethod]
        public void Float_ConvertToString_NullInputShouldReturnDash()
        {
            Assert.IsTrue(_converter.ConvertToString(null, null, null) == "-");
        }
    }
}
