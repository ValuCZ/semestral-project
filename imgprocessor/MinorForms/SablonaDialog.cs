using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imgprocessor
{
    public partial class SablonaDialog : Form
    {
        public string nameFile;

        public SablonaDialog()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            label2.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text.Length == 0)
            {
                pictureBox1.Visible = true;
                label2.Text = "název nemůže být prázdný";
                label2.Visible = true;
                return;
            }
            if (Convertot.HasIlegalChar(textBox1.Text))
            {
                pictureBox1.Visible = true;
                label2.Text = "název obsahuje nepovolené znaky";
                label2.Visible = true;
                return;
            }
            string location = textBox1.Text + ".xml";
            if (File.Exists(location))
            {
                pictureBox1.Visible = true;
                label2.Visible = true;
                return;
            }

            nameFile = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }
    }
}