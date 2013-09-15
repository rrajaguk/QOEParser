using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QOEParser.Element
{
    public class SingleValue : ValueItem
    {

        
        public override bool ParseDefinition(System.Xml.Linq.XElement element)
        {
            return this.parse(element);
        }

        public override int ParseValueInput(string val, int startingVal)
        {
            this.Value = val.Substring(startingVal, Length * 2);

            return startingVal + (Length * 2);
        }

        public override PairResult[] GetValueOutput()
        {
            throw new NotImplementedException();
        }
    }
}
