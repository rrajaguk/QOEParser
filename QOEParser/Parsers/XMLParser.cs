using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QOEParser.Element;

namespace QOEParser.Parsers
{
    public class XMLParser
    {

        public ValueItem getItem(XElement element)
        {
            ValueItem result = null;
            
            // create the basic value
            switch (element.Name.ToString())
            {
                case "SingleValue":
                    result = new SingleValue();
                    break;
                case "TLValue":
                    result = new TLValue();
                    break;
            }

            //add the decorator



            if (result != null)
            {
                result.ParseDefinition(element);
            }

            return result;
        }
    }
}
