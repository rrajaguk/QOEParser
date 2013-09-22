using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QOEParser;
using QOEParser.Element;
using QOEParser.Element.Decorator;

namespace QOEParserTest
{
    /// <summary>
    /// Test the composer capability to translate the XML into TLV class definition
    /// </summary>
    [TestClass]
    public class ComposerDefinitionTest
    {
        [TestMethod]
        public void Composer_SingleValue_OK()
        {
            // value in object
            SingleValue value1 = new SingleValue();
            value1.Name = "val 1";
            value1.Value = "0022";
            value1.Length = 2;

            SingleValue value2 = new SingleValue();
            value2.Name = "val 2";
            value2.Value = string.Empty;
            value2.Length = 2;


            // value definition in XML
            XElement definition = new XElement("Definition");
            definition.Add(XElementHelper.generateFromSingleValue(value1));
            definition.Add(XElementHelper.generateFromSingleValue(value2));


            Composer composer = new Composer();
            composer.ParseValueDefinition(definition);

            Assert.AreEqual(2, composer.Vals.Count);
            AreEqualSingleValue(value1, (SingleValue)composer.Vals[0]);
            AreEqualSingleValue(value2, (SingleValue)composer.Vals[1]);

        }

      

        [TestMethod]
        public void Composer_TLValue_OK()
        {
            // value in object
            TLValue value1 = new TLValue();
            value1.Name = "val 1";
            value1.Value = "0022";
            value1.Length = 2;
            value1.Tag = "04";

            TLValue value2 = new TLValue();
            value2.Name = "val 2";
            value2.Value = string.Empty;
            value2.Length = 2;
            value2.Tag = "09";


            // value definition in XML
            XElement definition = new XElement("Head");
            definition.Add(XElementHelper.generateFromTLValue(value1));
            definition.Add(XElementHelper.generateFromTLValue(value2));

            // exercise the composer
            Composer composer = new Composer();
            composer.ParseValueDefinition(definition);

            // check the result
            Assert.AreEqual(2, composer.Vals.Count);
            AreEqualTLValue(value1, (TLValue)composer.Vals[0]);
            AreEqualTLValue(value2, (TLValue)composer.Vals[1]);

        }


        [TestMethod]
        public void Composer_ValueDefinition_OK()
        {
            TLValue bareValue1 = new TLValue();
            bareValue1.Name = "val 1";
            bareValue1.Value = "0022";
            bareValue1.Length = 2;
            bareValue1.Tag = "04";

            ValueDescriptionDecorator value1= new ValueDescriptionDecorator(bareValue1);
            value1.ArrayOfOptions.Add(1, createOptions(3, 1, 1));
            value1.ArrayOfOptions.Add(2, createOptions(10, 2, 1));

            //value definition in XML
            XElement definition = new XElement("Definition");
            definition.Add(XElementHelper.generateFromValueDescriptionDecorator(value1));

            Composer composer = new Composer();
            composer.ParseValueDefinition(definition);

            // check the result
            Assert.AreEqual(1, composer.Vals.Count);
            Assert.IsInstanceOfType(composer.Vals[0], typeof(ValueDescriptionDecorator));
            AreEqualValueDescriptionDecorator(value1, (ValueDescriptionDecorator)composer.Vals[0]);
        }

        private static OptionContainer createOptions(int numberOfOption,int position,int length)
        {
            OptionContainer opContainer1 = new OptionContainer();
            opContainer1.Name = "opCon"+position;
            opContainer1.Length = length;
            opContainer1.Position = position;

            for (int i = 0; i < numberOfOption; i++)
            {
                opContainer1.insertOption("Option"+i,  ValueDefinitionTest.repeat("00",length-1) + i.ToString("X2") );                
            }

            return opContainer1;
        }

        private void AreEqualSingleValue(SingleValue expected, SingleValue result)
        {
            Assert.IsInstanceOfType(result, typeof(SingleValue));
            Assert.AreEqual(expected.Length, result.Length);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Value, result.Value);
        }
        private void AreEqualTLValue(TLValue expected, TLValue result)
        {
            Assert.IsInstanceOfType(result, typeof(TLValue));
            Assert.AreEqual(expected.Length, result.Length);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Value, result.Value);
            Assert.AreEqual(expected.Tag, result.Tag);
        }
        private void AreEqualValueDescriptionDecorator(ValueDescriptionDecorator expected, ValueDescriptionDecorator result)
        {
            AreEqualTLValue(expected, result);

            Assert.AreEqual(expected.ArrayOfOptions.Count, result.ArrayOfOptions.Count);
            
            OptionContainer[] expectedArray = expected.ArrayOfOptions.Values.ToArray<OptionContainer>();
            OptionContainer[] resultArray = result.ArrayOfOptions.Values.ToArray<OptionContainer>();


            for (int i = 0; i < expectedArray.Count(); i++)
            {
                Assert.AreEqual(expectedArray[i].Length, resultArray[i].Length);
                Assert.AreEqual(expectedArray[i].Name, resultArray[i].Name);
                Assert.AreEqual(expectedArray[i].Position, resultArray[i].Position);
            }

        }
    }
}
