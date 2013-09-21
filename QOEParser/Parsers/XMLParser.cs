using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QOEParser.Element;
using QOEParser.Element.Decorator;

namespace QOEParser.Parsers
{
    public class XMLParser
    {

        public ValueItem getItem(XElement element)
        {
            ValueItem result = null;
            
            // create the basic value
            switch (element.Name.ToString().ToLower())
            {
                case "singlevalue":
                    result = new SingleValue();
                    break;
                case "tlvalue":
                    result = new TLValue();
                    break;
            }

            //add the decorator
            foreach (var descendant in element.Descendants())
            {
                switch (descendant.Name.ToString().ToLower())
                {
                    case "namedvalue" :
                        if (result is TLValue)
                        {
                            result = new ValueDescriptionDecorator((TLValue)result);
                        }
                        break;
                    default:
                        break;

                }
            }



            if (result != null)
            {
                result.ParseDefinition(element);
            }

            return result;
        }
    }
}
