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
