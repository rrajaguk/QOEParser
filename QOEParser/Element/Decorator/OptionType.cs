using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QOEParser.Element.Decorator
{
    
    public class OptionContainer
    {
        private Dictionary<string, string> Options;
        public string Name { get; set; }
        public OptionContainer()
        {
            Options = new Dictionary<string, string>();
        }
        public void insertOption(string name, string value)
        {
            Options.Add(name, value);
        }
        public string getDescription(string value)
        {
            return Options[value];
        }
    }
}
