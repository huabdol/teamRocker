using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SearchLibraryUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_True()
        {
            string[] s = { "abc", "pqr" };
            List<String> l = new List<string>{ "abc", "def" };

            bool found=SearchLibrary.SearchLibrary.IsKeywordInList(s, l);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void Test_False()
        {
            string[] s = { "abc", "def" };
            List<String> l = new List<string> { "xyz", "pqr" };

            bool found = SearchLibrary.SearchLibrary.IsKeywordInList(s, l);
            Assert.IsFalse(found);
        }

    }
}
