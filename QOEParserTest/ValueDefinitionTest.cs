using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser.Element;
using QOEParser.Parsers;

namespace QOEParserTest
{
    [TestClass]
    public class ValueDefinitionTest
    {
        XMLParser xmlParser;
        [TestInitialize()]
        public void Initialize()
        {
            xmlParser = new XMLParser();
        }

        [TestMethod]
        public void Test_SingleValue_Definition_OK()
        {
            SingleValue expected = new SingleValue();
            expected.Name = "ola testing ah";
            expected.Value = "11223355";
            expected.Length = expected.Value.Length / 2;
            
            XElement XE = new XElement("SingleValue");
            XE.Add(new XAttribute("Length", expected.Length.ToString()));
            XE.Add(new XAttribute("Default", expected.Value));
            XE.Add(new XAttribute("Name", expected.Name));


            ValueItem result = xmlParser.getItem(XE);

            Assert.IsInstanceOfType(result, typeof(SingleValue));

            SingleValue res = (SingleValue)result;
            Assert.AreEqual(expected.Value, res.Value);
            Assert.AreEqual(expected.Length, res.Length);
            Assert.AreEqual(expected.Name, res.Name);           

        }
        [TestMethod]
        public void Test_TLValue_Definition_OK()
        {
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "11223355";
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            XElement XE = new XElement("TLValue");
            XE.Add(new XAttribute("Length", expected.Length.ToString()));
            XE.Add(new XAttribute("Default", expected.Value));
            XE.Add(new XAttribute("Name", expected.Name));
            XE.Add(new XAttribute("Tag", expected.Tag));


            ValueItem result = xmlParser.getItem(XE);

            Assert.IsInstanceOfType(result, typeof(TLValue));

            TLValue res = (TLValue)result;
            Assert.AreEqual(expected.Value, res.Value);
            Assert.AreEqual(expected.Length, res.Length);
            Assert.AreEqual(expected.Name, res.Name);
            Assert.AreEqual(expected.Tag, res.Tag);

        }
    }
}
