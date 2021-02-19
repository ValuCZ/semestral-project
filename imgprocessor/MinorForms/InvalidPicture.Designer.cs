namespace imgprocessor
{
    partial class InvalidPicture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gradientpanel1 = new imgprocessor.Gradientpanel();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gradientpanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gradientpanel1
            // 
            this.gradientpanel1.CollorTop = System.Drawing.Color.DarkRed;
            this.gradientpanel1.ColorBottom = System.Drawing.Color.DarkOrange;
            this.gradientpanel1.Controls.Add(this.button1);
            this.gradientpanel1.Controls.Add(this.listBox1);
            this.gradientpanel1.Controls.Add(this.label1);
            this.gradientpanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientpanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientpanel1.Name = "gradientpanel1";
            this.gradientpanel1.Size = new System.Drawing.Size(602, 333);
            this.gradientpanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Gabriola", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(173, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nepodařilo se načíst:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(53, 76);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(488, 184);
            this.listBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "zavřít";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InvalidPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 333);
            this.ControlBox = false;
            this.Controls.Add(this.gradientpanel1);
            this.Name = "InvalidPicture";
            this.Text = "Loading error";
            this.gradientpanel1.ResumeLayout(false);
            this.gradientpanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gradientpanel gradientpanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}