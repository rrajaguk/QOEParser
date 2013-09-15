using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QOEParser.Element
{
    public class TLValue : ValueItem
    {
        public const string TAG_TITLE = "Tag of ";
        public const string LENGTH_TITLE = "Length of ";
        public const string VALUE_TITLE = "Value of ";

        public string Tag { get; set; }
       
        public override bool ParseDefinition(System.Xml.Linq.XElement element)
        {
            bool result = this.parse(element);
            Tag = element.Attribute("Tag").Value;

            return result;
        }

        public override int ParseValueInput(string val, int startingVal)
        {
            // check is there any tag
            if (val.Substring(startingVal, 2) != Tag)
            {
                return startingVal;
            }
            Length = int.Parse(val.Substring(startingVal + 2, 2));
            Value = val.Substring(startingVal + 4, Length * 2);

            return startingVal + 4 + (Length*2);
        }

        public override PairResult[] GetValueOutput()
        {
            PairResult[] res = new PairResult[3];
            res[0] = new PairResult() { Title = TAG_TITLE + this.Name, Value = Tag };
            res[1] = new PairResult() { Title = LENGTH_TITLE + this.Name, Value = Length.ToString("X2")};
            res[2] = new PairResult() { Title = VALUE_TITLE+ this.Name, Value = Value};

            return res;
        }
    }
}
