
namespace FormFileSpeed
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
            this.groupBoxUpload = new System.Windows.Forms.GroupBox();
            this.txtUpload = new System.Windows.Forms.TextBox();
            this.lblUpload = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtDowload = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.lblDowload = new System.Windows.Forms.Label();
            this.richTxtUpload = new System.Windows.Forms.RichTextBox();
            this.btnDowload = new System.Windows.Forms.Button();
            this.richTxtDowload = new System.Windows.Forms.RichTextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBoxUpload.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.Controls.Add(this.richTxtUpload);
            this.groupBoxUpload.Controls.Add(this.btnUpload);
            this.groupBoxUpload.Controls.Add(this.lblUpload);
            this.groupBoxUpload.Controls.Add(this.txtUpload);
            this.groupBoxUpload.Location = new System.Drawing.Point(31, 69);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Size = new System.Drawing.Size(510, 313);
            this.groupBoxUpload.TabIndex = 0;
            this.groupBoxUpload.TabStop = false;
            this.groupBoxUpload.Text = "Upload File";
            // 
            // txtUpload
            // 
            this.txtUpload.Location = new System.Drawing.Point(6, 71);
            this.txtUpload.Multiline = true;
            this.txtUpload.Name = "txtUpload";
            this.txtUpload.Size = new System.Drawing.Size(335, 20);
            this.txtUpload.TabIndex = 0;
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(121, 34);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(91, 13);
            this.lblUpload.TabIndex = 1;
            this.lblUpload.Text = "Upload File Name";
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUpload.Location = new System.Drawing.Point(347, 60);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(157, 31);
            this.btnUpload.TabIndex = 14;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtDowload
            // 
            this.txtDowload.Location = new System.Drawing.Point(6, 71);
            this.txtDowload.Multiline = true;
            this.txtDowload.Name = "txtDowload";
            this.txtDowload.Size = new System.Drawing.Size(349, 20);
            this.txtDowload.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTxtDowload);
            this.groupBox1.Controls.Add(this.btnDowload);
            this.groupBox1.Controls.Add(this.btnBrowser);
            this.groupBox1.Controls.Add(this.lblDowload);
            this.groupBox1.Controls.Add(this.txtDowload);
            this.groupBox1.Location = new System.Drawing.Point(565, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 313);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload File";
            // 
            // btnBrowser
            // 
            this.btnBrowser.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBrowser.Location = new System.Drawing.Point(361, 16);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(143, 31);
            this.btnBrowser.TabIndex = 14;
            this.btnBrowser.Text = "Browser";
            this.btnBrowser.UseVisualStyleBackColor = false;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // lblDowload
            // 
            this.lblDowload.AutoSize = true;
            this.lblDowload.Location = new System.Drawing.Point(121, 34);
            this.lblDowload.Name = "lblDowload";
            this.lblDowload.Size = new System.Drawing.Size(99, 13);
            this.lblDowload.TabIndex = 1;
            this.lblDowload.Text = "Dowload File Name";
            // 
            // richTxtUpload
            // 
            this.richTxtUpload.Location = new System.Drawing.Point(6, 111);
            this.richTxtUpload.Name = "richTxtUpload";
            this.richTxtUpload.Size = new System.Drawing.Size(498, 189);
            this.richTxtUpload.TabIndex = 15;
            this.richTxtUpload.Text = "";
            // 
            // btnDowload
            // 
            this.btnDowload.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDowload.Location = new System.Drawing.Point(361, 60);
            this.btnDowload.Name = "btnDowload";
            this.btnDowload.Size = new System.Drawing.Size(143, 31);
            this.btnDowload.TabIndex = 15;
            this.btnDowload.Text = "Download";
            this.btnDowload.UseVisualStyleBackColor = false;
            this.btnDowload.Click += new System.EventHandler(this.btnDowload_Click);
            // 
            // richTxtDowload
            // 
            this.richTxtDowload.Location = new System.Drawing.Point(6, 111);
            this.richTxtDowload.Name = "richTxtDowload";
            this.richTxtDowload.Size = new System.Drawing.Size(498, 196);
            this.richTxtDowload.TabIndex = 16;
            this.richTxtDowload.Text = "";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(926, 35);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(143, 28);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 408);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxUpload);
            this.Name = "Form1";
            this.Text = "Speed Limit";
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxUpload;
        private System.Windows.Forms.TextBox txtUpload;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtDowload;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Label lblDowload;
        private System.Windows.Forms.RichTextBox richTxtUpload;
        private System.Windows.Forms.Button btnDowload;
        private System.Windows.Forms.RichTextBox richTxtDowload;
        private System.Windows.Forms.Button btnReset;
    }
}

