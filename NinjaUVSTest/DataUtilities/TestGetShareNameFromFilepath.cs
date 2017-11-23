using System.Linq;
using DataUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NinjaUVSTest.DataUtilities
{
    [TestClass]
    public class TestGetShareNameFromFilepath
    {
        private const string DummyPath = @"C:\Dummy\Subfolder\";
        private const string DummyEnding = @"-2000-01-01.xlsx";

        [TestMethod]
        public void ExtractShareName_AlphabeticCharactersShouldSucceed()
        {
            const string name = @"NXe";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + name + DummyEnding) == name);

            const string swedishName = @"NXÅ";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + swedishName + DummyEnding) == swedishName);
        }

        [TestMethod]
        public void ExtractShareName_IncludeBlankspaceShouldSucceed()
        {
            const string name = @"NX ExK B";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + name + DummyEnding) == name);
        }

        [TestMethod]
        public void ExtractShareName_IncludeAmpersandShouldSucceed()
        {
            const string name = @"NX&EB";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + name + DummyEnding) == name);
        }

        [TestMethod]
        public void ExtractShareName_AlphanumericShouldSucceed()
        {
            const string name = @"N5XG";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + name + DummyEnding) == name);
        }

        [TestMethod]
        public void ExtractShareName_CombinationsThatShouldWork()
        {
            //Verify that combining ampersand and blankspace works
            const string mixedAmpersandBlankspace = @"NX&E B";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + mixedAmpersandBlankspace + DummyEnding) == mixedAmpersandBlankspace);

            //Verify by mixing alphanumeric characters with ampersand and blankspace.
            const string alphanumericMixed = @"N5 G&4 B";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + alphanumericMixed + DummyEnding) == alphanumericMixed);
        }

        [TestMethod]
        public void ExtractShareName_DashShouldBreak()
        {
            //Verify that a dash works as a break character
            const string dashIsBreaking = @"NX&E-B";
            Assert.IsTrue(DataLoader.GetShareNameFromFile(DummyPath + dashIsBreaking + DummyEnding) == dashIsBreaking.Split('-').First());
        }
    }
}
