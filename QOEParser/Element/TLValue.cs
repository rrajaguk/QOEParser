using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QOEParser.Element
{
    public class TLValue : ValueItem
    {
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

        public override string GetValueOutput()
        {
            throw new NotImplementedException();
        }
    }
}
