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
            List<PairResult> result = new List<PairResult>();
            int offset = 0;
            foreach (var item in Vals)
            {
                offset = item.ParseValueInput(inputs, offset);
                result.AddRange(item.GetValueOutput());
            }
            return result.ToArray();
        }
    }
}
