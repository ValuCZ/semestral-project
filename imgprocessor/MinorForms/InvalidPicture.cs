using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace imgprocessor
{
    public partial class InvalidPicture : Form
    {
        public InvalidPicture(List<string> paths)
        {
            InitializeComponent();
            foreach (var kk in paths) listBox1.Items.Add(kk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}