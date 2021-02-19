using System;
using System.Windows.Forms;

namespace imgprocessor
{
    public partial class FormNoImage : Form
    {
        public FormNoImage()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
        }

        public FormNoImage(string message, string zahlavi)
        {
            InitializeComponent();
            this.Text = zahlavi;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            linkLabel1.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}