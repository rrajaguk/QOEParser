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
    /// <summary>
    /// Test the composer capability to parse the string input
    /// </summary>
    [TestClass]
    public class ComposerParserTest
    {
        Composer composer_BasicTLV;
        TLValue value_basicTLV_1;
        TLValue value_basicTLV_2;

        [TestInitialize]
        public void valInit()
        {
            // value in object
            value_basicTLV_1 = new TLValue();
            value_basicTLV_1.Name = "val 1";
            value_basicTLV_1.Value = "0022";
            value_basicTLV_1.Length = 2;
            value_basicTLV_1.Tag = "04";

            value_basicTLV_2 = new TLValue();
            value_basicTLV_2.Name = "val 2";
            value_basicTLV_2.Value = string.Empty;
            // purposely put wrong length 
            value_basicTLV_2.Length = 10;
            value_basicTLV_2.Tag = "09";


            composer_BasicTLV = new Composer();
            composer_BasicTLV.Vals.Add(value_basicTLV_1);
            composer_BasicTLV.Vals.Add(value_basicTLV_2);
        }

        [TestMethod]
        public void Composer_1Sets_2TLValue_OK()
        {
            string testVal = "0402223309024455";
            composer_BasicTLV.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value_basicTLV_1, value_basicTLV_2);
            int numberOfExpectedSets = 1;
            int numberOfResultSets;
            PairResult[] result = composer_BasicTLV.getValue(testVal,out numberOfResultSets);

            // exersize it!
            Assert.AreEqual(numberOfExpectedSets, numberOfResultSets);
            ComparisonHelper.Compare(expected, result);
                        
        }

        [TestMethod]
        public void Composer_1Sets_2TLValue_ExtraData()
        {

            string testVal = "0402223309024455" + "4455669988553366";
            composer_BasicTLV.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value_basicTLV_1, value_basicTLV_2); 
            int numberOfExpectedSets =1;
            int numberOfResultSets;
            PairResult[] result = composer_BasicTLV.getValue(testVal,out numberOfResultSets);

            // exersize it!
            Assert.AreEqual(numberOfExpectedSets, numberOfResultSets);
            ComparisonHelper.Compare(expected, result);            


        }

        [TestMethod]
        public void Composer_2Sets_2TLValue_OK()
        {
            string testVal = "04022233090244550402223309024455";
            composer_BasicTLV.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value_basicTLV_1, value_basicTLV_2, value_basicTLV_1, value_basicTLV_2); 
            int numberOfExpectedSets = 2;
            int numberOfResultSets;
            PairResult[] result = composer_BasicTLV.getValue(testVal,out numberOfResultSets);

            // exersize it!
            Assert.AreEqual(numberOfExpectedSets, numberOfResultSets);
            ComparisonHelper.Compare(expected, result);
           
        }
        [TestMethod]
        public void Composer_2Sets_2TLValue_ExtraData()
        {
            string testVal = "04022233090244550402223309024455" + "112233665544222";
            composer_BasicTLV.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value_basicTLV_1, value_basicTLV_2, value_basicTLV_1, value_basicTLV_2);
            PairResult[] result = composer_BasicTLV.getValue(testVal);

            // exersize it!
            ComparisonHelper.Compare(expected, result);

        }

        public static PairResult[] Get2PairResults(params TLValue[] values)
        {
            List<PairResult> listOfExpected = new List<PairResult>();

            foreach (var value in values)
            {
                // for value_basicTLV_1
                listOfExpected.Add(new PairResult()
                {
                    Title = TLValue.TAG_TITLE + value.Name,
                    Value = value.Tag
                });

                listOfExpected.Add(new PairResult()
                {
                    Title = TLValue.LENGTH_TITLE + value.Name,
                    Value = value.Length.ToString("X2")
                });

                listOfExpected.Add(new PairResult()
                {
                    Title = TLValue.VALUE_TITLE + value.Name,
                    Value = value.Value
                });
            }
            return listOfExpected.ToArray();
        }
    }
}
