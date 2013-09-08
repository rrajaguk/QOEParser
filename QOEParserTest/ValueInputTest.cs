using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser.Element;

namespace QOEParserTest
{
    [TestClass]
    public class ValueInputTest
    {
        [TestMethod]
        public void Test_SingleValue_Input_OK()
        {
             SingleValue expected = new SingleValue();
            expected.Name = "ola testing ah";
            expected.Value = "22";
            expected.Length = 1;


            int resultIndex = expected.ParseValueInput("1122334455667788991122",0);

            Assert.AreEqual(2,resultIndex);
            Assert.AreEqual(expected.Value, "11");
        }

        [TestMethod]
        public void Test_TLValue_Input_OK()
        {
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "22";
            expected.Length = 2;
            expected.Tag = "44";

            int resultIndex = expected.ParseValueInput("4402336600225566", 0);

            Assert.AreEqual(8, resultIndex);
            Assert.AreEqual(expected.Value, "3366");
            Assert.AreEqual(expected.Length, 2);
        }
    }
}
