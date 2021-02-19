namespace imgprocessor
{
    partial class FormNoImage
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
            this.button1 = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.gradientpanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gradientpanel1
            // 
            this.gradientpanel1.CollorTop = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gradientpanel1.ColorBottom = System.Drawing.Color.OrangeRed;
            this.gradientpanel1.Controls.Add(this.button1);
            this.gradientpanel1.Controls.Add(this.linkLabel1);
            this.gradientpanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientpanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientpanel1.Name = "gradientpanel1";
            this.gradientpanel1.Size = new System.Drawing.Size(431, 170);
            this.gradientpanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(181, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "zavřít";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(0, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(438, 46);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "NEZVOLEN OBRÁZEK";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // FormNoImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 170);
            this.Controls.Add(this.gradientpanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNoImage";
            this.ShowIcon = false;
            this.Text = "No Image ERROR";
            this.gradientpanel1.ResumeLayout(false);
            this.gradientpanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Gradientpanel gradientpanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}