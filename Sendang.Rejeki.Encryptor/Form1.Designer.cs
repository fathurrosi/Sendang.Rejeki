namespace Sendang.Rejeki.Encryptor
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConstring = new System.Windows.Forms.TextBox();
            this.textBoxCipherText = new System.Windows.Forms.TextBox();
            this.textBoxSalt = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Salt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CipherText";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Constring";
            // 
            // textBoxConstring
            // 
            this.textBoxConstring.Location = new System.Drawing.Point(60, 63);
            this.textBoxConstring.Multiline = true;
            this.textBoxConstring.Name = "textBoxConstring";
            this.textBoxConstring.Size = new System.Drawing.Size(577, 154);
            this.textBoxConstring.TabIndex = 3;
            this.textBoxConstring.Text = "Data Source=DESKTOP-5BJROPJ\\SQLSVR2K12;Initial Catalog=dbsendangrejeki;Persist Se" +
    "curity Info=True;User ID=sa;Password=sa123;";
            // 
            // textBoxCipherText
            // 
            this.textBoxCipherText.Location = new System.Drawing.Point(60, 34);
            this.textBoxCipherText.Name = "textBoxCipherText";
            this.textBoxCipherText.Size = new System.Drawing.Size(577, 20);
            this.textBoxCipherText.TabIndex = 4;
            this.textBoxCipherText.Text = "rezeki1234567890987654321";
            // 
            // textBoxSalt
            // 
            this.textBoxSalt.Location = new System.Drawing.Point(60, 9);
            this.textBoxSalt.Name = "textBoxSalt";
            this.textBoxSalt.Size = new System.Drawing.Size(577, 20);
            this.textBoxSalt.TabIndex = 5;
            this.textBoxSalt.Text = "sendang";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(60, 223);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(577, 154);
            this.textBox4.TabIndex = 6;
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(562, 383);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonEncrypt.TabIndex = 7;
            this.buttonEncrypt.Text = "Encrypt";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.buttonEncrypt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(481, 383);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Decrypt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 431);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonEncrypt);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBoxSalt);
            this.Controls.Add(this.textBoxCipherText);
            this.Controls.Add(this.textBoxConstring);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConstring;
        private System.Windows.Forms.TextBox textBoxCipherText;
        private System.Windows.Forms.TextBox textBoxSalt;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button buttonEncrypt;
        private System.Windows.Forms.Button button1;
    }
}

