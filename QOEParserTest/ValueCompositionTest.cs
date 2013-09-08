using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser;
using QOEParser.Element;

namespace QOEParserTest
{
    [TestClass]
    public class ValueCompositionTest
    {
        [TestMethod]
        public void Test_Composer_SingleValue_Definition_OK()
        {
            // value in object
            SingleValue value1 = new SingleValue();
            value1.Name = "val 1";
            value1.Value = "0022";
            value1.Length = 2;

            SingleValue value2 = new SingleValue();
            value2.Name = "val 2";
            value2.Value = string.Empty;
            value2.Length = 2;


            // value definition in XML
            XElement definition = new XElement("Head");
            definition.Add(XElementHelper.generateFromSingleValue(value1));
            definition.Add(XElementHelper.generateFromSingleValue(value2));


            Composer composer = new Composer();
            composer.ParseValueDefinition(definition);

            Assert.AreEqual(2, composer.Vals.Count);
            AreEqualSingleValue(value1, (SingleValue)composer.Vals[0]);
            AreEqualSingleValue(value2, (SingleValue)composer.Vals[1]);

        }

        [TestMethod]
        public void Test_Composter_SingleValue_Input_OK()
        {
            // value in object
            SingleValue value1 = new SingleValue();
            value1.Name = "val 1";
            value1.Value = "0022";
            value1.Length = 2;

            SingleValue value2 = new SingleValue();
            value2.Name = "val 2";
            value2.Value = string.Empty;
            value2.Length = 2;

            Composer composer = new Composer();
            composer.Vals.Add(value1);
            composer.Vals.Add(value2);

            composer.ParseValueInput("2233445533");

            Assert.AreEqual("2233", value1.Value);
            Assert.AreEqual("4455", value2.Value);
        }

        [TestMethod]
        public void Test_Composer_TLValue_Definition_OK()
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
            value2.Length = 2;
            value2.Tag = "09";


            // value definition in XML
            XElement definition = new XElement("Head");
            definition.Add(XElementHelper.generateFromTLValue(value1));
            definition.Add(XElementHelper.generateFromTLValue(value2));


            Composer composer = new Composer();
            composer.ParseValueDefinition(definition);

            Assert.AreEqual(2, composer.Vals.Count);
            AreEqualTLValue(value1, (TLValue)composer.Vals[0]);
            AreEqualTLValue(value2, (TLValue)composer.Vals[1]);

        }

        [TestMethod]
        public void Test_Composter_TLValue_Input_OK()
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

            composer.ParseValueInput("040222330902445533");

            Assert.AreEqual("2233", value1.Value);
            Assert.AreEqual("4455", value2.Value);
        }

        private void AreEqualSingleValue(SingleValue expected, SingleValue result)
        {
            Assert.IsInstanceOfType(result, typeof(SingleValue));
            Assert.AreEqual(expected.Length, result.Length);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Value, result.Value);
        }
        private void AreEqualTLValue(TLValue expected, TLValue result)
        {
            Assert.IsInstanceOfType(result, typeof(TLValue));
            Assert.AreEqual(expected.Length, result.Length);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Value, result.Value);
            Assert.AreEqual(expected.Tag, result.Tag);
        }
    }
}
