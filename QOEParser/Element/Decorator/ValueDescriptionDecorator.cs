using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QOEParser.Element.Decorator
{
    public class ValueDescriptionDecorator : TLValue
    {
        private TLValue basicClass;
        private OptionType[] ArrayOfOptions;
        public ValueDescriptionDecorator(TLValue tlvValue)
        {   
            basicClass = tlvValue;
            this.Length = tlvValue.Length;
            this.Tag = tlvValue.Tag;
            this.Value = tlvValue.Value;
            this.ArrayOfOptions = ArrayOfOptions;
        }
        public override bool ParseDefinition(System.Xml.Linq.XElement element)
        {
            bool result =   base.parse(element);
            
            // parsing of the ArrayOfOptions
            var subElement= element.Descendants("SubElement").First();
            if (subElement != null)
            {
                var NumberOfOptions = subElement.Descendants("NumberOfOptions").Count();
                if (NumberOfOptions > 0)
                {
                    ArrayOfOptions = new OptionType[NumberOfOptions];
                    int counter = 0;
                    foreach (var item in subElement.Descendants("NumberOfOptions"))
                    {
                        ArrayOfOptions[counter] = new OptionType()
                        {
                            Value = item.Value

                        };
                        counter++;
                    }
                }
            }

            return result;
        }
        public override PairResult[] GetValueOutput()
        {
            PairResult[] res = new PairResult[2 + Length];
            res[0] = new PairResult() { Title = TAG_TITLE + this.Name, Value = Tag };
            res[1] = new PairResult() { Title = LENGTH_TITLE + this.Name, Value = Length.ToString("X2") };

            int totLength = Length / 2;
            for(int i =0 ;i <totLength  ;i++){
                // traverse the ArrayOfOptions
                int pointer = 0;
                while (pointer < ArrayOfOptions.Length)
                {
                    if (Value.Substring(i * 2, 2) == ArrayOfOptions[i].Value)
                    {
                        res[2+i] = new PairResult() { Title = VALUE_TITLE + this.Name, Value = Value };
                        break;
                    }
                    pointer++;
                }
            }
            return res;
        }
    }
}
