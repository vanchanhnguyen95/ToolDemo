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
            this.txtDowload = new System.Windows.Forms.TextBox();
            this.btnBrowserDowload = new System.Windows.Forms.Button();
            this.btnDowload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PhotoGallary
            // 
            this.PhotoGallary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PhotoGallary.Location = new System.Drawing.Point(10, 182);
            this.PhotoGallary.Name = "PhotoGallary";
            this.PhotoGallary.Size = new System.Drawing.Size(495, 164);
            this.PhotoGallary.TabIndex = 7;
            // 
            // BrowseMultipleButton
            // 
            this.BrowseMultipleButton.Location = new System.Drawing.Point(375, 131);
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
            this.txtResult.Location = new System.Drawing.Point(68, 138);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(285, 20);
            this.txtResult.TabIndex = 8;
            // 
            // txtDowload
            // 
            this.txtDowload.Location = new System.Drawing.Point(68, 64);
            this.txtDowload.Name = "txtDowload";
            this.txtDowload.Size = new System.Drawing.Size(285, 20);
            this.txtDowload.TabIndex = 9;
            // 
            // btnBrowserDowload
            // 
            this.btnBrowserDowload.Location = new System.Drawing.Point(375, 64);
            this.btnBrowserDowload.Name = "btnBrowserDowload";
            this.btnBrowserDowload.Size = new System.Drawing.Size(130, 23);
            this.btnBrowserDowload.TabIndex = 10;
            this.btnBrowserDowload.Text = "BrowserDowload";
            this.btnBrowserDowload.UseVisualStyleBackColor = true;
            this.btnBrowserDowload.Click += new System.EventHandler(this.btnBrowserDowload_Click);
            // 
            // btnDowload
            // 
            this.btnDowload.Location = new System.Drawing.Point(375, 94);
            this.btnDowload.Name = "btnDowload";
            this.btnDowload.Size = new System.Drawing.Size(130, 23);
            this.btnDowload.TabIndex = 11;
            this.btnDowload.Text = "Dowload";
            this.btnDowload.UseVisualStyleBackColor = true;
            this.btnDowload.Click += new System.EventHandler(this.btnDowload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 366);
            this.Controls.Add(this.btnDowload);
            this.Controls.Add(this.btnBrowserDowload);
            this.Controls.Add(this.txtDowload);
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
        private System.Windows.Forms.TextBox txtDowload;
        private System.Windows.Forms.Button btnBrowserDowload;
        private System.Windows.Forms.Button btnDowload;
    }
}

