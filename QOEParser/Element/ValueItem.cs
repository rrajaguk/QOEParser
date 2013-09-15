using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QOEParser.Element
{
    public abstract class ValueItem
    {
        public int Length{ get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Mandatory { get; set; }
        protected bool parse(XElement element)
        {
            bool result = true;
            try
            {
                Length = int.Parse(element.Attribute("Length").Value.ToString());
                Name = element.Attribute("Name").Value;
                if (element.Attribute("Default") != null)
                {
                    Value = element.Attribute("Default").Value;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }


                return result;
        }
        public virtual bool ParseDefinition(XElement element){
            
            return parse(element);
        }
        public abstract int ParseValueInput(string val, int startingVal);
        public abstract PairResult[] GetValueOutput();
    }
}
