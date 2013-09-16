using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QOEParser;
using QOEParser.Element;

namespace QOEParserTest.Helper
{
    public class StaticValue
    {

        public static PairResult[] Get2PairResults(TLValue value1, TLValue value2)
        {
            List<PairResult> listOfExpected = new List<PairResult>();
            // for value1

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.TAG_TITLE + value1.Name,
                Value = "04"
            });

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.LENGTH_TITLE + value1.Name,
                Value = "02"
            });

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.VALUE_TITLE + value1.Name,
                Value = "2233"
            });

            // for value2
            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.TAG_TITLE + value2.Name,
                Value = "09"
            });

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.LENGTH_TITLE + value2.Name,
                Value = "02"
            });

            listOfExpected.Add(new PairResult()
            {
                Title = TLValue.VALUE_TITLE + value2.Name,
                Value = "4455"
            });

            return listOfExpected.ToArray();
        }
    }
}
