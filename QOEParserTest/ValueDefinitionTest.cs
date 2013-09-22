using System;
using System.Collections.Generic;
using System.Text;
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
        public void NamedValue_AllNamed_OK()
        {
            //TLV Object Creation
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "0C";
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            // XML definition Creation
            XElement XE = DefineXMLForTLValue_def_ok(expected);

            // set the expected result
            PairResult[] expPair = DefineListExpectedforTLValue_def_ok(expected).ToArray();
            
            TLValue res = (TLValue)xmlParser.getItem(XE); 
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);
            
            
        }

        [TestMethod]
        public void NamedValue_OneNamed_1Byte_OK()
        {
            //TLV Object Creation
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "000C";
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            // XML definition Creation
            XElement XE = DefineXMLForTLValue_mix_def_ok(expected,1);

            // set the expected result
            PairResult[] expPair = DefineListExpectedforTLValue_mix_def_ok(expected,1).ToArray();

            TLValue res = (TLValue)xmlParser.getItem(XE);
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);


        }

        [TestMethod]
        public void NamedValue_OneNamed_2Byte_OK()
        {
            //TLV Object Creation
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "000C0C";
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            // XML definition Creation
            XElement XE = DefineXMLForTLValue_mix_def_ok(expected,2);

            // set the expected result
            PairResult[] expPair = DefineListExpectedforTLValue_mix_def_ok(expected,2).ToArray();

            TLValue res = (TLValue)xmlParser.getItem(XE);
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);


        }

        [TestMethod]
        public void NamedValue_AllNamed_Less1Byte_OK()
        {
            //TLV Object Creation
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "0C0C";
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            // XML definition Creation
            XElement XE = DefineXMLForTLValue_def_Multi(expected,3);

            // set the expected result
            PairResult[] expPair = DefineListExpectedforTLValue_mix_def_multi_ok(expected, 2).ToArray();

            TLValue res = (TLValue)xmlParser.getItem(XE);
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);


        }
        [TestMethod]
        public void NamedValue_AllNamed_LessNByte_OK()
        {
            int numberOfData = 100;
            int NumberOfMissingData = 2;
            
            //TLV Object Creation
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = repeat("0C",NumberOfMissingData);
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            // XML definition Creation
            XElement XE = DefineXMLForTLValue_def_Multi(expected, numberOfData- NumberOfMissingData );

            // set the expected result
            PairResult[] expPair = DefineListExpectedforTLValue_mix_def_multi_ok(expected, NumberOfMissingData).ToArray();

            TLValue res = (TLValue)xmlParser.getItem(XE);
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);

        }


        [TestMethod]
        public void NamedValue_AllNamed_Residue_OK()
        {
            string residueVal = "0B0B";
            string residueDesc = "this is residue";

            //TLV Object Creation
            TLValue expected = new TLValue();
            expected.Name = "ola testing ah";
            expected.Value = "0C"+residueVal;
            expected.Length = expected.Value.Length / 2;
            expected.Tag = "0A";

            // XML definition Creation
            XElement XE = DefineXMLForTLValue_def_ok_residued(expected,residueDesc);

            // set the expected result
            PairResult[] expPair = DefineListExpectedforTLValue_def_ok_residued(expected,residueVal,residueDesc).ToArray();

            TLValue res = (TLValue)xmlParser.getItem(XE);
            PairResult[] resPair = res.GetValueOutput();

            //validate the result
            ComparisonHelper.Compare(expPair, resPair);


        }

        private XElement DefineXMLForTLValue_def_ok_residued(TLValue expected,string residueDesc)
        {
            XElement result = DefineXMLForTLValue_def_ok(expected);

            XElement XResidueElement = new XElement("NamedValue");
            XResidueElement.Add(new XAttribute("Length", "*"));
            XResidueElement.Add(new XAttribute("Name", residueDesc));
            XResidueElement.Add(new XAttribute("Position", "2"));
            
            //add the residue
            result.Add(XResidueElement);


            return result;
        }

        private List<PairResult> DefineListExpectedforTLValue_def_ok_residued(TLValue expected,string residueValue,string residueDesc)
        {
            List<PairResult> result = DefineListExpectedforTLValue_def_ok(expected);

            // add the residue;
            result.Add(new PairResult()
            {
                Title = residueDesc,
                Value = residueValue
            });

            return result;

                
        }

        private List<PairResult> DefineListExpectedforTLValue_mix_def_multi_ok(TLValue expected, int p)
        {
            List<PairResult> listOfExpected = new List<PairResult>();
            // for value_basicTLV_1
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

            for (int i = 0; i < p; i++)
            {
                listOfExpected.Add(new PairResult()
                {
                    Title = "Call ID"+i,
                    Value = "0C",
                    Description = "EVENT_EVENT_DOWNLOAD_MT_CALL"
                });                
            }
            return listOfExpected;
        }

        private XElement DefineXMLForTLValue_def_Multi(TLValue expected, int p)
        {
            XElement XE = new XElement("TLValue");
            XE.Add(new XAttribute("Length", expected.Length.ToString()));
            XE.Add(new XAttribute("Default", expected.Value));
            XE.Add(new XAttribute("Name", expected.Name));
            XE.Add(new XAttribute("Tag", expected.Tag));

            for (int i = 0; i < p; i++)
            {             
                // adding of head element
                XElement XSubElement = new XElement("NamedValue");
                XSubElement.Add(new XAttribute("Length", 1));
                XSubElement.Add(new XAttribute("Name", "Call ID"+i));
                XSubElement.Add(new XAttribute("Position",1+i));
                // adding of child head element
                XElement option1 = new XElement("Options", "0C");
                option1.Add(new XAttribute("Name", "EVENT_EVENT_DOWNLOAD_MT_CALL"));
                XSubElement.Add(option1);
                XElement option2 = new XElement("Options", "0D");
                option2.Add(new XAttribute("Name", "EVENT_EVENT_DOWNLOAD_CALL_CONNECTED"));
                XSubElement.Add(option2);
                //add to head
                XE.Add(XSubElement);   
            }
            return XE;
        }





        private static List<PairResult> DefineListExpectedforTLValue_def_ok(TLValue expected)
        {
            List<PairResult> listOfExpected = new List<PairResult>();
            // for value_basicTLV_1
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
            return listOfExpected;
        }

        private static XElement DefineXMLForTLValue_def_ok(TLValue expected)
        {
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
            return XE;
        }

        private static List<PairResult> DefineListExpectedforTLValue_mix_def_ok(TLValue expected,int bytenumber)
        {
            List<PairResult> listOfExpected = new List<PairResult>();
            // for value_basicTLV_1
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
                Value = "00"
            });
            listOfExpected.Add(new PairResult()
            {
                Title = "Event ID",
                Value = repeat("0C",bytenumber),
                Description = "EVENT_EVENT_DOWNLOAD_MT_CALL"
            });
            return listOfExpected;
        }

        private static XElement DefineXMLForTLValue_mix_def_ok(TLValue expected,int byteNumber)
        {
            XElement XE = new XElement("TLValue");
            XE.Add(new XAttribute("Length", expected.Length.ToString()));
            XE.Add(new XAttribute("Default", expected.Value));
            XE.Add(new XAttribute("Name", expected.Name));
            XE.Add(new XAttribute("Tag", expected.Tag));

            //adding of first element (no options)
            XElement XSubElement_Position1 = new XElement("NamedValue");
            XSubElement_Position1.Add(new XAttribute("Length", "1"));
            XSubElement_Position1.Add(new XAttribute("Name", "Call ID"));
            XSubElement_Position1.Add(new XAttribute("Position", "1"));
            //add to head
            XE.Add(XSubElement_Position1);




            // adding of Second element (with options)
            XElement XSubElement_Position2 = new XElement("NamedValue");
            XSubElement_Position2.Add(new XAttribute("Length", byteNumber));
            XSubElement_Position2.Add(new XAttribute("Name", "Event ID"));
            XSubElement_Position2.Add(new XAttribute("Position","2"));
            
            // adding of child head element
            XElement option1 = new XElement("Options", repeat("0C",byteNumber));
            option1.Add(new XAttribute("Name", "EVENT_EVENT_DOWNLOAD_MT_CALL"));
            XSubElement_Position2.Add(option1);
            XElement option2 = new XElement("Options", repeat("0D",byteNumber));
            option2.Add(new XAttribute("Name", "EVENT_EVENT_DOWNLOAD_CALL_CONNECTED"));
            XSubElement_Position2.Add(option2);
            
            //add to head
            XE.Add(XSubElement_Position2);


            return XE;
        }

        public static string repeat(string p, int byteNumber)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < byteNumber; i++)
            {
                SB.Append(p);
            }

            return SB.ToString();
        }
    }
}
