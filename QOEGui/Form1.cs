using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using QOEParser;

namespace QOEGui
{
    public partial class Form1 : Form
    {
        Composer composer;
        public Form1()
        {
            InitializeComponent();
            composer = new Composer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XElement definition = XElement.Load(Application.StartupPath + @"\QOE_Sample.xml");
            composer.ParseValueDefinition(definition);
            PairResult[] res = composer.getValue(ValueTextBox.Text);
            dataGridView.DataSource = res;

        }
    }
}
