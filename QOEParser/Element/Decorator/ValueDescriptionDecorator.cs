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
        private Dictionary<int, OptionContainer> ArrayOfOptions;
        public ValueDescriptionDecorator(TLValue tlvValue)
        {   
            basicClass = tlvValue;
            this.Length = tlvValue.Length;
            this.Tag = tlvValue.Tag;
            this.Value = tlvValue.Value;
            ArrayOfOptions = new Dictionary<int, OptionContainer>();
        }
        public override bool ParseDefinition(System.Xml.Linq.XElement element)
        {
            bool result =   base.ParseDefinition(element);
            
            // parsing of the namedValue
            var ListOfNamedValues = element.Descendants("NamedValue");
            foreach (var namedValueDefinition in ListOfNamedValues)
            {
                //create the NamedValue container
                int namedValuePosition = int.Parse(namedValueDefinition.Attribute("Position").Value);
                OptionContainer opsContainer = new OptionContainer();
                opsContainer.Name = namedValueDefinition.Attribute("Name").Value;
                opsContainer.Length = int.Parse(namedValueDefinition.Attribute("Length").Value);

                //fill up the created container
                var listOfOptions = namedValueDefinition.Descendants("Options");
                foreach (var option in listOfOptions)
                {
                    opsContainer.insertOption(option.Value, option.Attribute("Name").Value);
                }

                // add the container to array of options
                ArrayOfOptions.Add(namedValuePosition, opsContainer);

            }
           

            return result;
        }
        public override PairResult[] GetValueOutput()
        {
            List<PairResult> res = new List<PairResult>();

            // add the Tag and Length
            res.Add( new PairResult() { Title = TAG_TITLE + this.Name, Value = Tag });
            res.Add( new PairResult() { Title = LENGTH_TITLE + this.Name, Value = Length.ToString("X2") });

            // fill up the array
            int counter = 0;
            while (counter < Length) { 

                OptionContainer currentOpsContainer = ArrayOfOptions[counter + 1];

                string val = Value.Substring(2 * counter, currentOpsContainer.Length *2);
                string description = currentOpsContainer.getDescription(val);
                res.Add (new PairResult() { Title = currentOpsContainer.Name, Value = val, Description = description });

                counter += currentOpsContainer.Length;
            }

            return res.ToArray();
        }
    }
}
