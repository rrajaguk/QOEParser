using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser;
using QOEParser.Element;
using QOEParser.Parsers;
using QOEParserTest.Helper;

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

        [TestMethod]
        public void Test_ContentValue_Definition_OK()
        {
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "0C";
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            XElement XE = new XElement("TLValue");
            XE.Add(new XAttribute("Length", expected.Length.ToString()));
            XE.Add(new XAttribute("Default", expected.Value));
            XE.Add(new XAttribute("Name", expected.Name));
            XE.Add(new XAttribute("Tag", expected.Tag));
            // adding of head element
            XElement XSubElement = new XElement("NamedValue");
            XSubElement.Add(new XAttribute("Length", 1));
            XSubElement.Add(new XAttribute("Name", "Call ID"));
            XSubElement.Add(new XAttribute("Position", "1"));
            // adding of child head element
            XElement option1 = new XElement("Options", "0C");
            option1.Add(new XAttribute("Name", "EVENT_EVENT_DOWNLOAD_MT_CALL"));
            XSubElement.Add(option1);
            XElement option2 = new XElement("Options", "0D");
            option2.Add(new XAttribute("Name", "EVENT_EVENT_DOWNLOAD_CALL_CONNECTED"));
            XSubElement.Add(option2);
            //add to head
            XE.Add(XSubElement);


            List<PairResult> listOfExpected = new List<PairResult>();
            // for value1

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.TAG_TITLE + expected.Name,
                Value = expected.Tag
            });

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.LENGTH_TITLE + expected.Name,
                Value = expected.Length.ToString("X2")
            });

            listOfExpected.Add(new PairResult()
            {
                Title = "Call ID",
                Value = "0C",
                Description = "EVENT_EVENT_DOWNLOAD_MT_CALL"
            });

            PairResult[] expPair = listOfExpected.ToArray();
            TLValue res = (TLValue)xmlParser.getItem(XE); 
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);
            
            
        }
    }
}
