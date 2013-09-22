using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QOEParser.Element;
using QOEParser.Element.Decorator;

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
		public static XElement generateFromValueDescriptionDecorator(ValueDescriptionDecorator TLV)
		{
			XElement XE = generateFromTLValue(TLV);

			//add the options
			foreach (var item in TLV.ArrayOfOptions.Values.ToList())
			{
				XElement Options1 = new XElement("NamedValue");
                Options1.Add(new XAttribute("Name", item.Name));
                Options1.Add(new XAttribute("Length", item.Length));
                Options1.Add(new XAttribute("Position", item.Position));

                foreach (var option in item.getOptions())
                {
                    // create the option
                    XElement optionElement = new XElement("Options", option.Key);
                    optionElement.Add(new XAttribute("Name", option.Value));
                    
                    //add it to options
                    Options1.Add(optionElement);                    
                }
                XE.Add(Options1);
			}

			return XE;
		}
	}
}
