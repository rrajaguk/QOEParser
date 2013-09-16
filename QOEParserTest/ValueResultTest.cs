using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser;
using QOEParser.Element;
using QOEParserTest.Helper;

namespace QOEParserTest
{
    [TestClass]
    public class ValueResultTest
    {

        [TestMethod]
        public void Test_Value_1Round_OK()
        {
            // value in object
            TLValue value1 = new TLValue();
            value1.Name = "val 1";
            value1.Value = "0022";
            value1.Length = 2;
            value1.Tag = "04";

            TLValue value2 = new TLValue();
            value2.Name = "val 2";
            value2.Value = string.Empty;
            // purposely put wrong length 
            value2.Length = 10;
            value2.Tag = "09";


            Composer composer = new Composer();
            composer.Vals.Add(value1);
            composer.Vals.Add(value2);


            string testVal = "040222330902445533";
            composer.ParseValueInput(testVal);

            PairResult[] expected = StaticValue.Get2PairResults(value1, value2);
            PairResult[] result = composer.getValue(testVal);


            ComparisonHelper.Compare(expected, result);
            
            // for value2
            Assert.AreEqual("2233", value1.Value);
            Assert.AreEqual("4455", value2.Value);
            
        }

        [TestMethod]
        public void Test_Value_2Round_OK()
        {

        }
    }
}
