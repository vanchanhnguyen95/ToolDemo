namespace OpenFileDialogueSample
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.PhotoGallary = new System.Windows.Forms.FlowLayoutPanel();
            this.BrowseMultipleButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PhotoGallary
            // 
            this.PhotoGallary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PhotoGallary.Location = new System.Drawing.Point(10, 133);
            this.PhotoGallary.Name = "PhotoGallary";
            this.PhotoGallary.Size = new System.Drawing.Size(495, 213);
            this.PhotoGallary.TabIndex = 7;
            // 
            // BrowseMultipleButton
            // 
            this.BrowseMultipleButton.Location = new System.Drawing.Point(375, 100);
            this.BrowseMultipleButton.Name = "BrowseMultipleButton";
            this.BrowseMultipleButton.Size = new System.Drawing.Size(130, 27);
            this.BrowseMultipleButton.TabIndex = 6;
            this.BrowseMultipleButton.Text = "Browse Multiple Files";
            this.BrowseMultipleButton.UseVisualStyleBackColor = true;
            this.BrowseMultipleButton.Click += new System.EventHandler(this.BrowseMultipleButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(68, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(285, 20);
            this.textBox1.TabIndex = 5;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(375, 20);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(130, 27);
            this.BrowseButton.TabIndex = 4;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(68, 73);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(285, 20);
            this.txtResult.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 366);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.PhotoGallary);
            this.Controls.Add(this.BrowseMultipleButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BrowseButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel PhotoGallary;
        private System.Windows.Forms.Button BrowseMultipleButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BrowseButton;
        public System.Windows.Forms.TextBox txtResult;
    }
}

