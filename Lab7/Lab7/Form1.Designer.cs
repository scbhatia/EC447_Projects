namespace Lab7
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
            this.label1 = new System.Windows.Forms.Label();
            this.FN_text = new System.Windows.Forms.TextBox();
            this.openFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_key = new System.Windows.Forms.TextBox();
            this.encryptFile = new System.Windows.Forms.Button();
            this.decryptFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name: ";
            // 
            // FN_text
            // 
            this.FN_text.Location = new System.Drawing.Point(27, 45);
            this.FN_text.Name = "FN_text";
            this.FN_text.Size = new System.Drawing.Size(287, 20);
            this.FN_text.TabIndex = 1;
            this.FN_text.TextChanged += new System.EventHandler(this.FN_text_TextChanged);
            // 
            // openFile
            // 
            this.openFile.Image = global::Lab7.Properties.Resources.OpenPH;
            this.openFile.Location = new System.Drawing.Point(330, 34);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(34, 36);
            this.openFile.TabIndex = 2;
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Key: ";
            // 
            // textBox_key
            // 
            this.textBox_key.Location = new System.Drawing.Point(27, 101);
            this.textBox_key.Name = "textBox_key";
            this.textBox_key.Size = new System.Drawing.Size(114, 20);
            this.textBox_key.TabIndex = 4;
            this.textBox_key.TextChanged += new System.EventHandler(this.textBox_key_TextChanged);
            // 
            // encryptFile
            // 
            this.encryptFile.Location = new System.Drawing.Point(27, 150);
            this.encryptFile.Name = "encryptFile";
            this.encryptFile.Size = new System.Drawing.Size(81, 25);
            this.encryptFile.TabIndex = 5;
            this.encryptFile.Text = "Encrypt";
            this.encryptFile.UseVisualStyleBackColor = true;
            this.encryptFile.Click += new System.EventHandler(this.encryptFile_Click);
            // 
            // decryptFile
            // 
            this.decryptFile.Location = new System.Drawing.Point(143, 150);
            this.decryptFile.Name = "decryptFile";
            this.decryptFile.Size = new System.Drawing.Size(81, 25);
            this.decryptFile.TabIndex = 6;
            this.decryptFile.Text = "Decrypt";
            this.decryptFile.UseVisualStyleBackColor = true;
            this.decryptFile.Click += new System.EventHandler(this.decryptFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 209);
            this.Controls.Add(this.decryptFile);
            this.Controls.Add(this.encryptFile);
            this.Controls.Add(this.textBox_key);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.FN_text);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "File Encrypt/Decrypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FN_text;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_key;
        private System.Windows.Forms.Button encryptFile;
        private System.Windows.Forms.Button decryptFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

