using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki
{
    public partial class TestBarcode : Form
    {
        public TestBarcode()
        {
            InitializeComponent();
        }

        private void TestBarcode_Load(object sender, EventArgs e)
        {
            LogicLayer.Barcode barcode = new LogicLayer.Barcode();
            pictureBox1.Image = barcode.GenerateBarcode(textBox1.Text);
        }
    }
}
