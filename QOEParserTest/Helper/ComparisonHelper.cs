using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser;

namespace QOEParserTest.Helper
{
    public class ComparisonHelper
    {
        public static void Compare(PairResult[] expected, PairResult[] result)
        {
            Assert.AreEqual(expected.Count(), result.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected[i].Title, result[i].Title);
                Assert.AreEqual(expected[i].Value, result[i].Value);
                Assert.AreEqual(expected[i].Description, result[i].Description);
            }
        }
    }
}
