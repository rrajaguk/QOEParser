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
    public class ComposerParserTest
    {
        Composer composer;
        TLValue value1;
        TLValue value2;

        [TestInitialize]
        public void valInit()
        {
            // value in object
            value1 = new TLValue();
            value1.Name = "val 1";
            value1.Value = "0022";
            value1.Length = 2;
            value1.Tag = "04";

            value2 = new TLValue();
            value2.Name = "val 2";
            value2.Value = string.Empty;
            // purposely put wrong length 
            value2.Length = 10;
            value2.Tag = "09";


            composer = new Composer();
            composer.Vals.Add(value1);
            composer.Vals.Add(value2);
        }

        [TestMethod]
        public void Composer_1Sets_2TLValue_OK()
        {
            string testVal = "0402223309024455";
            composer.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value1, value2);
            int numberOfExpectedSets = 1;
            int numberOfResultSets;
            PairResult[] result = composer.getValue(testVal,out numberOfResultSets);

            // exersize it!
            Assert.AreEqual(numberOfExpectedSets, numberOfResultSets);
            ComparisonHelper.Compare(expected, result);
                        
        }

        [TestMethod]
        public void Composer_1Sets_2TLValue_ExtraData()
        {

            string testVal = "0402223309024455" + "4455669988553366";
            composer.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value1, value2); 
            int numberOfExpectedSets =1;
            int numberOfResultSets;
            PairResult[] result = composer.getValue(testVal,out numberOfResultSets);

            // exersize it!
            Assert.AreEqual(numberOfExpectedSets, numberOfResultSets);
            ComparisonHelper.Compare(expected, result);            


        }

        [TestMethod]
        public void Composer_2Sets_2TLValue_OK()
        {
            string testVal = "04022233090244550402223309024455";
            composer.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value1, value2, value1, value2); 
            int numberOfExpectedSets = 2;
            int numberOfResultSets;
            PairResult[] result = composer.getValue(testVal,out numberOfResultSets);

            // exersize it!
            Assert.AreEqual(numberOfExpectedSets, numberOfResultSets);
            ComparisonHelper.Compare(expected, result);
           
        }
        [TestMethod]
        public void Composer_2Sets_2TLValue_ExtraData()
        {
            string testVal = "04022233090244550402223309024455" + "112233665544222";
            composer.ParseValueInput(testVal);

            PairResult[] expected = Get2PairResults(value1, value2, value1, value2);
            PairResult[] result = composer.getValue(testVal);

            // exersize it!
            ComparisonHelper.Compare(expected, result);

        }

        public static PairResult[] Get2PairResults(params TLValue[] values)
        {
            List<PairResult> listOfExpected = new List<PairResult>();

            foreach (var value in values)
            {
                // for value1
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
