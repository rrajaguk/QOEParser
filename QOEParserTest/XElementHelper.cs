using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QOEParser.Element;

namespace QOEParserTest
{
    public class XElementHelper
    {
        public static XElement generateFromSingleValue(SingleValue SV)
        {
            XElement XE = generateFromValue(SV);
            XE.Name = "SingleValue";
            return XE;
        }
        public static XElement generateFromTLValue(TLValue TV)
        {
            XElement XE = generateFromValue(TV);
            XE.Name = "TLValue";
            XE.Add(new XAttribute("Tag", TV.Tag));
            return XE;
        }

        private static XElement generateFromValue(ValueItem TV)
        {
            XElement XE = new XElement("Value");
            XE.Add(new XAttribute("Name", TV.Name));
            XE.Add(new XAttribute("Length", TV.Length));
            if (TV.Value != null)
            {
                XE.Add(new XAttribute("Default", TV.Value));            
            }

            return XE;
        }
    }
}
