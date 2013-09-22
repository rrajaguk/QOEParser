using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QOEParser.Element;
using QOEParser.Parsers;

namespace QOEParser
{
    public class Composer
    {
        public List<ValueItem> Vals = new List<ValueItem>();
        private XMLParser parser = new XMLParser();
        public void ParseValueDefinition(XElement xe)
        {
            Vals.Clear();
            foreach (var item in xe.Elements())
            {
                Vals.Add(parser.getItem(item));
            }

        }
        public void ParseValueInput(string inputs)
        {
            int offset = 0;
            foreach (var item in Vals)
            {
                offset = item.ParseValueInput(inputs, offset);
            }
        }
        public PairResult[] getValue(string inputs)
        {
            int dummy = 0;
            return getValue(inputs, out dummy);
        }
        public PairResult[] getValue(string inputs,out int numberOfSets)
        {
            List<PairResult> result = new List<PairResult>();
            int offset = 0;
            numberOfSets = 0;
            
            // create a detector, if after the loop the value still the same with the beginning then it means the data has finished
            bool runWhile = true;
            while (offset < inputs.Length && runWhile)
            {
                int endsDetector = offset;
                foreach (var item in Vals)
                {
                    // check the parsed value length
                    int resLength= item.ParseValueInput(inputs, offset);
                    
                    // if the length same like before means that there is no data being parse, ignore it
                    if (resLength > offset)
                    {
                        result.AddRange(item.GetValueOutput());
                    }

                    // set the offset for next reading
                    offset = resLength;
                }

                // add the number of set
                if (endsDetector != offset)
                {
                    numberOfSets++;
                }
                else
                {
                    runWhile = false;
                }
            }
            return result.ToArray();
        }
    }
}
